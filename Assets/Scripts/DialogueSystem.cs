using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Dialogue;
using UnityEngine.Events; 

// Code by Azukie on Itch; Edits/additions by me (Devon) are labeled

public class DialogueSystem : MonoBehaviour
{
    public Story currentStory;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button[] responseButtons; 
    public GameObject responsePanel; 


    [Header("Typing Settings")]
    public float typingSpeed = 0.03f;
    private float lastTypingSpeed = 0.0f;

    private int dialogueIndex = 0;
    public bool isTyping = false;
    private bool preventLooping = false;
    private int currentBranchingStep = 0;

    private int lastImpactfulChoice = -1;
    
    // unity events for returning dialogue data to listeners; added by me
    public UnityEvent DialogueEndEvent;
    public UnityEvent<int> DialogueImpactfulChoiceEvent;
    public UnityEvent<int> ReturnDialogueIndex;
    
    // ghost listeners are non-persistent listeners that ignore RemoveAllListeners(), and something that i made up - Devon
    public UnityEvent<string> GhostListeners;
    private List<string> activeGhosts;
    bool isAlertingGhosts = false;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        responsePanel.SetActive(false);

        // ensure that the events and lists exist
        if (DialogueEndEvent == null)
        {
            DialogueEndEvent = new UnityEvent();
        }
        
        if (DialogueImpactfulChoiceEvent == null)
        {
            DialogueImpactfulChoiceEvent = new UnityEvent<int>();
        }
        
        if (GhostListeners == null)
        {
            GhostListeners = new UnityEvent<string>();
        }

        if (activeGhosts == null)
        {
            activeGhosts = new List<string>();
        }

        if (ReturnDialogueIndex == null)
        {
            ReturnDialogueIndex = new UnityEvent<int>();
        }
        
