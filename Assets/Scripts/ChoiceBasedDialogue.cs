using UnityEngine;
using Dialogue;

public class ChoiceBasedDialogue : MonoBehaviour
{
    [Header("Story Objects")] 
    [SerializeField] private Story Act1Dialogue;
    [SerializeField] private Story[] Act2Dialogue;
    [SerializeField] private Story[] Act3Dialogue;
    [SerializeField] private Story ErrorText;
    
    [Header("Dialogue Objects")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private ActDirector actDirector;

    [Header("Range Limits")] 
    [SerializeField] private int Act2Limit = 2;
    [SerializeField] private int[] Act3Limits;

    private bool dialogueSeen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Act1Dialogue == null)
        {
            Act1Dialogue = ScriptableObject.CreateInstance<Story>();
        }

        if (Act2Dialogue == null)
        {
            Act2Dialogue = new Story[2];
        }

        if (Act3Dialogue == null)
        {
            Act3Dialogue = new Story[4];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCurrentDialogue()
    {
        if (!dialogueSeen)
        {
            dialogueSystem.StartDialogue(GetCurrentDialogue());
        }

    }

    public void MarkDialogueSeen()
    {
        dialogueSeen = true;
    }
    
    public void MarkDialogueUnseen()
    {
        dialogueSeen = false;
    }
    
    public Story GetCurrentDialogue()
    {
        int currentAct = actDirector.GetCurrentAct();
        Story currentStory = ErrorText;
        if (currentAct == 1)
        {
            currentStory = GetDialogueAct1();
        } else if (currentAct == 2)
        {
            currentStory = GetDialogueAct2();
        }
        else
        {
            currentStory = GetDialogueAct3();
        }
        return currentStory;
    }
    
    private Story GetDialogueAct1()
    {
        return Act1Dialogue;
    }

    private Story GetDialogueAct2()
    {
        // the AI use score is inverted; the less it is used, the higher the player's score
        if (actDirector.GetAIUse() <= Act2Limit)
        {
            return Act2Dialogue[1]; // bad dialogue
        }
        else
        {
            return Act2Dialogue[0]; // good dialogue
        }
    }

    private Story GetDialogueAct3()
    {
        bool isGoodRoute = actDirector.GetIsGoodRoute();
        if (isGoodRoute)
        {
            if (actDirector.GetAIUse() > Act3Limits[1])
            {
                return Act3Dialogue[0]; //good dialogue
            } else
            {
                return Act3Dialogue[1]; // neutral-good dialogue
            }            
        }
        else
        {
            if (actDirector.GetAIUse() > Act3Limits[0])
            {
                return Act3Dialogue[2]; // neutral-bad dialogue
            }
            else
            {
                 return Act3Dialogue[3]; // bad dialogue                
            }
        }
    }
}
