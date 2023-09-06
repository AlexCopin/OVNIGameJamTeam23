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
    
    
    private int _playerInput;
    private int _aiInput;

    private bool _isPlayerChoice = false;
    private bool _canRedo = true;

    public Transform playerCardPosition; 
    public Transform aiCardPosition; 

    private  void Update()
    {
        //Pique > Coeur > Trèfles > Carreau > Pique
        
        // MISE 
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
            _instantiatedPlayerSign = Instantiate(signPrefab);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.DogeSign;
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            _isPlayerChoice = true;
            _playerInput = 1;
            _instantiatedPlayerSign = Instantiate(signPrefab);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.LamastiSign;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            _isPlayerChoice = true;
            _playerInput = 2;
            _instantiatedPlayerSign = Instantiate(signPrefab);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.EteRhumSign;
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            _isPlayerChoice = true;
            _playerInput = 3;
            _instantiatedPlayerSign = Instantiate(signPrefab);
            _instantiatedPlayerSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.ShitSign;
        }
        if (_isPlayerChoice == true)
        {
            _instantiatedPlayerSign.transform.position = playerCardPosition.position;
            _canRedo = false;
            AiChoice();
            CompareChoices();
        }
    }

    private void AiChoice()
    {
        _aiInput = Random.Range(0, 4);
        
        switch (_aiInput)
        {
            case 0:
                _instantiatedAiSign = Instantiate(signPrefab);
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.DogeSign;
                break; 
            case 1:
                _instantiatedAiSign = Instantiate(signPrefab);
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.LamastiSign;
                break; 
            case 2:
                _instantiatedAiSign = Instantiate(signPrefab);
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.EteRhumSign;
                break;
            case 3:
                _instantiatedAiSign = Instantiate(signPrefab);
                _instantiatedAiSign.GetComponent<RPS_Sign>().typeSign = RPS_Sign.SignType.ShitSign;
                break; 
        } 
        _instantiatedAiSign.transform.position = aiCardPosition.position;
    }
    
    private void CompareChoices()
    {
        if (_playerInput == _aiInput || Mathf.Abs(_playerInput - _aiInput) == 2)
        {
            _canRedo = true; 

        } else if (_playerInput !=  _aiInput) {
            if (_playerInput > _aiInput)
            {
                Debug.Log("Player Won");
                //player wins 
            }
            else 
            {
                Debug.Log("Player Lost");
                //player loses
            }
            
        }
    }
}