        lastTypingSpeed = typingSpeed;
    }

    public void StartDialogue(Story story)
    {
        currentStory = story;
        dialogueIndex = 0;
        currentBranchingStep = 0;

        dialoguePanel.SetActive(true);
        responsePanel.SetActive(false);
        nameText.text = currentStory._NPCName;

        if (currentStory._dialogueType == Story.DialogueType.Linear)
        {
            ShowLinearDialogue();
        }
        else
        {
            //extremely hacky solution, but it's not like the player name is used for anything else -Devon
            //also figure out how to get the first line typed, the TypeLine coroutine works but causes problems if the player goes too fast through dialogue
            //StartCoroutine(TypeLine(currentStory._playerName));
            
            dialogueText.text = currentStory._playerName;
            ShowBranchingDialogue();

        }
    }

    public void Next()
    {
        if (currentStory._dialogueType == Story.DialogueType.Linear)
        {
            if (dialogueIndex < currentStory.linear._pharases.Length)
            {
                ShowLinearDialogue();
            }
            else
            {
                ReportDialogueFinished();
                EndDialogue();
            }
        }
    }

    void ShowLinearDialogue()
    {
        //fast dialogue (Doesn't work properly, can cause desyncs)
        //if (isTyping)
        //{
        //    typingSpeed = 0.0f;
        //    return;
        //}        
        responsePanel.SetActive(false);
        StopAllCoroutines();
        string phrase = currentStory.linear._pharases[dialogueIndex];
        StartCoroutine(TypeLine(phrase));
        

       
        if (currentStory.linear._haveResponses && dialogueIndex < currentStory.linear._responses.Length)
        {
            responsePanel.SetActive(true);
            for (int i = 0; i < responseButtons.Length; i++)
            {
                if (i == 0)
                {
                    responseButtons[i].gameObject.SetActive(true);
                    responseButtons[i].GetComponentInChildren<TMP_Text>().text = currentStory.linear._responses[dialogueIndex];
                    int capturedIndex = dialogueIndex;
                    responseButtons[i].onClick.RemoveAllListeners();
                    responseButtons[i].onClick.AddListener(() =>
                    {
                        ReportDialogueIndex();
                        responsePanel.SetActive(false);
                        dialogueIndex++;
                        Next();
                    });
                }
                else
                {
                    responseButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            responsePanel.SetActive(false);
            dialogueIndex++;
        }
    }

    void ShowBranchingDialogue()
    {
        // fast dialogue (doesn't work, causes the rest to not run)
        //if (isTyping)
        //{
        //    typingSpeed = 0.0f;
        //    return;
        //}
        responsePanel.SetActive(true);
        nameText.text = currentStory._NPCName;

        var step = currentStory.branching[currentBranchingStep];

        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < step._Question.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<TMP_Text>().text = step._Question[i];

                int index = i;
                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() =>
                {
                    responsePanel.SetActive(false);
                    AlertGhostListeners();
                    // this is my solution to "branching" dialogue where the player only has one choice (since there's no point in reporting it)
                    if (step._Question.Length > 1)
                    {
#if UNTIY_EDITOR
                        Debug.Log(index)
#endif
                        ReportDialogueChoice(index);                        
                    }
                    ReportDialogueIndex();
                    StopAllCoroutines();
                    StartCoroutine(ShowNpcResponseThenNext(step._NpcResponses[index], step.nextStepIndices[index]));
                });
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }
    }



    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        typingSpeed = lastTypingSpeed;
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingSpeed = lastTypingSpeed;
        isTyping = false;
    }

    IEnumerator ShowNpcResponseThenNext(string npcResponse, int nextIndex)
    {
        yield return StartCoroutine(TypeLine(npcResponse));

        yield return new WaitForSeconds(1f);

        if (nextIndex >= 0 && nextIndex < currentStory.branching.Length)
        {
            currentBranchingStep = nextIndex;
            ShowBranchingDialogue();
        }
        else
        {
            ReportDialogueFinished();
            EndDialogue();
        }
    }


    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        responsePanel.SetActive(false);
        dialogueIndex = 0;
        currentBranchingStep = 0;
    }

    /*****************************************************
    Everything below this line was created by Devon
    ******************************************************/
    
    // report the player's choice to anything listening
    public void ReportDialogueChoice(int choice)
    {
        DialogueImpactfulChoiceEvent.Invoke(choice);
        lastImpactfulChoice = choice;
    }

    public int ReportLastImpactfulChoice()
    {
        return lastImpactfulChoice;
    }
    
    // report that dialogue has finished, and the result (if any)
    public void ReportDialogueFinished()
    {
        DialogueEndEvent.Invoke();
    }

    //
    public void AddGhostListener(string ghostName)
    {
        activeGhosts.Add(ghostName);
        Debug.Log("Added ghost with name " + ghostName);
    }

    public void AlertGhostListeners()
    {
        isAlertingGhosts = true;
        foreach (string ghost in activeGhosts)
        {
            GhostListeners.Invoke(ghost);
        }

        isAlertingGhosts = false;
    }

    // tries to remove a ghost 
    public void RemoveGhostListener(string ghostName)
    {
        
        foreach (string ghost in activeGhosts)
        {
            if (ghost == ghostName && !isAlertingGhosts)
            {
                Debug.Log("Removed ghost with name " + ghostName);                
                activeGhosts.Remove(ghost);
                return;
            } else if (ghost == ghostName && isAlertingGhosts)
            {
                Debug.Log("Failed to remove ghost with name " + ghostName + " because ghosts are being alerted. Try adding a delay, and try again.");
            }
        }
        Debug.Log("No ghost found with name " + ghostName + " so no ghost was removed; did you make a typo?");
    }

    // removes every ghost listener, and clears the list of active ghosts
    public void BustAllGhosts()
    {
        GhostListeners.RemoveAllListeners();
        activeGhosts.Clear();
        isAlertingGhosts = false;
        Debug.Log("Ghosts busted.");
    }

    public void ResetDialogueSystem()
    {
        BustAllGhosts();
        dialogueIndex = 0;
        isTyping = false;
        currentBranchingStep = 0;
        lastImpactfulChoice = -1;
        isAlertingGhosts = false; 
    }

    public void ReportDialogueIndex()
    {
        if(currentStory._dialogueType == Story.DialogueType.Branching)
        {
            ReturnDialogueIndex.Invoke(currentBranchingStep);
        }
        else
        {
            ReturnDialogueIndex.Invoke(dialogueIndex);
        }
    }
}
