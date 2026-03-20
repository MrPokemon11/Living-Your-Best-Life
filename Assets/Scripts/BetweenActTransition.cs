using System.Security;
using UnityEngine;
using Dialogue;
using TMPro;
using UnityEngine.UI;

public class BetweenActTransition : MonoBehaviour
{
    [Header("Director Object")]
    [SerializeField] private GameObject GameDirector;
    private ActDirector actDirector;

    [Header("Dialogue Objects")]
    [SerializeField] private GameObject LyblDialogueObject;
    [SerializeField] private GameObject PlayerDialogueObject;
    private ChoiceBasedDialogue LyblDialogue;
    private ChoiceBasedDialogue PlayerDialogue;
    [SerializeField] private TextMeshProUGUI transitionText;
    
    [SerializeField] private GameObject DialogueObject;
    private DialogueSystem dialogueSystem;
    [SerializeField] private GameObject TalkyBox;
    [SerializeField] private Act3Manager act3Manager;


    private bool doesTriggerExist = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actDirector = GameDirector.GetComponent<ActDirector>();
        LyblDialogue = LyblDialogueObject.GetComponent<ChoiceBasedDialogue>();
        PlayerDialogue = PlayerDialogueObject.GetComponent<ChoiceBasedDialogue>();
        dialogueSystem = DialogueObject.GetComponent<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doesTriggerExist)
        {
            actDirector.StartInitializing.AddListener(ResetVars);
        }
    }

    public void HandleTransition()
    {
        if (transitionText.fontStyle == FontStyles.Normal && actDirector.GetCurrentAct() == 3)
        {
            transitionText.fontStyle = FontStyles.Strikethrough;
            GetComponent<Button>().interactable = false;
            TalkyBox.GetComponent<Button>().onClick.AddListener(() => {gameObject.SetActive(false);});
        }
        else
        {
            gameObject.SetActive(false);
            transitionText.fontStyle = FontStyles.Normal;
        }
    }

    private void act3Dialogue()
    {
        //Act3Dialogue.StartCurrentDialogue();
        //dialogueSystem.DialogueEndEvent.RemoveListener(act3Dialogue);
    }

    void ResetVars()
    {
        GetComponent<Button>().interactable = true;
        transitionText.fontStyle = FontStyles.Normal;
    }
}
