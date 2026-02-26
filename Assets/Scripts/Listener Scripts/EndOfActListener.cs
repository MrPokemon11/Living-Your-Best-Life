using UnityEngine;
using UnityEngine.Events;

public class EndOfActListener : MonoBehaviour
{
    public bool IsDemo = true;
    [SerializeField] private GameObject TransitionScreen;
    
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private GameObject ActManager;
    
    DialogueSystem dialogueSystem;
    ActDirector actDirector;
    private UnityEvent triggerNextAct;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
        if (triggerNextAct == null)
        {
            triggerNextAct = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateListeners()
    {
        dialogueSystem.DialogueEndEvent.AddListener(TransitionToNextAct);
    }

    public void RemoveListeners()
    {
        dialogueSystem.DialogueEndEvent.RemoveListener(TransitionToNextAct);
    }

    void TransitionToNextAct()
    {
        TransitionScreen.SetActive(true);
        RemoveListeners();
        if (!IsDemo)
        {
            triggerNextAct.Invoke();
        }
    }

    void TriggerNextAct()
    {
        //add code to move to the next act
    }
}
