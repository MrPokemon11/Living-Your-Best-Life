using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PhotoListener : MonoBehaviour
{
    [Header("Photos")]
    [SerializeField] GameObject photo;
    [SerializeField] GameObject photo2;
    [SerializeField] GameObject photo3;
    [SerializeField] GameObject photo4;
    
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;
    [SerializeField] private GameObject ActManager;
    [SerializeField] private GameObject ChoiceBasedDialogue;
    
    ChoiceBasedDialogue ChoiceBasedDialogueScript;
    
    [Header("Task Text")]
    [SerializeField] private GameObject TaskText;
    
    DialogueSystem dialogueSystem;
    ActDirector actDirector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
        ChoiceBasedDialogueScript = ChoiceBasedDialogue.GetComponent<ChoiceBasedDialogue>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        //as a reminder to myself, the act director blocks initialization during act 3
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        //gameObject.transform.parent.gameObject.SetActive(false);
        photo.SetActive(false);
        photo2.SetActive(false);
        photo3.SetActive(false);
        photo4.SetActive(false);            
        

    }
    
    public void ActivateListeners()
    {
        if (actDirector.GetCurrentAct() == 1)
        {
           dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(SwapPhotos); 
        }
        if (actDirector.GetCurrentAct() == 2)
        {
            dialogueSystem.ReturnDialogueIndex.AddListener(Act2Photos);
        }
        dialogueSystem.DialogueEndEvent.AddListener(MarkComplete);
    }

    public void RemoveListeners()
    {
        if (actDirector.GetCurrentAct() == 1)
        {
            dialogueSystem.DialogueImpactfulChoiceEvent.RemoveListener(SwapPhotos);            
        }
        if (actDirector.GetCurrentAct() == 2)
        {
            dialogueSystem.ReturnDialogueIndex.RemoveListener(Act2Photos);
        }
        dialogueSystem.DialogueEndEvent.RemoveListener(MarkComplete);
    }

    public void ShowPhotos()
    {
        if (actDirector.GetCurrentAct() == 1)
        {
            photo.SetActive(true);
        }
        else
        {
            photo3.SetActive(true);
        }
    }
    
    void SwapPhotos(int doSwap)
    {
        if (doSwap == 0 && actDirector.GetCurrentAct() == 1)
        {
            photo.SetActive(false);
            photo2.SetActive(true);
        } else if (doSwap == 0 && actDirector.GetCurrentAct() == 2)
        {
            photo3.SetActive(false);
            photo4.SetActive(true);
        }
    }

    void MarkComplete()
    {
        actDirector.MarkTaskAsDone("Social");
        TaskText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
        ChoiceBasedDialogueScript.MarkDialogueSeen();
        RemoveListeners();
    }

    void Act2Photos(int currentLine)
    {
        if (currentLine == 2)
        {
            SwapPhotos(0);
        }
    }
}
