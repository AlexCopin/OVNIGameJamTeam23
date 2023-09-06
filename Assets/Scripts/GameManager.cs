using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PokerManager _pokerManager;
    [SerializeField]
    private Rps _Rps;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("hit button validate");
            _currentGame.Validate();
        }
        if (Input.GetButtonDown("Bet1"))
        {
            Debug.Log("Change Bet1");
            _currentBid = _Bid1Value;
        }
        if (Input.GetButtonDown("Bet2"))
        {
            Debug.Log("Change Bet2");
            _currentBid = _Bid2Value;
        }
        if (Input.GetButtonDown("Bet3"))
        {
            Debug.Log("Change Bet3");
            _currentBid = _Bid3Value;
        }
        if (Input.GetButtonDown("Bet4"))
        {
            Debug.Log("Change Bet4");
            _currentBid = _Bid4Value;
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
