using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Change the speed of the game onclick
/// f.e : if speed == 1 then new speed == 2 
/// if speed == 2 then new speed == 0 (pause)
/// </summary>
public class GameSpeed : MonoBehaviour
{
    //int speedValue = 1;
    private GameManager manager;

    [SerializeField] private RawImage imageMenuPause; //don't forget to put the button's which change game speed label
    [SerializeField] private Texture vitesseDeux;
    [SerializeField] private Texture vitesseUn;
    [SerializeField] private Texture pause;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

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
