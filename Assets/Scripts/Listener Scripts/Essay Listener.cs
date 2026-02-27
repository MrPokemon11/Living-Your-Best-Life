using Dialogue;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EssayListener : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private GameObject ActManager;
    [SerializeField] private Story EssayStory;

    DialogueSystem dialogueSystem;
    ActDirector actDirector;   
    
    [Header("Relevant Game Objects")] 
    [SerializeField] private GameObject TransitionScreen;
    [SerializeField] private GameObject EssayText;
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private GameObject SubmissionIncompleteButton;
    private Button continueButton;

    [Header("Task Text")]
    [SerializeField] private GameObject TaskText;

    private bool hasDialogueBeenSeen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
        continueButton = ContinueButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateListeners()
    {
        Debug.Log("Activate Listeners");
        dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(WriteEmail);
        dialogueSystem.DialogueEndEvent.AddListener(MarkComplete);
        if (hasDialogueBeenSeen == false)
        {
            EssayDialogue();
        }
    }

    public void RemoveListeners()
    {
        dialogueSystem.DialogueImpactfulChoiceEvent.RemoveListener(WriteEmail);
        dialogueSystem.DialogueEndEvent.RemoveListener(MarkComplete);
    }

    void WriteEmail(int isManual)
    {
        if (isManual == 1)
        {
            //script for manually writing the essay
            dialogueSystem.AddGhostListener("Essay");
            dialogueSystem.GhostListeners.AddListener(transitionScreen);
        }
        else
        {
            // script for LYBL writing the essay
            EssayText.SetActive(true);
        }
    }

    void transitionScreen(string ghostDetector)
    {
        if (ghostDetector == "Essay")
        {
           TransitionScreen.SetActive(true);   
           dialogueSystem.GhostListeners.RemoveListener(transitionScreen);
        }

    }
    
    void MarkComplete()
    {
        SubmissionIncompleteButton.SetActive(false);
        hasDialogueBeenSeen = true; // set this so that dialogue doesn't trigger again
        RemoveListeners();
    }

    void EssayDialogue()
    {
        Debug.Log("Essay Dialogue");
        dialogueSystem.StartDialogue(EssayStory);
    }

    public void MarkTaskDone()
    {
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
    }
}
