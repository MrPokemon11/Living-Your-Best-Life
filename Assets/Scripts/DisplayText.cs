using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DisplayText : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // displays the given text in the given TMP_Text object
    public void DisplayTextOnScreen(Text textbox, string input)
    {
        textbox.text = input;
    }
}
