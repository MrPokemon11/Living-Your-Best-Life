using UnityEngine;

public class FinaleToggle : MonoBehaviour
{
    private FinaleScreen finaleScreen;
    bool listenerActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finaleScreen = GetComponentInParent<FinaleScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!listenerActive)
        {
            listenerActive = true;
            finaleScreen.toggleKids.AddListener(Toggle);
            Toggle();
        }
    }

    void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
