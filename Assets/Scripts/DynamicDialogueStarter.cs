using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using Dialogue;
using Unity.VisualScripting;
using UnityEngine.ResourceManagement.AsyncOperations;

//This code references code made by Azukie on Itch. All code in this file is created by me (Devon)

public class DynamicDialogueStarter : MonoBehaviour
{
    [Header("Dialogue Managers")]
    [SerializeField] private GameObject playerDialogueManager;
    [SerializeField] private GameObject lyblDialogueManager;
    private DialogueSystem playerDialogueSystem;
    private DialogueSystem lyblDialogueSystem;

    [Header("Act Director")]
    [SerializeField] private GameObject actDirector;
    private ActDirector actDirectorScript;
    
    //feels dirty to do it this way, but it works, so w/e    
    [Header("Story Data")]
    [SerializeField] private Story dialogueLyblAct1;
    [SerializeField] private Story dialoguePlayerAct1;
    [SerializeField] private Story dialogueLyblAct2;
    [SerializeField] private Story dialoguePlayerAct2;
    [SerializeField] private Story dialogueLyblAct3;
    [SerializeField] private Story dialoguePlayerAct3;
    private Story currentDialoguePlayer;
    private Story currentDialogueLybl;
    
    //make sure that dialogue isn't shown twice
    private bool Act1DialogueSeen = false;
    private bool Act2DialogueSeen = false;
    private bool Act3DialogueSeen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDialogueSystem = playerDialogueManager.GetComponent<DialogueSystem>();
        lyblDialogueSystem = lyblDialogueManager.GetComponent<DialogueSystem>();
        actDirectorScript = actDirector.GetComponent<ActDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogueByAct()
    {
        bool canShowDialogue = true;
        // load the correct story
        int currentAct = actDirectorScript.GetCurrentAct();
        if (currentAct == 1 && dialoguePlayerAct1 != null && dialogueLyblAct1 != null && Act1DialogueSeen == false)
        {
            currentDialoguePlayer = dialoguePlayerAct1;
            currentDialogueLybl = dialogueLyblAct1;
            Act1DialogueSeen = true;
        } else if (currentAct == 2 && dialoguePlayerAct2 != null && dialogueLyblAct2 != null && Act2DialogueSeen == false)
        {
            currentDialoguePlayer = dialoguePlayerAct2;
            currentDialogueLybl = dialogueLyblAct2;
            Act2DialogueSeen = true;
        }
        else if (currentAct == 3 && dialoguePlayerAct3 != null && dialogueLyblAct3 != null && Act3DialogueSeen == false)
        {
            currentDialoguePlayer = dialoguePlayerAct3;
            currentDialogueLybl = dialogueLyblAct3;
            Act3DialogueSeen = true;
        }
        else
        {
            canShowDialogue = false;
        }
        
        // start the dialogue, if it hasn't already been seen
        if (canShowDialogue)
        {
            playerDialogueSystem.StartDialogue(currentDialoguePlayer);
            lyblDialogueSystem.StartDialogue(currentDialogueLybl);            
        }
    }
    
    //start dialogue based on the current act and the dialogue's context
    public void StartDialogueThroughContext(string ActNum, string Context)
    {
        //build the shared portions of the filepath
        string sharedFilePath = "Assets/Dialogue/Act " + ActNum + "/" + Context + "/" + "Act " + ActNum + "_" + Context + "_";
        
        //add the unique parts of the file paths
        string filePathPlayer = sharedFilePath + "Player.asset";
        string filePathLybl = sharedFilePath + "LYBL.asset";
        
        //load the Story files
        //AsyncOperationHandle<Story> playerStory = Addressables.LoadAssetAsync<Story>(filePathPlayer);
        //AsyncOperationHandle<Story> lyblStory = Addressables.LoadAssetAsync<Story>(filePathLybl);
        
        //Story playerTempStory = playerStory.Result;
        //Story lyblTempStory = lyblStory.Result;
        
        //start the dialogue

    }
}
