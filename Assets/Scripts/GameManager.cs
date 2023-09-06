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

    private MiniGame _CurrentMiniGame;

    [HideInInspector]
    private UnityEvent _validateEvent;
    [HideInInspector]
    public UnityAction _validateAction;

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
        if (_validateEvent == null)
            _validateEvent = new UnityEvent();
        _validateAction += ValidateCurrentGame;
        _validateEvent.AddListener(_validateAction);
    }
    void Start()
    {
        _pokerManager.StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("hit button validate");
            _validateEvent.Invoke();
        }
    }

    void ValidateCurrentGame()
    {
        Debug.Log("Valider le current game");
        //Start coroutine or shits for FX Sounds
        //Then call ChangeGame
        ChangeGame();
    }

    void ChangeGame()
    {
        if (_pokerManager.gameObject.activeSelf)
        {
            _pokerManager.CloseGame();
            _pokerManager.gameObject.SetActive(false);
            _Rps.gameObject.SetActive(true);
            _Rps.StartGame();
        }
        else if (_Rps.gameObject.activeSelf)
        {
            _Rps.CloseGame();
            _Rps.gameObject.SetActive(false);
            _pokerManager.gameObject.SetActive(true);
            _Rps.StartGame();
        }
    }
}
