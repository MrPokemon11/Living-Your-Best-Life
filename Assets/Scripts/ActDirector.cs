using UnityEngine;

public class ActDirector : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogueManagers;
    private DialogueSystem lyblDialogueSystem;
    private DialogueSystem playerDialogueSystem;

    [SerializeField] private GameObject dynamicDialogueStarterObject;
    private DynamicDialogueStarter dynamicDialogueStarter;
    
    private int currentAct = 1;
    private int tasksComplete = 0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lyblDialogueSystem = dialogueManagers[0].GetComponent<DialogueSystem>();
        playerDialogueSystem = dialogueManagers[1].GetComponent<DialogueSystem>();
        dynamicDialogueStarter = dynamicDialogueStarterObject.GetComponent<DynamicDialogueStarter>();
        
        StartAct();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllTasksComplete()
    {
        
        //add 1 to the act number
        currentAct++;
        

    }

    private void StartAct()
    {
        //reset the number of completed tasks
        tasksComplete = 0;

        dynamicDialogueStarter.StartDialogueByAct();
    }
    
    public void ShowDialogue(string context)
    {
        dynamicDialogueStarter.StartDialogueThroughContext(currentAct.ToString(), context);
    }

    public int GetCurrentAct()
    {
        return currentAct;
    }
}
