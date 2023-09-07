using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    // Update is called once per frame
    public void LaunchGame(string PokerScene)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(PokerScene);
        }
    }
}
