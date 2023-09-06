using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{

    public bool IsActive = false;

    void Start()
    {
        
    }

    void Update()
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
}
