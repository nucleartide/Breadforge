using UnityEngine;

public class PlayingFieldPresenter : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    PlayingField playingField;

    [SerializeField]
    [NotNull]
    CardFactory cardFactory;

    [SerializeField]
    [NotNull]
    Card testCard;

    void Start()
    {
        // Instantiate 2 cards, and place them in random locations in the opponent row and player row
        var card1 = cardFactory.Build(testCard);
        var card2 = cardFactory.Build(testCard);
        Debug.Log("instantiated 2 cards");

        // place card 1 in a random location in opponent row
        for (var i = 0; i < 4; i++)
        {
            playingField.OpponentRow[i] = cardFactory.Build(testCard);
			playingField.PlayerRow[i] = cardFactory.Build(testCard);
		}
    }

    // update the positions of the cards in the playing Field
    void Update()
    {
        // TODO: need to position the cards centered on the table,
        // hint: take into account the padding and widths of cards

        var offset = new Vector3(playingField.OpponentRow.Length * .5f * (.15f + .1f), 0f, 0f);

        for (var i = 0; i < playingField.OpponentRow.Length; i++)
        {
            var card = playingField.OpponentRow[i];
            if (card != null)
            { 
			    card.transform.position = transform.position + new Vector3(i * .15f, 0f, -.13f) - offset;
                card.transform.rotation = Orientation.FACE_UP;
		    }
		}

        for (var i = 0; i < playingField.PlayerRow.Length; i++)
        { 
            var card = playingField.PlayerRow[i];
            if (card != null)
            { 
			    card.transform.position = transform.position + new Vector3(i * .15f, 0f, .13f) - offset;
                card.transform.rotation = Orientation.FACE_UP;
		    }
		}
    }

    // [ ] need a play action on the handpresenter, which moves cards from handpresenter to playingfieldpresenter
    // [ ] draw card outlines
}
