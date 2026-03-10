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
       story.branching = new BranchingDialogue[25];
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
        dialogueQs.Add("Click to continue)");
        dialogueResponses.Add("");
        dialogueNextSteps.Add(1)
        **/
        
        // story elements go here
        //in branching dialogue, the playerName is used as the first line of text
        story._playerName = "Say cheese!";
        story._NPCName = "LYBL";

        //add the lines
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("You're looking, um, good..?");
        dialogueNextSteps.Add(1);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("But we can do better! Just give me a moment...");
        dialogueNextSteps.Add(2);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Here! How does this look?");
        dialogueNextSteps.Add(3);
        
        dialogueQs.Add("Who's the woman?");
        dialogueResponses.Add("It's your girlfriend! Isn't she beautiful?");
        dialogueNextSteps.Add(4);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Anyway the image looks good right? I can send it to her?");
        dialogueNextSteps.Add(5);
        
        //branch
        dialogueQs.Add("Looks good to me.");
        dialogueResponses.Add("Oh good, I was a little worried...");
        dialogueNextSteps.Add(6);
        dialogueQs.Add("That's not my girlfriend...");
        dialogueResponses.Add("Oh.");
        dialogueNextSteps.Add(16);
        
        dialogueQs.Add("Why would you be worried?");
        dialogueResponses.Add("I wasn't entirely sure...");
        dialogueNextSteps.Add(7);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Anyway, we can cross it off the list!");
        dialogueNextSteps.Add(8);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Now, what else do you-");
        dialogueNextSteps.Add(9);
        
        dialogueQs.Add("What's wrong?");
        dialogueResponses.Add("So um... turns out that wasn't her in the photo.");
        dialogueNextSteps.Add(10);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("And she wants to know who the other woman is.");
        dialogueNextSteps.Add(11);
        
        dialogueQs.Add("You've got to be kidding me... let me talk to her.");
        dialogueResponses.Add("No, it's fine! I'm handling it-");
        dialogueNextSteps.Add(12);
        
        dialogueQs.Add("Lybl...");
        dialogueResponses.Add("I've um. Made everything worse.");
        dialogueNextSteps.Add(13);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("She says it's over between the two of you.");
        dialogueNextSteps.Add(14);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("... so I may have messed this up, but you still have things I can help with right?");
        dialogueNextSteps.Add(15);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Let's go do those!");
        dialogueNextSteps.Add(-1);
        
        dialogueQs.Add("What do you mean, \"Oh\"?");
        dialogueResponses.Add("I already sent the photo to her.");
        dialogueNextSteps.Add(17);
        
        dialogueQs.Add("What? Why?");
        dialogueResponses.Add("You said I could!");
        dialogueNextSteps.Add(18);
        
        dialogueQs.Add("No I didn't?");
        dialogueResponses.Add("Y-you did though! You said \"I can send it to her\"!");
        dialogueNextSteps.Add(19);
        
        dialogueQs.Add("You said that.");
        dialogueResponses.Add("Oh. Oh no.");
        dialogueNextSteps.Add(20);
        
        dialogueQs.Add("What now?");
        dialogueResponses.Add("She... she wants to break up.");
        dialogueNextSteps.Add(21);
        
        dialogueQs.Add("You're joking. Let me talk to her.");
        dialogueResponses.Add("No no, I can handle this!");
        dialogueNextSteps.Add(22);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("That's why I'm here, right? To help you Live Your Best Life(tm)?");
        dialogueNextSteps.Add(23);
        
        dialogueQs.Add("...");
        dialogueResponses.Add("Anyway... anything else I can help with?");
        dialogueNextSteps.Add(-1);
        
        // add elements to the story
        for (int i = 0; i < dialogueQs.Count; i++)
        {
            story.branching[i]._Question[0] = dialogueQs[i];
            story.branching[i]._NpcResponses[0] = dialogueResponses[i];
            story.branching[i].nextStepIndices[0] = dialogueNextSteps[i];
        }
        
        // create the asset
        string path = "Assets/Dialogue/story.asset";
        UnityEditor.AssetDatabase.CreateAsset(story, path);
        
    }
}
