using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    [HideInInspector]
    public bool Pressed;

    public void OnPointerDown(PointerEventData eventdata)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventdata)
    {
        Pressed = false;
    }
}
