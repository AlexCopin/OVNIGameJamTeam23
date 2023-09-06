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
    [SerializeField] AnimationCurve _lerpCurve;
    float _timeElapsed;
    float _lerpDuration;
    Vector2 _originPos;
    [SerializeField] bool _isPlayer;
    bool _isChanging;

    private void Start()
    {
        _originPos = transform.position;
        _lerpDuration = 1f;
        
    }
    private void Update()
    {
        
        if (_isPlayer && !_isChanging)
        {
 

            if (_timeElapsed < _lerpDuration)
            {

                gameObject.transform.position = Vector2.Lerp(_originPos, new Vector2(_originPos.x + _speedValue / 10, _originPos.y - _losingValue), _timeElapsed / _lerpDuration);
                    

                _timeElapsed += Time.deltaTime;
            }
            else
            {
                _originPos = transform.position;
                _timeElapsed = 0;
            }
            
           
        }

        else
        {
            
            if (_timeElapsed < _lerpDuration)
            {

                gameObject.transform.position = Vector2.Lerp(_originPos, new Vector2(_originPos.x + _speedValue / 10, gameObject.transform.position.y), _timeElapsed / _lerpDuration);


                _timeElapsed += Time.deltaTime;
            }
            else
            {
                _originPos = transform.position;
                _timeElapsed = 0;
            }


        }

        //Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isChanging)
            {
                //_isChanging = true;
                MovementCurve(Random.Range(-2f, 2f));
                
            }
            
        }
        
    }
    void MovementCurve(float value)
    {
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
