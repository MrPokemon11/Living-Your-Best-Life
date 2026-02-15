using UnityEngine;

public class ActDirctor : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogueManagers;
    private DialogueSystem lyblDialogueSystem;
    DialogueSystem playerDialogueSystem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lyblDialogueSystem = dialogueManagers[0].GetComponent<DialogueSystem>();
        playerDialogueSystem = dialogueManagers[1].GetComponent<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllTasksComplete()
    {
        
    }
}
