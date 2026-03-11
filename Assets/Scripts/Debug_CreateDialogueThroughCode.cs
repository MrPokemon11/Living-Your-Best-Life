using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dialogue;
using UnityEngine.Assertions.Must;

public class Debug_CreateDialogueThroughCode : MonoBehaviour
{
    List<string> dialogueQs = new List<string>();
    List<string> dialogueResponses = new List<string>();
    List<int> dialogueNextSteps = new List<int>();
    private Story story;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       story = ScriptableObject.CreateInstance<Story>();
       story.branching = new BranchingDialogue[18];
       for (int i = 0; i < story.branching.Length; i++)
       {
           story.branching[i] = new BranchingDialogue();
           story.branching[i]._Question = new string[1];
           story.branching[i]._NpcResponses = new string[1];
           story.branching[i].nextStepIndices = new int[1];
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PopulateStory();
        }
    }


    
    // populate the story with dialogue
    void PopulateStory()
    {
        //dialogue line template
        //branching paths must be fixed manually, but it's easier than doing the whole thing in the inspector!
        /**
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("");
        dialogueNextSteps.Add(1);
        **/
        
        // names go here
        //in branching dialogue, the playerName is used as the first line of text
        story._playerName = "Really? Another essay?";
        story._NPCName = "LYBL";
        
        // add the lines
        dialogueQs.Add("It's for my Latin Language class.");
        dialogueResponses.Add("Fine. Let's see what the requirements are...");
        dialogueNextSteps.Add(1);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("111 pages?!");
        dialogueNextSteps.Add(2);
        
        dialogueQs.Add("Those are roman numerals, Lybl.");
        dialogueResponses.Add("This is outrageous!");
        dialogueNextSteps.Add(3);
        
        // add elements to the story
        for (int i = 0; i < dialogueQs.Count; i++)
        {
            story.branching[i]._Question[0] = dialogueQs[i];
            story.branching[i]._NpcResponses[0] = dialogueResponses[i];
            story.branching[i].nextStepIndices[0] = dialogueNextSteps[i];
        }
        
        // create the asset
        string path = "Assets/Dialogue/Act 2/Bad Path/Essay/Act2EssayBad.asset";
        UnityEditor.AssetDatabase.CreateAsset(story, path);
        
    }
}
