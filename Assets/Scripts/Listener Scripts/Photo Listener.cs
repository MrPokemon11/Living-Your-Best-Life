using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PhotoListener : MonoBehaviour
{
    [Header("Photos")]
    [SerializeField] GameObject photo;
    [SerializeField] GameObject photo2;
    
    [Header("Dialogue")]
    [SerializeField] private GameObject DialogueManager;

    [SerializeField] private GameObject ActManager;
    
    DialogueSystem dialogueSystem;
    ActDirector actDirector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueSystem = DialogueManager.GetComponent<DialogueSystem>();
        actDirector = ActManager.GetComponent<ActDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateListeners()
    {
        dialogueSystem.DialogueImpactfulChoiceEvent.AddListener(SwapPhotos);
        dialogueSystem.DialogueEndEvent.AddListener(MarkComplete);
    }

    public void RemoveListeners()
    {
        dialogueSystem.DialogueImpactfulChoiceEvent.RemoveListener(SwapPhotos);
        dialogueSystem.DialogueEndEvent.RemoveListener(MarkComplete);
    }

    void SwapPhotos(int doSwap)
    {
        if (doSwap == 0)
        {
            photo.SetActive(false);
            photo2.SetActive(true);
        }
    }

    void MarkComplete()
    {
        actDirector.MarkTaskAsDone("Social");
        RemoveListeners();
    }
}
