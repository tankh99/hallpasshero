using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LandingPageManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI titleText;
    
    [Header("Animation")]
    [SerializeField] private float titlePulseSpeed = 1f;
    [SerializeField] private float titlePulseScale = 1.1f;
    
    private void Start()
    {
        // Set up button listeners
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        // Start title animation
        if (titleText != null)
            StartCoroutine(PulseTitle());
    }
    
    private System.Collections.IEnumerator PulseTitle()
    {
        Vector3 originalScale = titleText.transform.localScale;
        float time = 0;
        
        while (true)
        {
            time += Time.deltaTime * titlePulseSpeed;
            float scale = Mathf.Lerp(1f, titlePulseScale, Mathf.PingPong(time, 1f));
            titleText.transform.localScale = originalScale * scale;
            yield return null;
        }
    }
    
    private void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainGame");
    }
    
    private void OpenSettings()
    {
        // TODO: Implement settings menu
        Debug.Log("Settings menu not implemented yet");
    }
    
    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 