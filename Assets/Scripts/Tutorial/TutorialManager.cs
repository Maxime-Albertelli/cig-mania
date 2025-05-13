using UnityEngine;

// This class manages the player's choice to play in tutorial mode or not
public class TutorialManager : MonoBehaviour
{
    // Using a static class member means its value will be shared across all instances of this class
    public static TutorialManager Instance;
    // The TutorialStatus variable stores the player's choice regarding tutorial mode
    public bool TutorialStatus = false;

    // Awake is called as soon as the object is created
    private void Awake()
    {
        // Ensure that only a single instance of the TutorialManager class can exist
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
     
        // Ensure that the GameObject is preserved when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
