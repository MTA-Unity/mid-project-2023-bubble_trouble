using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Level1");
    }
}
