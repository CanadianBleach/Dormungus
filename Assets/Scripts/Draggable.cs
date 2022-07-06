using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : EventTrigger
{
    private bool dragging;
    Vector2 offset;

    public void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        offset = Input.mousePosition - transform.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}