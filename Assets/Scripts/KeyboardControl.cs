using UnityEngine;
using Dialogue;
using UnityEngine.UI;

public class KeyboardControl : MonoBehaviour
{
    [SerializeField] private DialogueSystem LyblDialogueSystem;
    [SerializeField] private DialogueSystem PlayerDialogueSystem;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (LyblDialogueSystem.currentStory._dialogueType == Story.DialogueType.Linear &&
                !LyblDialogueSystem.isTyping && !PlayerDialogueSystem.isTyping)
            {
                LyblDialogueSystem.Next();
                PlayerDialogueSystem.Next();
            } 
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (LyblDialogueSystem.currentStory._dialogueType == Story.DialogueType.Branching)
            {
                leftButton.onClick.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (LyblDialogueSystem.currentStory._dialogueType == Story.DialogueType.Branching)
            {
                rightButton.onClick.Invoke();
            }
        }
    }
}
