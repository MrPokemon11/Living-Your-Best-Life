using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// initial code copied from https://www.youtube.com/watch?v=dPdmJ0RDLSI
public class DynamicUIBase : MonoBehaviour
{
    [SerializeField] RectTransform CanvasTransform;
    [SerializeField] GraphicRaycaster Raycaster;

    private List<GameObject> DragTargets = new();

    public void OnCursorInput(Vector2 InNormalisedPosition)
    {
        //get the input position of the mouse in canvas space
        Vector3 InputPosition = new Vector3(CanvasTransform.sizeDelta.x * InNormalisedPosition.x,
                                            CanvasTransform.sizeDelta.y * InNormalisedPosition.y, 0);
        
        //construct a pointer event
        PointerEventData PointerEvent = new PointerEventData(EventSystem.current);
        PointerEvent.position = InputPosition;
        
        // determine what UI objects were hit by the raycast
        List<RaycastResult> Results = new();
        Raycaster.Raycast(PointerEvent, Results);
        
#if ENABLE_LEGACY_INPUT_MANAGER
        bool bMouseDownThisFrame = Input.GetMouseButtonDown(0);
        bool bMouseUpThisFrame = Input.GetMouseButtonUp(0);
        bool bIsMouseDown = Input.GetMouseButton(0);
#else
        bool bMouseDownThisFrame = UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame;
        bool bMouseUpThisFrame = UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame;
        bool bIsMouseDown = UnityEngine.InputSystem.Mouse.current.leftButton.isPressed;        
#endif // ENABLE_LEGACY_INPUT_MANAGER
        
        // if the mouse button was released this frame, stop dragging
        if (bMouseDownThisFrame)
        {
            foreach (var Target in DragTargets)
            {
                if (ExecuteEvents.Execute(Target, PointerEvent, ExecuteEvents.endDragHandler))
                {
                    break;
                }
            }
            DragTargets.Clear();
        }
        
        //Process hit results
        foreach (var Result in Results)
        {
         //create new event data
            PointerEventData PointerEventForResult = new PointerEventData(EventSystem.current);
            PointerEventForResult.position = InputPosition;
            PointerEventForResult.pointerCurrentRaycast = Result;
            PointerEventForResult.pointerPressRaycast = Result;
            
            // is the (left) button down?
            if (bIsMouseDown)
            {
                PointerEventForResult.button = PointerEventData.InputButton.Left;
            }

            //retrieve a slider if hit
            var HitSlider = Result.gameObject.GetComponentInParent<Slider>();
            
            //new drag targets?
            if (bMouseDownThisFrame)
            {
                if (ExecuteEvents.Execute(Result.gameObject, PointerEventForResult, ExecuteEvents.beginDragHandler))
                {
                    DragTargets.Add(Result.gameObject);
                }

                if (HitSlider != null)
                {
                    HitSlider.OnInitializePotentialDrag(PointerEventForResult);

                    if (!DragTargets.Contains(Result.gameObject))
                    {
                        DragTargets.Add(Result.gameObject);
                    }
                    
                    
                }
            } // need to update current drag targets?
            else if (DragTargets.Contains(Result.gameObject))
            {
                PointerEventForResult.dragging = true;
                ExecuteEvents.Execute(Result.gameObject, PointerEventForResult, ExecuteEvents.dragHandler);

                if (HitSlider != null)
                {
                    HitSlider.OnDrag(PointerEventForResult);
                }
            }

            if (bMouseDownThisFrame)
            {
                if (ExecuteEvents.Execute(Result.gameObject, PointerEventForResult, ExecuteEvents.pointerClickHandler))
                {
                    break;
                }
            }
            else if (bMouseUpThisFrame)
            {
                bool bDidRun = false;
                bDidRun |= ExecuteEvents.Execute(Result.gameObject, PointerEventForResult, ExecuteEvents.pointerUpHandler);
                bDidRun |= ExecuteEvents.Execute(Result.gameObject, PointerEventForResult, ExecuteEvents.pointerClickHandler);

                if (bDidRun)
                {
                    break;
                }
            }
        }
    }
}
