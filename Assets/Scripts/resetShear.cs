using UnityEngine;
using UnityEngine.UI;

public class resetShear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetShear()
    {
        gameObject.GetComponent<Image>().material.SetFloat("_Shear_Intensity", 0f);
    }
}
