using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningLightController : MonoBehaviour
{
    [Header ("Controls")]
    public bool trigger = false;
    public bool loop = false;

    public float winStreak;
    public float duration = 0f;

    [SerializeField] public float Correction = 1f;
    
    [Space]

    [Header ("Range")]
    public AnimationCurve curve;
    public Light _light;


    [Header ("Color")]
    public Color[] colorArray;

    [Header ("Intensity")]
    public AnimationCurve intensityCurve;

    [Header ("Debug")]
    public bool over;
    public float _value;
    public float _Time;


    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            trigger = false;
            StartCoroutine(LightRange());
            StartCoroutine(LightColor());
            StartCoroutine(LightIntensity());
        }        
    }

    IEnumerator LightRange() // La Range suit la curve 
    {
        over = false;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            _light.range = strength * (winStreak * 2 + Correction); //* Correction;

            _Time = elapsedTime;
            _value = strength;
            yield return null;
        }
        over = true;
        if(loop)
        {
            StartCoroutine(LightRange());
            StartCoroutine(LightColor());
            StartCoroutine(LightIntensity());
        }
        
    }
     IEnumerator LightColor() // Lerp vers une couleur aléatoire prédéfinie
    {
        float elapsedTime = 0f;
        Color oldColor = _light.color;
        Color newColor = colorArray[Random.Range(0, colorArray.Length - 1)];
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);

            //Lerp de l'ancienne couleur à la nouvelle
            _light.color = Color.Lerp(oldColor, newColor, strength);
            yield return null;
        }
        
    }

     IEnumerator LightIntensity() // L'intensité de la light suit la curve d'intensité 
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = intensityCurve.Evaluate(elapsedTime/duration);
            _light.intensity = strength * (winStreak * 2 + Correction);
            yield return null;

        }
    }

}

