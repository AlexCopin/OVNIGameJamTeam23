using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PokerManager _pokerManager;

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
    }
    void Start()
    {
        if (_validateEvent == null)
            _validateEvent = new UnityEvent();
        _validateAction += ValidateCurrentGame;
        _validateEvent.AddListener(_validateAction);
    }

    void Update()
    {
        if (Input.GetButtonDown("Validate"))
            _validateEvent.Invoke();
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
        if (_pokerManager.IsActive)
        {

        }
    }
}
