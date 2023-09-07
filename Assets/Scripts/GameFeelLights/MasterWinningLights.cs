using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWinningLights : MonoBehaviour
{
    [Header ("Controls")]
    public bool triggerAll;
    public bool hideAll;

    public bool loop = true;

    public bool setParams;
    public WinningLightController[] slavedLights;



    public float winStreak;
    
    [Header ("Params")]
    public float Correction = 10f;
    public float duration;
    public AnimationCurve rangeCurve;
    public AnimationCurve intensityCurve;
    public Color[] lightColors;
    


    // Start is called before the first frame update
    void Start()
    {
       setParamsOnSlaves();
       hideAll = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(setParams) // Set tous les params du Master sur ses Slaves
        {
            setParamsOnSlaves();
            setParams = false;
        }

        if(triggerAll) // Active toutes les lights
        {
            foreach (WinningLightController light in slavedLights)
            {
                light.gameObject.SetActive(true);
                light.loop = loop;
                light.trigger = true;
            }
            triggerAll = false;
        }

        if(hideAll)
        {
            foreach (WinningLightController light in slavedLights)
            {
                light.gameObject.SetActive(false);
            }
            hideAll = false;
        }
    }

    public void setParamsOnSlaves()
    {
        foreach (WinningLightController light in slavedLights)
        {
            light.Correction = Correction;
            light.winStreak = winStreak;
            light.duration = duration;
            light.curve = rangeCurve;
            light.intensityCurve = intensityCurve;
            light.colorArray = lightColors;
        }
    }
}
