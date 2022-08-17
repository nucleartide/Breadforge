using UnityEngine;
using System.Collections.Generic;

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
    CardOutlineFactory cardOutlineFactory;

    [SerializeField]
    [NotNull]
    Card testCard;

    [SerializeField]
    [NotNull]
    CardOutlinePresenter cardOutline;

    [SerializeField]
    [NotNull]
    CurrentlySelectedCard currentlySelectedCard;

    [SerializeField]
    [NotNull]
    HandPresenter handPresenter;

    [SerializeField]
    [NotNull]
    GameViewPresenter gameViewPresenter;

    CardOutlinePresenter[] opponentRowCardOutlines = new CardOutlinePresenter[4];
    CardOutlinePresenter[] playerRowCardOutlines = new CardOutlinePresenter[4];
    CardPresenter[] cardsInPlay = new CardPresenter[4];

    public bool IsInPlay(CardPresenter cardPresenter)
    {
        return System.Array.Exists(cardsInPlay, card => card == cardPresenter);
    }

    void Start()
    {
        for (var i = 0; i < 4; i++)
        {
            opponentRowCardOutlines[i] = cardOutlineFactory.Build(PlayingFieldSide.Enemy);
            playerRowCardOutlines[i] = cardOutlineFactory.Build(PlayingFieldSide.Player);
        }
    }

    /// <summary>
    /// Set a cardPresenter to the position of the cardOutlinePresenter.
    /// </summary>
    public void SetCard(CardPresenter card, CardOutlinePresenter cardOutline)
    {
        // Find the index of the card outline.
        var outlines = new List<CardOutlinePresenter>(playerRowCardOutlines);
        var index = System.Array.FindIndex(playerRowCardOutlines, outline => outline == cardOutline);
        if (index == -1)
            throw new System.Exception("Card outline wasn't found, this shouldn't happen.");

        // Set the card to the corresponding index.
        cardsInPlay[index] = card;
    }

    void UpdatePositionsAndRotations()
    { 
        var CARD_WIDTH = .1f;
        var CARD_HEIGHT = .2f;
        var GUTTER = .04f;
        var CENTER_X = (CARD_WIDTH + GUTTER) * .5f;

        for (var i = 0; i < playingField.OpponentRow.Length; i++)
        {
            {
                var card = opponentRowCardOutlines[i];
                if (card != null)
                {
                    var HALF_CARDS = playingField.OpponentRow.Length * .5f;
                    var INITIAL_X = CENTER_X - (HALF_CARDS * CARD_WIDTH + GUTTER * Mathf.Floor(HALF_CARDS));
                    var FINAL_X = INITIAL_X + (CARD_WIDTH + GUTTER) * i;
                    var z = (GUTTER + CARD_HEIGHT) * .5f;
                    card.transform.position = transform.position + new Vector3(FINAL_X, 0f, z);
                    card.transform.rotation = Orientation.FACE_UP;
                }
            }

            {
                var card = playerRowCardOutlines[i];
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

            {
                var card = cardsInPlay[i];
                if (card != null)
                {
                    var HALF_CARDS = playingField.OpponentRow.Length * .5f;
                    var INITIAL_X = CENTER_X - (HALF_CARDS * CARD_WIDTH + GUTTER * Mathf.Floor(HALF_CARDS));
                    var FINAL_X = INITIAL_X + (CARD_WIDTH + GUTTER) * i;
                    var z = (GUTTER + CARD_HEIGHT) * .5f * -1f;
                    card.transform.position = transform.position + new Vector3(FINAL_X, .01f, z);
                    card.transform.rotation = Quaternion.AngleAxis(180f, Vector3.up) * Orientation.FACE_UP;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            currentlySelectedCard.Clear();

        UpdatePositionsAndRotations();
    }
}
