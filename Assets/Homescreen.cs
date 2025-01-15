using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Homescreen : MonoBehaviour
{
    
    public void StartGame()
    {
        // load other scene 
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        # if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        # endif
        
        Application.Quit();
    }
    
    
}