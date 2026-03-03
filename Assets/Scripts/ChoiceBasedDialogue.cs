using UnityEngine;
using Dialogue;

public class ChoiceBasedDialogue : MonoBehaviour
{
    [Header("Story Objects")] 
    [SerializeField] private Story Act1Dialogue;
    [SerializeField] private Story[] Act2Dialogue;
    [SerializeField] private Story[] Act3Dialogue;
    
    [Header("Dialogue Objects")]
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private ActDirector actDirector;

    [Header("Range Limits")] 
    [SerializeField] private int Act2Limit;
    [SerializeField] private int[] Act3Limits;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCurrentDialogue()
    {
        int currentAct = actDirector.GetCurrentAct();
        if (currentAct == 1)
        {
            GetDialogueAct1();
        } else if (currentAct == 2)
        {
            GetDialogueAct2();
        }
        else
        {
            GetDialogueAct3();
        }
    }
    
    private Story GetDialogueAct1()
    {
        return Act1Dialogue;
    }

    private Story GetDialogueAct2()
    {
        if (actDirector.GetAIUse() >= Act2Limit)
        {
            return Act2Dialogue[1];
        }
        else
        {
            return Act2Dialogue[0];
        }
    }

    private Story GetDialogueAct3()
    {
        if (actDirector.GetAIUse() < Act3Limits[0])
        {
            return Act3Dialogue[0];
        } else if (actDirector.GetAIUse() >= Act3Limits[1])
        {
            return Act3Dialogue[2];
        }
        else
        {
            return Act3Dialogue[1];
        }
    }
}
