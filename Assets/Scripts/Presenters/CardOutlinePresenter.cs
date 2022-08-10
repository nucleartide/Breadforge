using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardOutlinePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [NotNull]
    [Tooltip("When enabled, makes it appear as if the card outline has a glow.")]
    GameObject glow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
