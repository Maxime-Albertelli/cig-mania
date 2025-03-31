using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Change the speed of the game onclick
/// f.e : if speed == 1 then new speed == 2 
/// if speed == 2 then new speed == 0 (pause)
/// When the game is paused, only the values are paused
/// Otherwise, we wouldn't be able to detect click event
/// </summary>
public class GameSpeed : MonoBehaviour
{
    //int speedValue = 1;
    private GameManager manager;

    [Header("Images Game Speed")]
    [SerializeField] private RawImage imageMenuPause; //don't forget to put the button's which change game speed label
    [Tooltip("Image of the speed value : 2")]
    [SerializeField] private Texture vitesseDeux;
    [Tooltip("Image of the speed value : 1")]
    [SerializeField] private Texture vitesseUn;
    [Tooltip("Image of pause game")]
    [SerializeField] private Texture pause;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Check the speed value in game manager
    /// 0 : pause game
    /// 1 : normal speed
    /// 2 : Fast speed
    /// </summary>
    public void gameSpeed()
    {
        switch (manager.speedValue)
        {
            case 1:
                Time.timeScale = 2; //change game speed
                manager.speedValue = 2; //change the variable used by the switch which indicate the game speed
                imageMenuPause.texture = vitesseDeux; //change the button's label text to indicate game speed to the player
                break;
            case 2:
                Time.timeScale = 1;
                manager.speedValue = 0;
                imageMenuPause.texture = pause;
                break;
            case 0:
                Time.timeScale = 1;
                manager.speedValue = 1;
                imageMenuPause.texture = vitesseUn;
                break;
        }
    }
}
