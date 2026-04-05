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
    [SerializeField] private GameObject ChoiceBasedDialogue;
    
    ChoiceBasedDialogue ChoiceBasedDialogueScript;

    DialogueSystem dialogueSystem;
    ActDirector actDirector;   
    
    [Header("Relevant Game Objects")] 
    [SerializeField] private GameObject TransitionScreen;
    [SerializeField] private GameObject EssayAct1;
    [SerializeField] private GameObject EssayText;
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private GameObject SubmissionIncompleteButton;
    private Button continueButton;

    [Header("Task Text")]
    [SerializeField] private GameObject TaskText;
    private TextMeshProUGUI TaskTextText;

    private bool hasDialogueBeenSeen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
        continueButton = ContinueButton.GetComponent<Button>();
        ChoiceBasedDialogueScript = ChoiceBasedDialogue.GetComponent<ChoiceBasedDialogue>();
        TaskTextText = TaskText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateListeners()
    {
        Debug.Log("Activate Listeners");
        dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(WriteEssay);
        if (actDirector.GetCurrentAct() == 1)
        {
            dialogueSystem.DialogueEndEvent.AddListener(MarkComplete);        
        }

        
        if (actDirector.GetCurrentAct() == 2)
        {
            dialogueSystem.DialogueEndEvent.AddListener(MarkTaskDone);
            dialogueSystem.ReturnDialogueIndex.AddListener(ShowEssayByPath);
        }
    }

    public void RemoveListeners()
    {
        dialogueSystem.DialogueImpactfulChoiceEvent.RemoveListener(WriteEssay);

        if (actDirector.GetCurrentAct() == 1)
        {
            dialogueSystem.DialogueEndEvent.RemoveListener(MarkComplete);
        }
        
        if (actDirector.GetCurrentAct() == 2)
        {
            dialogueSystem.DialogueEndEvent.RemoveListener(MarkTaskDone);
            dialogueSystem.ReturnDialogueIndex.RemoveListener(ShowEssayByPath);
        }
    }

    public void Initialize()
    {
        if (actDirector.GetCurrentAct() != 3)
        {
            if (actDirector.GetCurrentAct() == 1)
            {
                TaskTextText.text = "- Submit Essay";
            }
            else
            {
                TaskTextText.text = "- Submit Latin Essay";
            }
            TaskTextText.fontStyle = FontStyles.Normal;
            hasDialogueBeenSeen = false;
            EssayText.SetActive(false);
            EssayAct1.SetActive(false);
            TransitionScreen.SetActive(false);
        }

    }
    
    void WriteEssay(int isManual)
    {
        if (actDirector.GetCurrentAct() == 1)
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
                EssayAct1.SetActive(true);
            }            
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
        ChoiceBasedDialogueScript.MarkDialogueSeen(); // set this so that dialogue doesn't trigger again
        RemoveListeners();
    }

    void EssayDialogue()
    {
        Debug.Log("Essay Dialogue");
        //dialogueSystem.StartDialogue(EssayStory);
    }

    public void MarkTaskDone()
    {
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
        actDirector.MarkTaskAsDone("Essay");
    }

    private void ShowEssayByPath(int index)
    {
        if (actDirector.GetIsGoodRoute() || actDirector.GetCurrentAct() == 1)
        {
            if (index == 4 || actDirector.GetCurrentAct() == 1)
            {
                EssayText.SetActive(true);
            }
        }
        else
        {
            if (index == 10 || index == 28)
            {
                EssayText.SetActive(true);
            }
        }
    }

    public void delayedEssayCompletion()
    {
        dialogueSystem.DialogueEndEvent.AddListener(MarkTaskDone);
    }
}
