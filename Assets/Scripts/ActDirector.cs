using System.Collections.Generic;
using UnityEngine;

public class ActDirector : MonoBehaviour
{
    [Header("Dialogue Managers")]
    [SerializeField] private GameObject[] dialogueManagers;
    private DialogueSystem lyblDialogueSystem;
    private DialogueSystem playerDialogueSystem;
    
    [Header("Dialogue Starters")]
    // the act director needs two DynamicDialogueStarters, one for the intro of each act and one for when all 3 tasks are complete
    [SerializeField] private GameObject dynamicDialogueStarterObject;
    [SerializeField] private GameObject actFinisherObject;
    [SerializeField] private GameObject TransitionManager;
    private DynamicDialogueStarter dynamicDialogueStarter;
    private DynamicDialogueStarter actFinisher;
    
    private int currentAct = 0;
    private List<string> tasksCompleted;
    private bool actFinished = false;
    
    [Header("Initializable Objects")]
    [SerializeField] private GameObject emailListener;
    [SerializeField] private GameObject essayListener;
    [SerializeField] private GameObject photoListener;
    
    //other variables
    private int AIUse = 0;
    private int totalAIUse = 0;
    private bool isGoodRoute = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lyblDialogueSystem = dialogueManagers[0].GetComponent<DialogueSystem>();
        playerDialogueSystem = dialogueManagers[1].GetComponent<DialogueSystem>();
        dynamicDialogueStarter = dynamicDialogueStarterObject.GetComponent<DynamicDialogueStarter>();
        actFinisher = actFinisherObject.GetComponent<DynamicDialogueStarter>();
        tasksCompleted = new List<string>();
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
        
        //mark the act as finished
        actFinished = true;
        if (AIUse >= 2 && currentAct == 1)
        {
            ToggleGoodRoute();
        }
        
        actFinisher.StartDialogueByAct();
        TransitionManager.GetComponent<EndOfActListener>().ActivateListeners();
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

        dynamicDialogueStarter.StartDialogueByAct();
        InitializeObjects();
        
        lyblDialogueSystem.DialogueImpactfulChoiceEvent.AddListener(AddAIUse);
    }
    
    public void ShowDialogue(string context)
    {
        dynamicDialogueStarter.StartDialogueThroughContext(currentAct.ToString(), context);
    }

    public int GetCurrentAct()
    {
        return currentAct;
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
