using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RPS_Manager : MiniGame
{
    public GameObject signPrefab;
    private GameObject _instantiatedPlayerSign;
    private GameObject _instantiatedAiSign;

    public Vector2 instantiatedPlayerPos;
    public Vector2 instantiatedAiPos; 
    
    private int _playerInput;
    private int _aiInput;

    private bool _isPlayerChoice = false;
    private bool _canRedo = true;

    public override void StartGame()
    {
        base.StartGame();
        _canRedo = true; 
        AiChoice();
    }

    private void Update()
    {
        //Pique > Coeur > Trèfles > Carreau > Pique
        if (_canRedo)
        {
            PlayerChoice();
        }
    }

    private void PlayerChoice()
    {
        _isPlayerChoice = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            _isPlayerChoice = true;
            _playerInput = 0;
            _instantiatedPlayerSign = Instantiate(signPrefab, instantiatedPlayerPos, Quaternion.Euler(270, 0,90), gameObject.transform);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.DogeSign;
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            _isPlayerChoice = true;
            _playerInput = 1;
            _instantiatedPlayerSign = Instantiate(signPrefab, instantiatedPlayerPos, Quaternion.Euler(270, 0,90), gameObject.transform);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.LamastiSign;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            _isPlayerChoice = true;
            _playerInput = 2;
            _instantiatedPlayerSign = Instantiate(signPrefab, instantiatedPlayerPos, Quaternion.Euler(270, 0,90), gameObject.transform);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.EteRhumSign;
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            _isPlayerChoice = true;
            _playerInput = 3;
            _instantiatedPlayerSign = Instantiate(signPrefab, instantiatedPlayerPos, Quaternion.Euler(270, 0,90), gameObject.transform);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.ShitSign;
        }
        if (_isPlayerChoice == true)
        {
            _canRedo = false;
            StartCoroutine(CompareChoices());
        }
    }

    private void AiChoice()
    {
        _aiInput = Random.Range(0, 4);
        
        _instantiatedAiSign = Instantiate(signPrefab, instantiatedAiPos, Quaternion.Euler(270,180,90),gameObject.transform);
        switch (_aiInput)
        {
            case 0:
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.DogeSign;
                break; 
            case 1:
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.LamastiSign;
                break; 
            case 2:
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.EteRhumSign;
                break;
            case 3:
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.ShitSign;
                break; 
        } 
    }
    
    private IEnumerator CompareChoices()
    {
        _instantiatedAiSign.transform.eulerAngles = new Vector3(270, 0, 90);
        
        yield return new WaitForSeconds(2.0f);
        if (_playerInput == _aiInput || Mathf.Abs(_playerInput - _aiInput) == 2)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            _canRedo = true; 
            AiChoice();

        } else if (_playerInput !=  _aiInput) {
            if (_playerInput > _aiInput)
            {
                Debug.Log("Player Won");
                CallValidate(true);
            }
            else 
            {
                Debug.Log("Player Lost");
                CallValidate(false);
            }
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}