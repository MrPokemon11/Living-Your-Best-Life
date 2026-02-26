using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Dialogue;
using UnityEngine.Events; // added by me

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

    private int dialogueIndex = 0;
    private bool isTyping = false;
    private int currentBranchingStep = 0;
    
    // unity events for returning dialogue data to listeners; added by me
    public UnityEvent DialogueEndEvent;
    public UnityEvent<int> DialogueImpactfulChoiceEvent;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        responsePanel.SetActive(false);

        // ensure that the events exist
        if (DialogueEndEvent == null)
        {
            DialogueEndEvent = new UnityEvent();
        }
        if (DialogueImpactfulChoiceEvent == null)
        {
            DialogueImpactfulChoiceEvent = new UnityEvent<int>();
        }
    }

    public void StartDialogue(Story story)
    {
        currentStory = story;
        dialogueIndex = 0;

        dialoguePanel.SetActive(true);
        responsePanel.SetActive(false);
        nameText.text = currentStory._NPCName;

        if (currentStory._dialogueType == Story.DialogueType.Linear)
        {
            ShowLinearDialogue();
        }
        else
        {
            ShowBranchingDialogue();
            //extremely hacky solution, but it's not like the player name is used for anything else -Devon
            dialogueText.text = currentStory._playerName;
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
                    
                    // this is my solution to "branching" dialogue where the player only has one choice (since there's no point in reporting it)
                    if (step._Question.Length > 1)
                    {
                        ReportDialogueChoice(index);                        
                    }

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
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
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
    }

    // report the player's choice to anything listening
    // code by me
    public void ReportDialogueChoice(int choice)
    {
        DialogueImpactfulChoiceEvent.Invoke(choice);
    }
    
    // report that dialogue has finished, and the result (if any)
    // code by me
    public void ReportDialogueFinished()
    {
        DialogueEndEvent.Invoke();
    }
}
