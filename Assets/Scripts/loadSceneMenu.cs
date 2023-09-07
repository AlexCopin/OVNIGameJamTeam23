using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneMenu : MonoBehaviour
{

    public void Update()
    {

        if (Input.GetButtonDown("Lever"))
        {
            SceneManager.LoadScene("PokerScene");
            Debug.Log("ui");
        }
    }
}
