using UnityEngine;

public class HandleBossEmail : MonoBehaviour
{
    [SerializeField] private ActDirector actDirector;

    [SerializeField] private GameObject act1email;
    [SerializeField] private GameObject act2email;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendEmail()
    {
        if (actDirector.GetCurrentAct() == 1)
        {
            act1email.SetActive(true);
            act2email.SetActive(false);
        }
        else
        {
            act1email.SetActive(false);
            act2email.SetActive(true);
        }
    }
}
