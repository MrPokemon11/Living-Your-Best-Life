using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float finalFoV;
    [SerializeField] private float waitTime;
    private float currentFoV;
    Camera cam;
    bool zoomingIn = false;

    //do things before other stuff happens
    void Start()
    {
        cam = GetComponent<Camera>();
        currentFoV = cam.fieldOfView;
        Invoke (nameof(ZoomIn), waitTime);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (zoomingIn)
        {
            cam.fieldOfView -= zoomSpeed * Time.deltaTime;
            if (cam.fieldOfView <= finalFoV)
            {
                cam.fieldOfView = finalFoV;
                zoomingIn = false;
            }
        }
    }

    public void ResetFoV()
    {
        cam.fieldOfView = currentFoV;
    }

    public void ZoomIn()
    {
        zoomingIn = true;
    }
    
}
