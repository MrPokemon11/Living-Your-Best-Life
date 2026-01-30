using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New Story", menuName = "Dialogue/Story")]
    public class Story : ScriptableObject
    {
        [SerializeField] public string _playerName;
        [SerializeField] public string _NPCName;

        public enum DialogueType { Branching, Linear }
        [SerializeField] public DialogueType _dialogueType;

        [SerializeField] public bool _startDialogue;

        [SerializeField] public LinearDialogue linear;
        [SerializeField] public BranchingDialogue[] branching;
        
    }

    [Serializable]
    public class LinearDialogue
    {
        [SerializeField] public string[] _pharases;
        [SerializeField] public string[] _responses;
        [SerializeField] public bool _haveResponses;
    }

    [Serializable]
    public class BranchingDialogue
    {
        [SerializeField] public string[] _Question;
        [SerializeField] public string[] _NpcResponses;
        [SerializeField] public int[] nextStepIndices;
    }
}
