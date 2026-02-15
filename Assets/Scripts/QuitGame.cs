using UnityEngine;
using TMPro;

public class QuitGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI confirmText;
    bool confirmTextShown = false;
    
    public void Quit()
    {
        if (confirmTextShown)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif            
        }
        else
        {
            confirmText.text = "Quit game?";
            confirmTextShown = true;
        }

    }
}
