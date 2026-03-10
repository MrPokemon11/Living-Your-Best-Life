using UnityEngine;
using Dialogue;

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
    
    private bool Act2EpilogueSeen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actDirector = GameDirector.GetComponent<ActDirector>();
        LyblDialogue = LyblDialogueObject.GetComponent<ChoiceBasedDialogue>();
        PlayerDialogue = PlayerDialogueObject.GetComponent<ChoiceBasedDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleTransition()
    {
        if (Act2EpilogueSeen == false && actDirector.GetCurrentAct() == 2)
        {
            Act2EpilogueSeen = true;
            LyblDialogue.StartCurrentDialogue();
            PlayerDialogue.StartCurrentDialogue();
        }
    }
}
