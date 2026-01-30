using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class StartDialogue : MonoBehaviour
    {
        public DialogueSystem dialogueManager;
        public Story storyToPlay;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueManager.StartDialogue(storyToPlay);
            }
        }

    }
}
