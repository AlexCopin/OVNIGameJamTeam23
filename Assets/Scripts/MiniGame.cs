using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public event Action<bool> OnValidate;

    public int playerScore = 0; 

    virtual protected void Start()
    {
    }
    virtual public void StartGame()
    {
        //Global code
    }
    
    virtual public void Validate()
    {
        //Global code
    }

    protected void CallValidate(bool Result)
    {
        OnValidate?.Invoke(Result);
    }

    virtual public void CloseGame()
    {
        //Global code put here
        gameObject.SetActive(false);
    }

    public void ScoreUpdate()
    {
        playerScore += 10;
    }
    
}
