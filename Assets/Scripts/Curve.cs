using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] float _moneyValue;
    [SerializeField] float _speedValue;
    [SerializeField] float _losingValue;
    [SerializeField] bool _isPlayer;
    [SerializeField] Camera _cam;
    [SerializeField] Transform _playerCurve;
    float _timeElapsed;
    float _lerpDuration;
    Vector2 _originPos;
    float _randomValueBot;
    bool _isChanging;

    [Header("Bot Curve")]
    [SerializeField] float _maxMoneyToLose;
    [SerializeField] float _maxMoneyToAdd;

    private void Start()
    {
        _originPos = transform.position;
        _lerpDuration = 1f;
        _randomValueBot = Random.Range(_maxMoneyToLose/50, _maxMoneyToAdd/50);
        _moneyValue = GameManager.Instance.StartMoney;
    }

    //Note: Money = 50 * height 
    private void Update()
    {
        if (_isPlayer)
        {
            if (_moneyValue <= 0)
            {
                Debug.Log("Death");
            }

            if (gameObject.transform.position.x >= _cam.transform.position.x + _cam.pixelWidth)
            {
                /*_cam.transform.parent = gameObject.transform;*/
                Debug.Log("out");
            }

            else if (!_isChanging)
            {


                if (_timeElapsed < _lerpDuration)
                {

                    gameObject.transform.position = Vector2.Lerp(_originPos, new Vector2(_originPos.x + _speedValue / 10, _originPos.y - _losingValue), _timeElapsed);


                    _timeElapsed += Time.deltaTime;
                }
                else
                {
                    _originPos = transform.position;
                    _timeElapsed = 0;

                }


            }
        }

        else
        {
            if (_timeElapsed < _lerpDuration)
            {

                gameObject.transform.position = Vector2.Lerp(_originPos, new Vector2(_playerCurve.position.x, transform.position.y), _timeElapsed);


                _timeElapsed += (Random.Range(0.005f, 0.05f) + Time.deltaTime);
            }
            else
            {
                MovementCurve(_randomValueBot);
                _originPos = transform.position;
                _timeElapsed = 0;
                _randomValueBot = Random.Range(_maxMoneyToLose / 50, _maxMoneyToAdd / 50);
                if (_moneyValue <= 80)
                {
                    _randomValueBot = Mathf.Abs(_randomValueBot);
                }
            }

            _moneyValue = gameObject.transform.position.y*50;
        }

        //Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isChanging && _isPlayer)
            {
                //_isChanging = true;
                MovementCurve(Random.Range(-2f, 2f));
                
            }
            
        }



    }
    public void MovementCurve(float value)
    {
        _moneyValue -= value * 10;
        _isChanging = true;
        float elapsedTime = 0;
        Vector2 startPos = gameObject.transform.position;
        while (elapsedTime < _lerpDuration)
        {
            gameObject.transform.position = Vector2.Lerp(startPos, new Vector2(startPos.x, startPos.y + value), elapsedTime/_lerpDuration);
            elapsedTime += Time.deltaTime;
        }
        _originPos = gameObject.transform.position;
        _isChanging = false;
    }
}
