using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    public List<CardPresenter> cards;
    public new Camera camera;
    public Transform radialPivot;

    void Update()
    {
        transform.LookAt(camera.transform);


        // update the positions of the cards.
        for (var i = 0; i < cards.Count; i++)
        {
			var start = new Vector3(0f, .5f, i * .01f /* small offset so cards don't overlap */);
			var rotation = Quaternion.AngleAxis(10f * i - (cards.Count - 1) * 5f, Vector3.forward);
			var rotatedStart = rotation * start;
			var worldSpaceStart = radialPivot.transform.TransformPoint(rotatedStart);

            // var offset = new Vector3(.12f * i - (cards.Count - 1) * .06f, 0f, 0f);
            // currentCard.transform.position = transform.position + offset;
            var currentCard = cards[i];
            currentCard.transform.position = worldSpaceStart;
            currentCard.transform.rotation =  Quaternion.AngleAxis(10f * i - (cards.Count - 1) * 5f, transform.forward) * transform.rotation;
        }

        //var yOffset = Mathf.Abs(radialPivot.localPosition.y);
    }

    // [ ] render the hands' cards in front of the camera
    // [ ] listen to user input, and trigger a draw action
}
