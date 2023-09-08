using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CroupierSlide : MonoBehaviour
{
    public Transform croupier;
    public Vector2 boundaries;
    public float speed; 
    private void Update()
    {
        if (croupier.position.x > boundaries.x)
        {
            croupier.Translate(Vector3.left * speed*Time.deltaTime);
        }
    }
    
}
