using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject gamePanel;
    public GameObject dayEndPanel;
    
    [Header("Main Menu")]
    public Button startGameButton;
    public Button quitGameButton;
    public TextMeshProUGUI titleText;
    
    [Header("Game UI")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI studentsRemainingText;
    
    [Header("Student View")]
    public GameObject studentView;
    public TextMeshProUGUI studentNameText;
    public TextMeshProUGUI studentGradeText;
    public Image studentPortrait;
    public Button inspectPassButton;
    
    [Header("Hall Pass View")]
    public GameObject hallPassView;
    public TextMeshProUGUI passNameText;
    public TextMeshProUGUI passDateText;
    public TextMeshProUGUI passTimeText;
    public TextMeshProUGUI passDestinationText;
    public TextMeshProUGUI passTeacherText;
    public Image passStamp;
    public Button returnToStudentButton;
    
    [Header("Decision Buttons")]
    public Button approveButton;
    public Button denyButton;
    
    [Header("Day End Panel")]
    public TextMeshProUGUI dayEndTitleText;
    public TextMeshProUGUI studentsCheckedText;
    public TextMeshProUGUI correctDecisionsText;
    public TextMeshProUGUI incorrectDecisionsText;
    public TextMeshProUGUI finalReputationText;
    public Button nextDayButton;
    
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // Set up button listeners
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
        
        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(QuitGame);
        
        if (inspectPassButton != null)
            inspectPassButton.onClick.AddListener(InspectPass);
        
        if (returnToStudentButton != null)
            returnToStudentButton.onClick.AddListener(ReturnToStudent);
        
        if (approveButton != null)
            approveButton.onClick.AddListener(ApproveStudent);
        
        if (denyButton != null)
            denyButton.onClick.AddListener(DenyStudent);
        
        if (nextDayButton != null)
            nextDayButton.onClick.AddListener(NextDay);
    }
    
    private void Start()
    {
        // Show the main menu at start
        ShowMainMenu();
    }
    
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gamePanel.SetActive(false);
        dayEndPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        dayEndPanel.SetActive(false);
        
        // Initialize the game manager
        if (gameManager != null)
            gameManager.InitializeDay();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    public void InspectPass()
    {
        if (gameManager != null)
            gameManager.ToggleHallPass();
    }
    
    public void ReturnToStudent()
    {
        if (gameManager != null)
            gameManager.ToggleHallPass();
    }
    
    public void ApproveStudent()
    {
        if (gameManager != null)
            gameManager.ApproveStudent();
    }
    
    public void DenyStudent()
    {
        if (gameManager != null)
            gameManager.DenyStudent();
    }
    
    public void ShowDayEndPanel(int day, int studentsChecked, int correctDecisions, int incorrectDecisions, int reputation)
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(false);
        dayEndPanel.SetActive(true);
        
        dayEndTitleText.text = "Day " + day + " Complete!";
        studentsCheckedText.text = "Students Checked: " + studentsChecked;
        correctDecisionsText.text = "Correct Decisions: " + correctDecisions;
        incorrectDecisionsText.text = "Incorrect Decisions: " + incorrectDecisions;
        finalReputationText.text = "Reputation: " + reputation;
    }
    
    public void NextDay()
    {
        dayEndPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        // Start the next day
        if (gameManager != null)
            gameManager.InitializeDay();
    }
    
    public void UpdateGameUI(int day, int studentsRemaining, int reputation, string time)
    {
        dayText.text = "Day " + day;
        studentsRemainingText.text = "Students: " + studentsRemaining;
        reputationText.text = "Reputation: " + reputation;
        timeText.text = time;
    }
} 