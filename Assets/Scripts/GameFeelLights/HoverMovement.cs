using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    [Header ("Rotation")]
    public float duration;
    [Range (0,1)]
    public float maxAngle;
    public AnimationCurve curve;


    [Header ("Scale")]
    public bool useScale;
    public AnimationCurve scaleCurve;

    public float scaleCorrection = 1;

    void Awake()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            this.gameObject.transform.rotation = new Quaternion(0f,0f, strength * maxAngle, 1f); 
            // this.gameObject.transform.Rotate(0f, 0f, strength * maxAngle, Space.World);
            if(useScale)
            {
                float scaleStrength = scaleCurve.Evaluate(elapsedTime/duration);
                float corr = Mathf.Abs(scaleStrength) * scaleCorrection; // Correction to apply to the scale
                this.gameObject.transform.localScale = new Vector3(corr, corr, corr);
            }
            yield return null;
        }
        StartCoroutine(Rotate());
    }
}
