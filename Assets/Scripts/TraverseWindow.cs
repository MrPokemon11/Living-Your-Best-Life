using UnityEngine;
using UnityEngine.UI;

public class TraverseWindow : MonoBehaviour
{
    
    public void TraverseToWindow(GameObject target)
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        target.SetActive(true);
    }

    public void TraverseWithoutClosingWindow(GameObject target)
    {
        target.SetActive(true);
    }
    
}
