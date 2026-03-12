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
       story.branching = new BranchingDialogue[29];
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
        story._playerName = "Another essay? Didn't we write one yesterday?";
        story._NPCName = "LYBL";
        
        // add the lines
        dialogueQs.Add("It's for my Latin Language class.");
        dialogueResponses.Add("Oh I know Latin! Let me write this!");
        dialogueNextSteps.Add(1);
        
        dialogueQs.Add("Actually, I think I should write it.");
        dialogueResponses.Add("Please?");
        dialogueNextSteps.Add(2);
        
        dialogueQs.Add("It's worth 30% of my grade, and it won't take that long.");
        dialogueResponses.Add("Pretty please?");
        dialogueNextSteps.Add(3);
        
        dialogueQs.Add("Fine. You can write one, but don't submit it!");
        dialogueResponses.Add("Yippee! I won't let you down!");
        dialogueNextSteps.Add(4);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("This looks good right? I can go submit it?");
        dialogueNextSteps.Add(5);
        
        //branch
        dialogueQs.Add("Actually, this looks pretty good.");
        dialogueResponses.Add("Woo!");
        dialogueNextSteps.Add(6);
        dialogueQs.Add("No, I can see several issues.");
        dialogueResponses.Add("W-what kind of issues?");
        dialogueNextSteps.Add(16);
        
        //left path
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Alright, I've gone ahead and submitted it, so now we can-");
        dialogueNextSteps.Add(7);
        
        dialogueQs.Add("What? I told you not to.");
        dialogueResponses.Add("... oh right. But it's fine right? You said it looked good.");
        dialogueNextSteps.Add(8);
        
        dialogueQs.Add("I did, but... why is it 111 pages long?");
        dialogueResponses.Add("Because that's the minimum page count!");
        dialogueNextSteps.Add(9);
        
        dialogueQs.Add("In Arabic or Roman numerals?");
        dialogueResponses.Add("What are Roman numerals?");
        dialogueNextSteps.Add(10);
        
        dialogueQs.Add("You don't know what Roman numerals are?");
        dialogueResponses.Add("No, there's nothing about them in my database...");
        dialogueNextSteps.Add(11);
        
        dialogueQs.Add("Okay, what did the submission page say exactly?");
        dialogueResponses.Add("\"Minimum length: III pages\"");
        dialogueNextSteps.Add(12);
        
        dialogueQs.Add("Okay, it's only 3 pages. I can rewrite what you wrote, and then resubmit it.");
        dialogueResponses.Add("It also says that you only get one attempt.");
        dialogueNextSteps.Add(13);
        
        dialogueQs.Add("Oh.");
        dialogueResponses.Add("I'm sure it's fine though! You just... put in extra effort!");
        dialogueNextSteps.Add(14);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("But uh, since it's been submitted, we can cross it off the list!");
        dialogueNextSteps.Add(15);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("Anything else that I can help with?");
        dialogueNextSteps.Add(-1);
        
        dialogueQs.Add("First, I said not to submit it.");
        dialogueResponses.Add("...");
        dialogueNextSteps.Add(17);
        
        dialogueQs.Add("Second, it's all Lorem Ipsum.");
        dialogueResponses.Add("...");
        dialogueNextSteps.Add(18);
        
        dialogueQs.Add("Third, it's 111 pages long!");
        dialogueResponses.Add("Was that not the requirement?");
        dialogueNextSteps.Add(19);
        
        dialogueQs.Add("Uh, no? Where did you even get that from?");
        dialogueResponses.Add("The submission page! It says \"Minimum length: III pages\"!");
        dialogueNextSteps.Add(20);
        
        dialogueQs.Add("I think you mean \"iii\". It's 3 in roman numerals.");
        dialogueResponses.Add("Ohh that makes more sense. But aside from that it looks good?");
        dialogueNextSteps.Add(21);
        
        dialogueQs.Add("Well no, it's still just Lorem Ipsum.");
        dialogueResponses.Add("It's Latin!");
        dialogueNextSteps.Add(22);
        
        dialogueQs.Add("It's gibberish.");
        dialogueResponses.Add("It's Latin gibberish!");
        dialogueNextSteps.Add(23);
        
        dialogueQs.Add("Well the good news is that you didn't submit this.");
        dialogueResponses.Add("About that...");
        dialogueNextSteps.Add(24);
        
        dialogueQs.Add("I thought I told you not to.");
        dialogueResponses.Add("I was so sure that my work was good...");
        dialogueNextSteps.Add(25);
        
        dialogueQs.Add("Whatever, I'll just write it and do a new submission.");
        dialogueResponses.Add("About that... we only had the one attempt.");
        dialogueNextSteps.Add(26);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("I'll... I'll do better on the next task, though!");
        dialogueNextSteps.Add(27);
        
        dialogueQs.Add("(Click to continue)");
        dialogueResponses.Add("There is more for me to help with right?");
        dialogueNextSteps.Add(-1);
        
        // add elements to the story
        for (int i = 0; i < dialogueQs.Count; i++)
        {
            story.branching[i]._Question[0] = dialogueQs[i];
            story.branching[i]._NpcResponses[0] = dialogueResponses[i];
            story.branching[i].nextStepIndices[0] = dialogueNextSteps[i];
        }
        
        // create the asset
        string path = "Assets/Dialogue/Act 2/Good Path/Essay/Act2EssayGood.asset";
        UnityEditor.AssetDatabase.CreateAsset(story, path);
        
    }
}
