using UnityEngine;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject ActDirector;
    ActDirector ActDirectorScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       ActDirectorScript = ActDirector.GetComponent<ActDirector>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        ActDirectorScript.StartAct();
        MainCamera.GetComponent<ZoomCamera>().StartGameZoom();
    }
}
