using UnityEngine;
using UnityEngine.UI;

public class FinaleScreen : MonoBehaviour
{
    private Image screen;
    bool fading = false;
    float fadeSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen = GetComponent<Image>();
        screen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            screen.color += new Color(0.0f, 0.0f, 0.0f, 1f * Time.deltaTime * fadeSpeed);
            if (screen.color.a >= 100f)
            {
                fading = false;
            }
        }
    }

    public void FadeIn()
    {
        fading = true;
    }
}
