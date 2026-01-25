using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class ToggleTextAndBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleTextAndBox(GameObject bg, GameObject textbox)
    {
        bg.SetActive(!bg.activeSelf);
        textbox.SetActive(!textbox.activeSelf);
    }

}
