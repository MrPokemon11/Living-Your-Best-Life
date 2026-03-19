using UnityEngine;
using Dialogue;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Events;

public class Act3Manager : MonoBehaviour
{
    [Header("Story Objects")]
    [SerializeField] private Story badStory;
    [SerializeField] private Story goodStory;

    [SerializeField] private Story choiceBad;
    [SerializeField] private Story choiceGood;
    
    [Header("Dialogue Systems")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private ActDirector actDirector;
    [SerializeField] private Act3TaskReveal act3TaskReveal;

    [Header("Choice Buttons")] 
    [SerializeField] private Button noButton;

    [SerializeField] private GameObject LyblLock;

    private bool addedListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        if (addedListener == false)
        {
            actDirector.alertOfEpilogue.AddListener(StartAct3);
            addedListener = true;
        }
        LyblLock.SetActive(false);
        noButton.interactable = true;
    }

    private void StartAct3()
    {
        if (actDirector.GetCurrentAct() != 3)
        {
            return;
        }
        dialogueSystem.DialogueEndEvent.AddListener(Act3Reveal);
        Debug.Log("Act 3 listener added");
    }

    private void Act3Reveal()
    {
        dialogueSystem.DialogueEndEvent.RemoveListener(Act3Reveal);
        act3TaskReveal.RevealTaskText();
        if (actDirector.GetIsGoodRoute())
        {
            dialogueSystem.StartDialogue(goodStory);
        }
        else
        {
            dialogueSystem.StartDialogue(badStory);
        }
        dialogueSystem.DialogueEndEvent.AddListener(Act3Choice);
    }

    private void Act3Choice()
    {
        if (actDirector.GetIsGoodRoute() && actDirector.GetAIUse() < 4 || !actDirector.GetIsGoodRoute() && actDirector.GetAIUse() < 3)
        {
            noButton.interactable = false;
            LyblLock.SetActive(true);
        }
    }
}
