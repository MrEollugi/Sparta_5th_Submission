using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform joystickBG;
    [SerializeField] private RectTransform joystickHandle;

    public Vector2 InputDirection { get; private set; }

    private float radius;

    private void Start()
    {
        radius = joystickBG.sizeDelta.x * 0.5f;
        InputDirection = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG, eventData.position, eventData.pressEventCamera, out pos);

        pos = Vector2.ClampMagnitude(pos, radius);
        joystickHandle.anchoredPosition = pos;

        InputDirection = pos / radius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero;
        InputDirection = Vector2.zero;
    }
}
