using UnityEngine;
using UnityEngine.UI;
using Dialogue;

public class HandleButtonsDuringTasks : MonoBehaviour
{
    [SerializeField] private ActDirector actDirector;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private Button[] buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        dialogueSystem.DialogueEndEvent.AddListener(enableButtons);
    }

    private void enableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        dialogueSystem.DialogueEndEvent.RemoveListener(enableButtons);
    }
}
