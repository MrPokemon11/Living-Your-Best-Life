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
    
    [SerializeField] private GameObject DialogueObject;
    private DialogueSystem dialogueSystem;
    [SerializeField] private GameObject TalkyBox;
    [SerializeField] private Act3Manager act3Manager;
    
    
    private bool Act2EpilogueSeen = false;
    
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
        
    }

    public void HandleTransition()
    {
        if (Act2EpilogueSeen == false && actDirector.GetCurrentAct() == 3)
        {
            Act2EpilogueSeen = true;
            LyblDialogue.StartCurrentDialogue();
            PlayerDialogue.StartCurrentDialogue();
            gameObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            dialogueSystem.DialogueEndEvent.AddListener(act3Dialogue);
            TalkyBox.GetComponent<Button>().onClick.AddListener(() => {gameObject.SetActive(false);});
        }
        else if (Act2EpilogueSeen == false)
        {
            gameObject.SetActive(false);
        }
    }

    private void act3Dialogue()
    {
        //Act3Dialogue.StartCurrentDialogue();
        //dialogueSystem.DialogueEndEvent.RemoveListener(act3Dialogue);
    }
}
