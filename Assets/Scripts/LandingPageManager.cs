using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LandingPageManager : MonoBehaviour
{
    public Button startGameButton;
    public Button exitGameButton;
    
    private void Start()
    {
        // Set up button listeners
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
        if (exitGameButton != null)
            exitGameButton.onClick.AddListener(ExitGame);
    }
    
    private void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainGame");
    }
    
    
    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 