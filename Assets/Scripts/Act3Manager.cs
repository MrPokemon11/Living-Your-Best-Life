using UnityEngine;
using Dialogue;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Act3Manager : MonoBehaviour
{
    [Header("Story Objects")] 
    [SerializeField] private Story GoodBridgeLybl;
    [SerializeField] private Story BadBridgeLybl;
    [SerializeField] private Story GoodBridgePlayer;
    [SerializeField] private Story BadBridgePlayer;
    
    [SerializeField] private Story badStory;
    [SerializeField] private Story goodStory;

    [SerializeField] private Story choiceBad;
    [SerializeField] private Story choiceGood;
    
    [Header("Dialogue Systems")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private DialogueSystem playerDialogueSystem;
    [SerializeField] private ActDirector actDirector;
    [SerializeField] private Act3TaskReveal act3TaskReveal;
    
    [Header("UI Elements")]    
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject LyblLock;
    [SerializeField] private GameObject finaleScreen;
    
    [Header("Lybl Objects")]
    [SerializeField] private GameObject LyblIcon;
    [SerializeField] private GameObject LyblWindow;
    private Material lyblWindowMaterial;
    [SerializeField] private GameObject LyblDialogue;
    [SerializeField] private TMP_FontAsset normalFont;
    [SerializeField] private TMP_FontAsset glitchFont;

    [Header("Misc.")] 
    [SerializeField] private GameObject MainCamera;

    private bool addedListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LyblLock.SetActive(false);
        lyblWindowMaterial = LyblWindow.GetComponent<Image>().material;
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
        if(actDirector.GetIsGoodRoute())
        {
            dialogueSystem.StartDialogue(GoodBridgeLybl);
            playerDialogueSystem.StartDialogue(GoodBridgePlayer);
        }
        else
        {
            dialogueSystem.StartDialogue(BadBridgeLybl);
            playerDialogueSystem.StartDialogue(BadBridgePlayer);
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
        LyblLock.SetActive(false);
        
        
        //delete LYBL if the story calls for it
        if (doesLyblGetDeleted)
        {
            dialogueSystem.ReturnDialogueIndex.AddListener(hideIcon);
            dialogueSystem.ReturnDialogueIndex.AddListener(hideWindow);
            dialogueSystem.ReturnDialogueIndex.AddListener(glitchWindow);
            
        }
        
        dialogueSystem.DialogueEndEvent.AddListener(GoodNight);
    }

    private void GoodNight()
    {
        dialogueSystem.DialogueEndEvent.RemoveListener(GoodNight);
        this.GetComponent<TurnOffElectronics>().TurnOff();
        MainCamera.GetComponent<ZoomCamera>().ZoomOut();
        finaleScreen.GetComponent<FinaleScreen>().FadeIn();
    }

    private void hideIcon(int when)
    {
        if (actDirector.GetIsGoodRoute() && when == 17)
        {
            LyblIcon.SetActive(false);
            dialogueSystem.ReturnDialogueIndex.RemoveListener(hideIcon);            
        } else if (!actDirector.GetIsGoodRoute() && when == 9)
        {
            LyblIcon.SetActive(false);
            dialogueSystem.ReturnDialogueIndex.RemoveListener(hideIcon);
        }

    }

    private void hideWindow(int when)
    {
        if (actDirector.GetIsGoodRoute() && when == 17)
        {
            LyblWindow.gameObject.transform.parent.gameObject.SetActive(false);
            dialogueSystem.ReturnDialogueIndex.RemoveListener(hideWindow);            
        } else if (!actDirector.GetIsGoodRoute() && when == 10)
        {
            LyblWindow.gameObject.transform.parent.gameObject.SetActive(false);
            dialogueSystem.ReturnDialogueIndex.RemoveListener(hideWindow);                  
        }

    }

    private void glitchWindow(int when)
    {
        if (!actDirector.GetIsGoodRoute() && when == 9)
        {
            lyblWindowMaterial.SetFloat("_Shear_Intensity", 0.03f);
            LyblDialogue.GetComponent<TextMeshProUGUI>().font = glitchFont;
            dialogueSystem.ReturnDialogueIndex.RemoveListener(glitchWindow);        
        }
        
    }

    public void undoDeletion()
    {
        LyblIcon.SetActive(true);
        lyblWindowMaterial.SetFloat("_Shear_Intensity", 0.0f);
        LyblDialogue.GetComponent<TextMeshProUGUI>().font = normalFont;
        LyblWindow.gameObject.transform.parent.gameObject.SetActive(true);
    }
}
