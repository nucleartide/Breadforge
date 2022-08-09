using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CurrentlySelectedCard : ScriptableObject
{
    /// <summary>
    /// The currently selected card that is ready to be played on the playing field.
    /// </summary>
    [System.NonSerialized]
    public CardPresenter Card;
}
