using UnityEngine;
using Dialogue;

public class Act1HandleEmailDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSystem dialogue;

    [SerializeField]
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        if (dialogue.GetBranchIndex() == 0)
        {
            
        }
    }
}
