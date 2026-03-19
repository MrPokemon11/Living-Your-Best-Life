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
    
    [Header("UI Elements")]    
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject LyblLock;
    [SerializeField] private GameObject finaleScreen;

    [Header("Misc.")] 
    [SerializeField] private GameObject MainCamera;

    private bool addedListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LyblLock.SetActive(false);
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

    public void StartAct3()
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
        dialogueSystem.DialogueEndEvent.RemoveListener(Act3Choice);
        if (actDirector.GetIsGoodRoute() && actDirector.GetAIUse() < 4 || !actDirector.GetIsGoodRoute() && actDirector.GetAIUse() < 2)
        {
            noButton.interactable = false;
            LyblLock.SetActive(true);
        }
        dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(Act3Outcome);
        if (actDirector.GetIsGoodRoute())
        {
            dialogueSystem.StartDialogue(choiceGood);
        }
        else
        {
            dialogueSystem.StartDialogue(choiceBad);
        }
    }

    private void Act3Outcome(int choice)
    {
        bool doesLyblGetDeleted = choice == 1; // weird looking code JetBrains, but ok
        
    }

    private void GoodNight()
    {
        this.GetComponent<TurnOffElectronics>().TurnOff();
        
        MainCamera.GetComponent<ZoomCamera>().ZoomOut();
        finaleScreen.GetComponent<FinaleScreen>().FadeIn();
    }
}
