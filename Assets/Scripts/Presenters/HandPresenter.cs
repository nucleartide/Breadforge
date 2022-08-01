using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    public List<CardPresenter> cards;

    void Update()
    {
        // update the positions of the cards.
        for (var i = 0; i < cards.Count; i++)
        {
            var currentCard = cards[i];
            var offset = new Vector3(1.2f * i, 0f, 0f);
            currentCard.transform.position = transform.position + offset;
		}
    }

    // [ ] render the hands' cards in front of the camera
    // [ ] listen to user input, and trigger a draw action
}
