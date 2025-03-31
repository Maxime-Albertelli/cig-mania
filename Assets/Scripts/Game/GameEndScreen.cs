using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the end screen
/// </summary>
public class GameEndScreen : MonoBehaviour
{
    [Header("Stats text display")]
    [Tooltip("The reason why you lose Text")]
    [SerializeField] private TMP_Text reasonLost;
    [Tooltip("The reason why you win Text")]
    [SerializeField] private TMP_Text reasonWin;

    private bool victory = false;

    /// <summary>
    /// Apply description adapt to the reason why the player win / loose
    /// </summary>
    public void ApplyDescription()
    {
        if (victory)
        {

        }
        else
        {
            reasonLost.text = "Vous avez perdu la confiance des acheteurs.";
        }
    }

    /// <summary>
    /// When called, restart the game
    /// </summary>
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    /// <summary>
    /// When called, load the menu
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("CigManiaHome");
    }

    #region Getter / Setter
    /// <summary>
    /// Set a boolean to know if it's a victory or a defeat
    /// </summary>
    /// <param name="victory">True if a victory, false for a defeat</param>
    public void SetVictory(bool victory)
    {
        this.victory = victory;
    }
    
    #endregion
}
