using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LandingPageManager : MonoBehaviour
{
    public Button startGameButton;
    
    private void Start()
    {
        // Set up button listeners
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
    }
    
    private void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainGame");
    }
    
    
    // private void QuitGame()
    // {
    //     #if UNITY_EDITOR
    //         UnityEditor.EditorApplication.isPlaying = false;
    //     #else
    //         Application.Quit();
    //     #endif
    // }
} 