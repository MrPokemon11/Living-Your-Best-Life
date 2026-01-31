using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

// inital code copied from https://www.youtube.com/watch?v=dPdmJ0RDLSI

public class DynamicUI_DisplayScreen : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 5.0f;
    [SerializeField] UnityEvent<Vector2> OnCursorInput = new();

    //called once per frame, yada yada
    private void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        Vector3 MousePosition = Input.mousePosition;
#else
        Vector3 MousePosition = UnityEngine.Input.Mouse.current.position.ReadValue();
#endif //ENABLE_LEGACY_INPUT_MANAGER
        
        //construct ray from mouse position
        Ray MouseRay = Camera.main.ScreenPointToRay(MousePosition);
        
        //perform raycast
        RaycastHit HitResult;
        if (Physics.Raycast(MouseRay, out HitResult, RaycastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            //ignore the cast if it isn't this object
            if (HitResult.collider.gameObject != gameObject)
            {
                return;
            }
            
            OnCursorInput.Invoke(HitResult.textureCoord);
        }
    }
}
