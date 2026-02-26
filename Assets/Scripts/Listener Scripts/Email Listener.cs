using UnityEngine;

public class EmailListener : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private GameObject ActManager;
    
    DialogueSystem dialogueSystem;
    ActDirector actDirector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateListeners()
    {
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
        if (isManual == 1)
        {
            //script for manually writing the email
        }
        else
        {
            // script for LYBL writing the email
        }
    }

    void MarkComplete()
    {
        actDirector.MarkTaskAsDone("Email");
        RemoveListeners();
    }
}
