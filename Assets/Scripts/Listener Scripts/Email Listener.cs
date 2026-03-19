using UnityEngine;
using System.Collections;
using Dialogue;
using TMPro;
using UnityEngine.UI;

public class EmailListener : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private GameObject ActManager;
    [SerializeField] private Story EmailStory;
    [SerializeField] private GameObject ChoiceBasedDialogue;
    
    ChoiceBasedDialogue ChoiceBasedDialogueScript;
    DialogueSystem dialogueSystem;
    ActDirector actDirector;   
    
    [Header("Relevant Game Objects")] 
    [SerializeField] private GameObject TransitionScreen;
    [SerializeField] private GameObject EmailText;
    [SerializeField] private GameObject EmailAct2Good;
    [SerializeField] private GameObject EmailAct2Bad;
    [SerializeField] private GameObject ContinueButton;
    private Button continueButton;
    private int act2YesHandler;
    
    [Header("Task Text")]
    [SerializeField] private GameObject TaskText;

    private bool hasDialogueBeenSeen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
        continueButton = ContinueButton.GetComponent<Button>();
        ChoiceBasedDialogueScript = ChoiceBasedDialogue.GetComponent<ChoiceBasedDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        hasDialogueBeenSeen = false;
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        TransitionScreen.SetActive(false);
        EmailText.SetActive(false);
        EmailAct2Good.SetActive(false);
        EmailAct2Bad.SetActive(false);
    }
    
    public void ActivateListeners()
    {
        Debug.Log("Activate Listeners");
        dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(WriteEmail);
        dialogueSystem.DialogueEndEvent.AddListener(MarkComplete);
    }

    public void RemoveListeners()
    {
        dialogueSystem.DialogueImpactfulChoiceEvent.RemoveListener(WriteEmail);
        dialogueSystem.DialogueEndEvent.RemoveListener(MarkComplete);
    }

    void WriteEmail(int isManual)
    {
        if (actDirector.GetCurrentAct() == 1)
        {
           if (isManual == 1)
           {
               //script for manually writing the Emails
               dialogueSystem.AddGhostListener("Email");
               dialogueSystem.GhostListeners.AddListener(transitionScreen);
           }
           else
           {
               // script for LYBL writing the Email
               EmailText.SetActive(true);
           } 
        }
        act2YesHandler = isManual;
    }

    void transitionScreen(string ghostDetector)
    {
        if (ghostDetector == "Email")
        {
           TransitionScreen.SetActive(true);   
           dialogueSystem.GhostListeners.RemoveListener(transitionScreen);
        }

    }
    
    void MarkComplete()
    {
        actDirector.MarkTaskAsDone("Email");
        ChoiceBasedDialogueScript.MarkDialogueSeen();
        MarkTaskDone();
        RemoveListeners();
    }

    void EmailDialogue()
    {
        Debug.Log("Email Dialogue");
        //dialogueSystem.StartDialogue(EmailStory);
    }

    public void MarkTaskDone()
    {
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
    }

    private void ShowEmailByPath(int index)
    {
        if (actDirector.GetIsGoodRoute())
        {
            if ((index == 5 && act2YesHandler == 0) || index == 14)
            {
                EmailAct2Good.SetActive(true);
            }
        }
        else
        {
            if ((index == 6 && act2YesHandler == 0) || index == 16)
            {
                EmailAct2Bad.SetActive(true);
            }
        }
    }
}
