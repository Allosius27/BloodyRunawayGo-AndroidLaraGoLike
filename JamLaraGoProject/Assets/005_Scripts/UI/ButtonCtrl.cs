using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCtrl : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private AllosiusDev.AudioData sfxButtonHighlightedObject;
    [SerializeField] private AllosiusDev.AudioData sfxButtonClickedObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        AllosiusDev.AudioManager.Play(sfxButtonHighlightedObject.sound);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down");
        AllosiusDev.AudioManager.Play(sfxButtonClickedObject.sound);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
    }

    private void Start()
    {
        
    }
}
