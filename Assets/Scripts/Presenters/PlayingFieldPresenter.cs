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
        // hint: take into account the widths of cards, and desired padding,
        // and use those numbers to lay out the cards in a way that you want

        // Gutter: .02

        var CARD_WIDTH = .1f;
        var CARD_HEIGHT = .2f;
        var GUTTER = .04f;
        var CENTER_X = (CARD_WIDTH + GUTTER) * .5f;

        for (var i = 0; i < playingField.OpponentRow.Length; i++)
        {
            {
                var card = playingField.OpponentRow[i];
                if (card != null)
                {
                    // could be 2, could be 2.5
                    var HALF_CARDS = playingField.OpponentRow.Length * .5f;
                    var INITIAL_X = CENTER_X - (HALF_CARDS * CARD_WIDTH + GUTTER * Mathf.Floor(HALF_CARDS));
                    var FINAL_X = INITIAL_X + (CARD_WIDTH + GUTTER) * i;
                    var z = (GUTTER + CARD_HEIGHT) * .5f;
                    card.transform.position = transform.position + new Vector3(FINAL_X, 0f, z);
                    card.transform.rotation = Orientation.FACE_UP;
                }
            }

            {
                var card = playingField.PlayerRow[i];
                if (card != null)
                {
                    var HALF_CARDS = playingField.OpponentRow.Length * .5f;
                    var INITIAL_X = CENTER_X - (HALF_CARDS * CARD_WIDTH + GUTTER * Mathf.Floor(HALF_CARDS));
                    var FINAL_X = INITIAL_X + (CARD_WIDTH + GUTTER) * i;
                    var z = (GUTTER + CARD_HEIGHT) * .5f * -1f;
                    card.transform.position = transform.position + new Vector3(FINAL_X, 0f, z);
                    card.transform.rotation = Quaternion.AngleAxis(180f, Vector3.up) * Orientation.FACE_UP;
                }
            }
        }
    }

    // [ ] need a play action on the handpresenter, which moves cards from handpresenter to playingfieldpresenter
    // [ ] draw card outlines
}
