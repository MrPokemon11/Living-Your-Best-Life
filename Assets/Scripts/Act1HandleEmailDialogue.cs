using UnityEngine;
using Dialogue;

public class Act1HandleEmailDialogue : MonoBehaviour
{
    [SerializeField] private DialogueSystem dialogue;

    [SerializeField]
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip audioClipNo;
    [SerializeField] private AudioClip audioClipYesNo;
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
        if (dialogue.GetBranchIndex() == 8)
        {
            audioSource.PlayOneShot(audioClipYesNo);
        } 
        else if (dialogue.GetBranchIndex() == 11)
        {
            audioSource.PlayOneShot(audioClipNo);
        }
    }
}
