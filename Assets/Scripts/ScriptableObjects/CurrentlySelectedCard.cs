using UnityEngine;

[CreateAssetMenu]
public class CurrentlySelectedCard : ScriptableObject
{
    /// <summary>
    /// The currently selected card that is ready to be played on the playing field.
    /// </summary>
    [field: System.NonSerialized]
    public CardPresenter Card
    {
        get;
        private set;
    }

    public bool IsPresent
    { 
        get
        {
            return Card != null;
		}
    }

    public void Clear()
    { 
        Card = null;
    }
}
