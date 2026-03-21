using UnityEngine;
using TMPro;

public class HandleBossEmail : MonoBehaviour
{
    [SerializeField] private ActDirector actDirector;

    [SerializeField] private GameObject act1email;
    [SerializeField] private GameObject act2email;
    [SerializeField] private TextMeshProUGUI EmailText;
    
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

        if (actDirector.GetCurrentAct() == 1)
        {
            EmailText.text = "Sender: Boss\nSubject: URGENT\n[Click to read more]";
        }
        else
        {
            EmailText.text = "Sender: Boss\nSubject: Re: Re: URGENT\n[Click to read more]";
        }
    }
}
