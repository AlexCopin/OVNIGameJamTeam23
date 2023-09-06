using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{

    virtual protected void Start()
    {
        
    }
    virtual public void StartGame()
    {
        GameManager.Instance._validateAction += Validate;
    }
    
    virtual public void Validate()
    {
        GameManager.Instance._validateAction -= Validate;
    }

    virtual public void CloseGame()
    {
        //Global code put here
        gameObject.SetActive(false);
    }
}
