using UnityEngine;

public class TutorialActivated : MonoBehaviour
{
    public void SetTutorialStatus(bool status)
    {
        // Pass the player's choice regarding tutorial mode to our current TutorialManager instance
        TutorialManager.Instance.TutorialStatus = status;
    }
}
