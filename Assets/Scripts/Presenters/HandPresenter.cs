using System.Collections.Generic;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    public List<CardPresenter> Cards;
    public Camera Camera;

    /// <summary>
    /// A child of the HandPresenter that determines the center point of the
    /// circle around which the hand's cards are positioned.
    /// </summary>
    public Transform RadialPivot;

    void Update()
    {
        // transform.LookAt(Camera.transform);

        for (var i = 0; i < Cards.Count; i++)
        {
            // Grab card reference.
            var card = Cards[i];

            // Set position.
            var point = new Vector3(0f, .5f, i * .01f /* Small offset so cards don't overlap. */);
            var rotation = Quaternion.AngleAxis(10f * i - (Cards.Count - 1) * 5f, Vector3.forward);
            var rotatedPoint = rotation * point;
            var worldSpacePoint = RadialPivot.transform.TransformPoint(rotatedPoint);
            card.transform.position = worldSpacePoint;

            // Set rotation.
            card.transform.rotation = Quaternion.AngleAxis(10f * i - (Cards.Count - 1) * 5f, transform.forward) * transform.rotation;
        }
    }
}
