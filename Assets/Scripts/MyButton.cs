using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class MyButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		this.value = 1;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		this.value = 0;
	}
	public int value;
}
