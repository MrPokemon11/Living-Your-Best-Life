using UnityEngine;

public class Act3TaskReveal : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameObject GameManager;
    private ActDirector actDirector;

    [Header("Task Text")]
    [SerializeField] private GameObject TaskTextGood;
    [SerializeField] private GameObject TaskTextBad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actDirector = GameManager.GetComponent<ActDirector>();
        TaskTextGood.SetActive(false);
        TaskTextBad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealTaskText()
    {
        if (actDirector.GetIsGoodRoute())
        {
            TaskTextGood.SetActive(true);
        }
        else
        {
            TaskTextBad.SetActive(true);
        }
        
    }
}
