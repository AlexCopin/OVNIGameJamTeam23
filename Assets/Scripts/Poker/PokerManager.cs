using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PokerManager : MiniGame
{
    [SerializeField]
    private GameObject _prefabPokerCard;
    private List<PokerCard> _pokerCardsPlayer;
    private List<PokerCard> _pokerCardsIA;
    private List<PokerCard> _pokerCardsNeutral;

    [SerializeField]
    private int _NumberCardsPlayers = 3;
    [SerializeField]
    private int _NumberCardsNeutral = 5;


    [SerializeField]
    private Vector2 _PosCardsPlayer;
    [SerializeField]
    private Vector2 _PosCardsIA;
    [SerializeField]
    private Vector2 _PosCardsNeutral;


    [SerializeField]
    private float _cardsOffset;

     void Awake()
    {
        _pokerCardsIA = new List<PokerCard>();
        _pokerCardsNeutral = new List<PokerCard>();
        _pokerCardsPlayer = new List<PokerCard>();
    }

    override public void StartGame()
    {
        base.StartGame();

        for(int i = 0; i < _NumberCardsPlayers; i++)
        {
            float leftOffset = (_PosCardsIA.x - (_cardsOffset * _NumberCardsPlayers) / 2) + (_cardsOffset * i);
            Vector2 posIA = new Vector2(leftOffset, _PosCardsIA.y);

            PokerCard cardIA = Instantiate<GameObject>(_prefabPokerCard, transform).GetComponent<PokerCard>();
            cardIA.GenerateCard(true);
            cardIA.transform.position = posIA;

            Vector2 posPlayer = new Vector2(leftOffset, _PosCardsPlayer.y);

            PokerCard cardPlayer = Instantiate<GameObject>(_prefabPokerCard, transform).GetComponent<PokerCard>();
            cardPlayer.GenerateCard();
            cardPlayer.transform.position = posPlayer;
            _pokerCardsIA.Add(cardIA);
            _pokerCardsPlayer.Add(cardPlayer);
        }
        for(int i = 0; i < _NumberCardsNeutral; i++)
        {
            float leftOffset = (_PosCardsNeutral.x - (_cardsOffset * _NumberCardsNeutral) / 2) + (_cardsOffset * i);
            Vector2 posNeutral = new Vector2(leftOffset, _PosCardsNeutral.y);

            PokerCard card = Instantiate<GameObject>(_prefabPokerCard, transform).GetComponent<PokerCard>();
            card.GenerateCard();
            card.transform.position = posNeutral;
            _pokerCardsNeutral.Add(card);
        }
    }

    override public void Validate()
    {
        base.Validate();
        //Put Coroutine HERE to check cards -> 
        Debug.Log("Validate Poker game");
        StartCoroutine(FlipEnumerator());
    }

    IEnumerator FlipEnumerator()
    {
        foreach (PokerCard card in _pokerCardsIA)
        {
            card.Flip();
            Debug.Log("Flip Card");
            yield return new WaitForSeconds(1.0f);
        }
        int scoreIA = 0;
        int scorePlayer = 0;
        for (int i = 0; i < _NumberCardsPlayers; i++)
        {
            for (int j = 0; j < _NumberCardsNeutral; j++)
            {
                scoreIA += (_pokerCardsIA[i].TypeCard == _pokerCardsNeutral[j].TypeCard ? 1 : 0);
                scorePlayer += (_pokerCardsPlayer[i].TypeCard == _pokerCardsNeutral[j].TypeCard ? 1 : 0);
            }
        }

        CallValidate(scoreIA < scorePlayer);
        yield return null;
    }


    public override void CloseGame()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        base.CloseGame();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(_PosCardsPlayer, new Vector3(1, 1, 1));
        Handles.Label(_PosCardsPlayer, "Player's Cards");

        Gizmos.DrawCube(_PosCardsIA, new Vector3(1, 1, 1));
        Handles.Label(_PosCardsIA, "IA's Cards");

        Gizmos.DrawCube(_PosCardsNeutral, new Vector3(1, 1, 1));
        Handles.Label(_PosCardsNeutral, "Neutral's Cards");
    }
}
