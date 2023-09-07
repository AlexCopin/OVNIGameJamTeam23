using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PokerManager _pokerManager;
    [SerializeField]
    private RPS_Manager _Rps;
    [SerializeField]
    private Curve _Curve;

    MiniGame _currentGame;

    public float StartMoney;
    private float _currentMoney;
    [SerializeField]
    private float _Bid1Value;
    [SerializeField]
    private float _Bid2Value;
    [SerializeField]
    private float _Bid3Value;
    [SerializeField]
    private float _Bid4Value;
    private float _currentBid;

    [SerializeField]
    private TMP_Text _textCurrentBid;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        _currentMoney = StartMoney;
        _pokerManager.StartGame();
        _currentGame = _pokerManager;
        _pokerManager.OnValidate += ValidateCurrentGame;
        _currentBid = _Bid1Value;
        _textCurrentBid.text = "" + _currentBid;
    }

    void Update()
    {
        if (Input.GetButtonDown("Validate"))
        {
            Debug.Log("hit button validate");
            _currentGame.Validate();
        }
        if (Input.GetButtonDown("Bet1"))
        {
            Debug.Log("Change Bet1");
            _currentBid = _Bid1Value;
            _textCurrentBid.text = "" + _currentBid;
        }
        if (Input.GetButtonDown("Bet2"))
        {
            Debug.Log("Change Bet2");
            _currentBid = _Bid2Value;
            _textCurrentBid.text = "" + _currentBid;
        }
        if (Input.GetButtonDown("Bet3"))
        {
            Debug.Log("Change Bet3");
            _currentBid = _Bid3Value;
            _textCurrentBid.text = "" + _currentBid;
        }
        if (Input.GetButtonDown("Bet4"))
        {
            Debug.Log("Change Bet4");
            _currentBid = _Bid4Value;
            _textCurrentBid.text = "" + _currentBid;
        }
    }

    void ValidateCurrentGame(bool PlayerWon)
    {
        //Start coroutine or shits for FX Sounds
        //Then call ChangeGame
        if (PlayerWon)
        {
            Debug.Log("Player won");
            _Curve.MovementCurve(_currentBid);
        }
        else
        {
            Debug.Log("Player lost");
            _Curve.MovementCurve(-_currentBid);
        }
        ChangeGame();
    }

    void ChangeGame()
    {
        if (_pokerManager.gameObject.activeSelf)
        {
            _pokerManager.CloseGame();
            _pokerManager.OnValidate -= ValidateCurrentGame;
            _pokerManager.gameObject.SetActive(false);

            _Rps.gameObject.SetActive(true);
            _Rps.OnValidate += ValidateCurrentGame;
            _currentGame = _Rps;
        }
        else if (_Rps.gameObject.activeSelf)
        {
            _Rps.CloseGame();
            _Rps.OnValidate -= ValidateCurrentGame;
            _Rps.gameObject.SetActive(false);

            _pokerManager.gameObject.SetActive(true);
            _pokerManager.OnValidate += ValidateCurrentGame;
            _currentGame = _pokerManager;
        }
        _currentGame.StartGame();
    }
}
