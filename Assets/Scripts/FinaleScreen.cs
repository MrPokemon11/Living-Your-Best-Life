using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinaleScreen : MonoBehaviour
{
    private Image screen;
    bool fading = false;
    float fadeSpeed = 5f;

    public UnityEvent toggleKids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toggleKids == null)
        {
            toggleKids = new UnityEvent();
        }
        screen = GetComponent<Image>();
        screen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            screen.gameObject.SetActive(true);
            screen.color += new Color(0.0f, 0.0f, 0.0f, 1f * Time.deltaTime * fadeSpeed);
            if (screen.color.a >= 100f)
            {
                fading = false;
                toggleKids.Invoke();
            }
        }
    }

    public void FadeIn()
    {
        fading = true;
    }

    public void GoAway()
    {
        screen.color = new Color(0f, 0f, 0f, 0f);
        screen.gameObject.SetActive(false);
        toggleKids.Invoke();
    }
}
