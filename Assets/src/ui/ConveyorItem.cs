using UnityEngine;
using UnityEngine.EventSystems;

public class ConveyorItem : MonoBehaviour, IPointerDownHandler {

	public delegate void PointerDownHandler();
	public PointerDownHandler onPointerDown;

	public void OnPointerDown(PointerEventData data) {
		onPointerDown();
	}
}
