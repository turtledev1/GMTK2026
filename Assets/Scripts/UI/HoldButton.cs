using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public System.Action OnHoldStart;
    public System.Action OnHoldEnd;

    public void OnPointerDown(PointerEventData eventData) {
        OnHoldStart?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData) {
        OnHoldEnd?.Invoke();
    }
}
