using UnityEngine;
using UnityEngine.Rendering;

public class TurnOffElectronics : MonoBehaviour
{
    [SerializeField] private Material lightMaterial;

    [SerializeField] private GameObject lightbulbLight;
    [SerializeField] private GameObject computerBlackScreen;
    [SerializeField] private GameObject aboveComputerLight;

    [SerializeField] private GameObject LyblWindow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOff()
    {
        lightbulbLight.SetActive(false);
        aboveComputerLight.SetActive(false);
        LyblWindow.SetActive(false);
        computerBlackScreen.SetActive(true);
        lightMaterial.SetFloat("_IsLightOn", 0.0f);

    }

    public void TurnOn()
    {
        lightbulbLight.SetActive(true);
        aboveComputerLight.SetActive(true);
        LyblWindow.SetActive(true);
        computerBlackScreen.SetActive(false);
        lightMaterial.SetFloat("_IsLightOn", 1.0f);
    }
}
