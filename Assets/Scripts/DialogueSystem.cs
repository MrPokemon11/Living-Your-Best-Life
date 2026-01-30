using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Dialogue;

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

    private void Start()
    {
        dialoguePanel.SetActive(false);
        responsePanel.SetActive(false);
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
            dialogueText.text = "Hi!";
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
            EndDialogue();
        }
    }


    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        responsePanel.SetActive(false);
        dialogueIndex = 0;
    }
}
