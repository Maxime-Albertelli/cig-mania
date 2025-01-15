using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

/// <summary>
/// Change the speed of the game onclick
/// f.e : if speed == 1 then new speed == 2 
/// if speed == 2 then new speed == 0 (pause)
/// </summary>

public class GameSpeed : MonoBehaviour
{
    int speedValue = 1;
    [SerializeField] private TMP_Text buttonText; //don't forget to put the button's which change game speed label

    //
    public void gameSpeed()
    {
        switch (speedValue)
        {
            case 1:
                Time.timeScale = 2; //change game speed
                speedValue = 2; //change the variable used by the switch which indicate the game speed
                buttonText.text = "x2"; //change the button's label text to indicate game speed to the player
                break;
            case 2:
                Time.timeScale = 0;
                speedValue = 0;
                buttonText.text = "Pause";
                break;
            case 0:
                Time.timeScale = 1;
                speedValue = 1;
                buttonText.text = "x1";
                break;
        }
    }
}
