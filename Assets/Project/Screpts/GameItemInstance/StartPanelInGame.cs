using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanelInGame : MonoBehaviour, IPointerClickHandler
{
    public event Action OnStartPanelClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnStartPanelClicked?.Invoke();
    }
}