using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActDirector : MonoBehaviour
{
    [Header("Dialogue Managers")]
    [SerializeField] private GameObject[] dialogueManagers;
    private DialogueSystem lyblDialogueSystem;
    private DialogueSystem playerDialogueSystem;
    
    [Header("Dialogue Starters")]
    // the act director needs two DynamicDialogueStarters, one for the intro of each act and one for when all 3 tasks are complete
    [SerializeField] private GameObject lyblDialogueStarterObject;
    [SerializeField] private GameObject playerDialogueStarterObject;
    [SerializeField] private GameObject lyblActFinisherObject;
    [SerializeField] private GameObject playerActFinisherObject;
    [SerializeField] private GameObject TransitionManager;
    private ChoiceBasedDialogue lyblDialogueStarter;
    private ChoiceBasedDialogue playerDialogueStarter;
    private ChoiceBasedDialogue lyblActFinisher;
    private ChoiceBasedDialogue playerActFinisher;
    
    [SerializeField] private int currentAct = 0;
    private List<string> tasksCompleted;
    private bool actFinished = false;
    
    [Header("Initializable Objects")]
    [SerializeField] private GameObject emailListener;
    [SerializeField] private GameObject essayListener;
    [SerializeField] private GameObject photoListener;
    
    [Header("Epilogue Objects")]
    public UnityEvent alertOfEpilogue;

    [SerializeField] private GameObject[] closeButtons;
    [SerializeField] private GameObject MainWindow;
    
    //other variables
    private int AIUse = 0;
    private int totalAIUse = 0;
    private bool isGoodRoute = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lyblDialogueSystem = dialogueManagers[0].GetComponent<DialogueSystem>();
        playerDialogueSystem = dialogueManagers[1].GetComponent<DialogueSystem>();
        lyblDialogueStarter = lyblDialogueStarterObject.GetComponent<ChoiceBasedDialogue>();
        playerDialogueStarter = playerDialogueStarterObject.GetComponent<ChoiceBasedDialogue>();
        lyblActFinisher = lyblActFinisherObject.GetComponent<ChoiceBasedDialogue>();
        playerActFinisher = playerActFinisherObject.GetComponent<ChoiceBasedDialogue>();
        tasksCompleted = new List<string>();

        if (alertOfEpilogue == null)
        {
            alertOfEpilogue = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(tasksCompleted.Count);
        }
        if (tasksCompleted.Count >= 3 && !actFinished)
        {
            AllTasksComplete();
        }
    }

    public void InitializeObjects()
    {
        emailListener.GetComponent<EmailListener>().Initialize();
        essayListener.GetComponent<EssayListener>().Initialize();
        photoListener.GetComponent<PhotoListener>().Initialize();
    }
    
    public void AllTasksComplete()
    {
        Debug.Log("All Tasks Complete");
        alertOfEpilogue.Invoke();
        foreach (GameObject button in closeButtons)
        {
            button.GetComponent<TraverseWindow>().TraverseToWindow(MainWindow);
        }
        
        //mark the act as finished
        actFinished = true;
        if (AIUse >= 2 && currentAct == 1)
        {
            ToggleGoodRoute();
        }
        
        lyblActFinisher.StartCurrentDialogue();
        playerActFinisher.StartCurrentDialogue();
        TransitionManager.GetComponent<EndOfActListener>().ActivateListeners();
    }

    public int GetCurrentAct()
    {
        return currentAct;
    }
    
    public void StartAct()
    {
        // add the AI use from the previous act to the total, reset the current AI use to 0
        totalAIUse += AIUse;
        AIUse = 0;
        
        //add 1 to the act number
        currentAct++;
        
        //reset the number of completed tasks
        tasksCompleted.Clear();
        actFinished = false;

        lyblDialogueStarter.StartCurrentDialogue();
        playerDialogueStarter.StartCurrentDialogue();
        
        //only initialize objects if it isn't act 3, as no tasks are done during act 3
        if (currentAct != 3)
        {
            InitializeObjects();  
        }

        
        lyblDialogueSystem.DialogueImpactfulChoiceEvent.AddListener(AddAIUse);
    }



    public void MarkTaskAsDone(string task)
    {
        foreach (var t in tasksCompleted)
        {
            if (t == task)
            {
                return;
            }
        }
        tasksCompleted.Add(task);
    }

    public int GetAIUse()
    {
        return totalAIUse;
    }

    public void AddAIUse(int value)
    {
        // this number is actually inverted; the less the AI is used, the higher the score
        AIUse += value;
        Debug.Log("Points added: " + value);
    }

    public void ToggleGoodRoute()
    {
        isGoodRoute = !isGoodRoute;
    }

    public bool GetIsGoodRoute()
    {
        return isGoodRoute;
    }
}
