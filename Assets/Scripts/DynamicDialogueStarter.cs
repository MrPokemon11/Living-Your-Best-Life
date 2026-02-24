using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using Dialogue;
using UnityEngine.ResourceManagement.AsyncOperations;

//This code references code made by Azukie on Itch. All code in this file is created by me (Devon)

public class DynamicDialogueStarter : MonoBehaviour
{
    [SerializeField] private GameObject playerDialogueManager;
    [SerializeField] private GameObject lyblDialogueManager;
    private DialogueSystem playerDialogueSystem;
    private DialogueSystem lyblDialogueSystem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDialogueSystem = playerDialogueManager.GetComponent<DialogueSystem>();
        lyblDialogueSystem = lyblDialogueManager.GetComponent<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        AsyncOperationHandle<Story> playerStory = Addressables.LoadAssetAsync<Story>(filePathPlayer);
        AsyncOperationHandle<Story> lyblStory = Addressables.LoadAssetAsync<Story>(filePathLybl);
        
        Story playerTempStory = playerStory.Result;
        Story lyblTempStory = lyblStory.Result;
        
        //start the dialogue
        playerDialogueSystem.StartDialogue(playerTempStory);
        lyblDialogueSystem.StartDialogue(lyblTempStory);
    }
}
