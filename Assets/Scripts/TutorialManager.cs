using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Link to the UI element for the tutorial panel")]
    private TextMeshProUGUI tutorialText;
    [SerializeField]
    private GameObject tutorialPanel;
    private bool isTutorialActive;

    void Awake()
    {
        tutorialText.text = "How to play: Use the arrow keys to move!\n\nCollect all the white and orange pellets to win!";
        Invoke("HideTutorial", 5f); //Hides tutorial after 5 seconds
    }

    void HideTutorial()
    {
        //Hide the tutorial panel
        tutorialPanel.gameObject.SetActive(false);
    }
}
