using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rps : MiniGame
{
    private readonly List<string> _cardValues = new List<string>();
    
    private string _playerInput;
    private string _aiInput;
    
    private void Awake()
    {
        _cardValues.Add("pique");
        _cardValues.Add("coeur");
        _cardValues.Add("trèfle");
        _cardValues.Add("carreau");
    }

    override protected void Start()
    {
        //Pique > Coeur > Trèfles > Carreau > Pique
        
        // MISE 
        PlayerChoice();
        AiChoice();
        CompareChoices();
    }

    private void PlayerChoice()
    {
        if (Input.GetKeyDown(KeyCode.A))
        { 
            _playerInput = _cardValues[1];
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            _playerInput = _cardValues[2];
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            _playerInput = _cardValues[3];
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            _playerInput = _cardValues[4];
        }
    }

    private void AiChoice()
    {
        _aiInput = _cardValues[Random.Range(0, 4)];
    }
    
    private void CompareChoices()
    {
        while (_playerInput == _aiInput)
        {
            PlayerChoice();
            AiChoice();
            
        } if (_playerInput !=  _aiInput) {
            switch (_playerInput)
            {
                case "piques":
                    if (_aiInput == "coeur")
                    {
                        Debug.Log("Player Won");
                        //player wins 
                    }
                    else if (_aiInput == "carreau")
                    {
                        Debug.Log("Player Lost");
                        //player loses
                    }
                    break;

                case "coeur":
                    if (_aiInput == "trèfle")
                    {
                        Debug.Log("Player Won");
                        //player wins
                    }
                    else if (_aiInput == "pique")
                    {
                        Debug.Log("Player Lost");
                        //player loses
                    }
                    break;

                case "trèfle":
                    if (_aiInput == "carreau")
                    {
                        Debug.Log("Player Won");
                        //player wins
                    }
                    else if (_aiInput == "coeur")
                    {
                        Debug.Log("Player Lost");
                        //player loses
                    }
                    break;

                case "carreau":
                    if (_aiInput == "pique")
                    {
                        Debug.Log("Player Won");
                        //player wins 
                    }
                    else if (_aiInput == "trèfle")
                    {
                        Debug.Log("Player Lost");
                        //player loses
                    }
                    break;
            }
        }
    }
}