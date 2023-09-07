using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CurveLine : MonoBehaviour
{
    private LineRenderer _line;
    private Vector2 _previousPosition;
    public Vector2 _currentPos;
    private Vector2 _startPos;

    [SerializeField] private float _minDistance = 0.1f;

    [SerializeField] int _moneyValue;
    [SerializeField] float _speedValue;
    [SerializeField] int _losingValue;
    [SerializeField] bool _isPlayer;
    [SerializeField] Camera _cam;
    [SerializeField] CurveLine _playerCurve;
    [SerializeField] GameObject _curveZone;
    float _timeElapsed;
    float _lerpDuration;
    Vector2 _originPos;
    int _randomValueBot;
    bool _isChanging;
    float _unitForMoney;


    bool _isAtRight;

    [Header("Bot Curve")]
    [SerializeField] int _maxMoneyToLose;
    [SerializeField] int _maxMoneyToAdd;

    int _originMoneyValue;

    Vector2 _curvePos;
    private void Start()
    {


        _isAtRight = false;
        _lerpDuration = 1f;
        _randomValueBot = Random.Range(_maxMoneyToLose, _maxMoneyToAdd);
        _moneyValue = 100;
        _timeElapsed = 0f;
        
        _unitForMoney = _curveZone.GetComponent<SpriteRenderer>().bounds.size.y / 600;
        transform.position = new Vector2(_curveZone.transform.position.x - (_curveZone.GetComponent<SpriteRenderer>().bounds.size.x / 2) + (50 * _unitForMoney), _curveZone.transform.position.y - (_curveZone.GetComponent<SpriteRenderer>().bounds.size.y / 2) + (50 * _unitForMoney) + (_moneyValue * _unitForMoney));
        GetComponent<LineRenderer>().enabled = true;
        _line = GetComponent<LineRenderer>();
        _previousPosition = transform.position;
        transform.position = new Vector3(0, 0, 0);
        _originPos = _previousPosition;
        _originMoneyValue = _moneyValue;

        _curvePos = transform.position;
    }

    private void Update()
    {
        
        CheckIfOut();
        if (_isPlayer)
        {
            if (!_isChanging)
            {
                if (_timeElapsed < _lerpDuration)
                {
                    if(_isAtRight)
                    {
                        _line.useWorldSpace = false;
                        transform.position = Vector2.Lerp(_curvePos, new Vector2(_curvePos.x - _speedValue / 10, _curvePos.y), _timeElapsed / _lerpDuration);
                    }
                    
                    _currentPos = Vector2.Lerp(_originPos, new Vector2(_originPos.x + _speedValue / 10, _originPos.y - (_losingValue * _unitForMoney)), _timeElapsed / _lerpDuration);
                    _moneyValue = (int)Mathf.Lerp(_originMoneyValue, _originMoneyValue - _losingValue, _timeElapsed / _lerpDuration);
                    _line.positionCount++;
                    _line.SetPosition(_line.positionCount - 1, _currentPos);
                    _previousPosition = _currentPos;
                    _timeElapsed += Time.deltaTime;
                }

                
                else
                {
                    _timeElapsed = 0;
                    _originPos = _previousPosition;
                    _originMoneyValue = _moneyValue;

                    _curvePos = transform.position;
                }
            }
        }

        else
        {


            if (_timeElapsed < _lerpDuration)
            {
                //_currentPos = Vector2.Lerp(_originPos, new Vector2(_playerCurve._currentPos.x, _originPos.y), _timeElapsed / _lerpDuration);
                _currentPos = Vector2.Lerp(_originPos, new Vector2(_playerCurve._currentPos.x, _originPos.y), _timeElapsed / _lerpDuration);
                _line.positionCount++;
                _line.SetPosition(_line.positionCount - 1, _currentPos);
                _previousPosition = _currentPos;


                _timeElapsed += (Random.Range(0.05f, 0.1f) + Time.deltaTime);
                
            }
            else
            {
                MovementCurve(_randomValueBot);
                _timeElapsed = 0;
                _originPos = _previousPosition;
                
                _randomValueBot = Random.Range(_maxMoneyToLose, _maxMoneyToAdd);
                if (_moneyValue <= 80)
                {
                    _randomValueBot = Mathf.Abs(_randomValueBot);
                }
            }

            _moneyValue = (int)(_currentPos.y * 100);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isChanging && _isPlayer)
            {
                MovementCurve(Random.Range(-200, 200));

            }

        }
    }
    public void MovementCurve(int money)
    {
        _moneyValue -= money;
        _isChanging = true;
        float elapsedTime = 0;
        Vector2 startPos = _previousPosition;
        while (elapsedTime < _lerpDuration)
        {
            _currentPos = Vector2.Lerp(startPos, new Vector2(startPos.x, startPos.y + money / 100), elapsedTime / _lerpDuration);
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, _currentPos);
            _previousPosition = _currentPos;
            elapsedTime += Time.deltaTime;
        }
        _originPos = _previousPosition;
        _isChanging = false;
    }



    void CheckIfOut()
    {
        float xmax = _curveZone.transform.position.x + (_curveZone.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        float ymin = _curveZone.transform.position.y - (_curveZone.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        float ymax = _curveZone.transform.position.y + (_curveZone.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        if (_currentPos.x >= xmax)
        {
            Debug.Log("Trop � droite");
            _isAtRight = true;
        }
        else if (_currentPos.y <= ymin)
        {
            Debug.Log("Trop en bas");
        }
        else if (_currentPos.y >= ymax)
        {
            Debug.Log("Trop en haut");
        }
    }




}
