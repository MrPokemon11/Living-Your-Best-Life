using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinaleScreen : MonoBehaviour
{
    [SerializeField] private GameObject finaleScreen;
    private Image screen;
    bool fading = false;
    public float fadeSpeed = 5f;

    public UnityEvent toggleKids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toggleKids == null)
        {
            toggleKids = new UnityEvent();
        }
        screen = finaleScreen.GetComponent<Image>();
        finaleScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            screen.color += new Color(0.0f, 0.0f, 0.0f, fadeSpeed);
            if (screen.color.a >= 100f)
            {
                fading = false;
                //toggleKids.Invoke();
            }
        }
    }

    public void FadeIn()
    {
        finaleScreen.SetActive(true);
        fading = true;
    }

    public void GoAway()
    {
        screen.color = new Color(0f, 0f, 0f, 0f);
        finaleScreen.SetActive(false);
        //toggleKids.Invoke();
    }
}
