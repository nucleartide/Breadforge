using System.Collections.Generic;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    List<CardPresenter> cards = new List<CardPresenter>();

    /// <summary>
    /// A child of the HandPresenter that determines the center point of the
    /// circle around which the hand's cards are positioned.
    /// </summary>
    [SerializeField]
    [NotNull]
    Transform radialPivot;

    void Update()
    {
        for (var i = 0; i < cards.Count; i++)
        {
            // Grab card reference.
            var card = cards[i];

            // Set position.
            var point = new Vector3(0f, .5f, i * .01f /* Small offset so cards don't overlap. */);
            var rotation = Quaternion.AngleAxis(10f * i - (cards.Count - 1) * 5f, Vector3.forward);
            var rotatedPoint = rotation * point;
            var worldSpacePoint = radialPivot.transform.TransformPoint(rotatedPoint);
            card.transform.position = worldSpacePoint;

            // Set rotation.
            card.transform.rotation = Quaternion.AngleAxis(10f * i - (cards.Count - 1) * 5f, transform.forward) * transform.rotation;
        }
    }

    public bool Remove(CardPresenter card)
    {
        return cards.Remove(card);
    }

    public bool Contains(CardPresenter card)
    {
        return cards.Contains(card);
    }
}
