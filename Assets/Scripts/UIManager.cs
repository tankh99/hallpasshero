using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    // public GameObject mainMenuPanel;
    // public GameObject gamePanel;
    public GameObject dayEndPanel;
    
    // [Header("Main Menu")]
    // public Button startGameButton;
    // public Button quitGameButton;
    // public TextMeshProUGUI titleText;
    
    [Header("Game UI")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI studentsRemainingText;
    
    [Header("Student View")]
    public GameObject studentView;
    public TextMeshProUGUI studentNameText;
    public TextMeshProUGUI reasonText;
    public Image studentPortrait;
    public Button inspectPassButton;
    
    [Header("Hall Pass View")]
    public GameObject hallPassView;
    public TextMeshProUGUI passStudentNameText;
    public TextMeshProUGUI passDateText;
    public TextMeshProUGUI passLeaveAtText;
    public TextMeshProUGUI passReturnAtText;
    public TextMeshProUGUI passVisitingText;
    public TextMeshProUGUI passTeacherNameText;
    public Button closeButton;
    
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
    
    [Header("Decision Feedback")]
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;
    public Button continueButton;
    public Image feedbackIcon;  // Optional: for showing success/failure icon
    public float feedbackDisplayTime = 2f;  // How long to show the feedback
    
    private MainGame gameManager;
    
    private void Awake()
    {
        gameManager = FindFirstObjectByType<MainGame>();
        
        // // Set up button listeners
        // if (startGameButton != null)
        //     startGameButton.onClick.AddListener(StartGame);
        
        // if (quitGameButton != null)
        //     quitGameButton.onClick.AddListener(QuitGame);
        
        if (inspectPassButton != null)
            inspectPassButton.onClick.AddListener(InspectPass);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(ReturnToStudent);
        
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
        if (studentView != null) studentView.SetActive(true);
        if (hallPassView != null) hallPassView.SetActive(false);
        
        ShowMainMenu();
    }
    
    public void ShowMainMenu()
    {
        // mainMenuPanel.SetActive(true);
        // gamePanel.SetActive(false);
        dayEndPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        // mainMenuPanel.SetActive(false);
        // gamePanel.SetActive(true);
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
    
    public void ShowDayEndPanel(int day, int studentsChecked, int correctDecisions, int reputation)
    {
        // mainMenuPanel.SetActive(false);
        // gamePanel.SetActive(false);
        dayEndPanel.SetActive(true);
        
        dayEndTitleText.text = "Day " + day + " Complete!";
        studentsCheckedText.text = "Students Checked: " + studentsChecked;
        correctDecisionsText.text = "Correct Decisions: " + correctDecisions + "/" + studentsChecked;
        finalReputationText.text = "Reputation: " + reputation;
    }
    
    public void NextDay()
    {
        dayEndPanel.SetActive(false);
        // gamePanel.SetActive(true);
        
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

    public void UpdateStudentUI(string studentName, string reason, Sprite displayImage)
    {
        if (studentNameText != null)
            studentNameText.text = studentName;
        
        if (reasonText != null)
            reasonText.text = reason;
        
        if (studentPortrait != null)
            studentPortrait.sprite = displayImage;
    }
    private StudentProfile[] students;
    public void SetDecisionButtonsInteractable(bool interactable)
    {
        if (approveButton != null) approveButton.interactable = interactable;
        if (denyButton != null) denyButton.interactable = interactable;
        if (inspectPassButton != null) inspectPassButton.interactable = interactable;
    }

    public void SetStudentViewActive(bool active)
    {
        if (studentView != null) 
            studentView.SetActive(active);
    }

    public void SetHallPassViewActive(bool active)
    {
        if (hallPassView != null) 
            hallPassView.SetActive(active);
    }

    // Combined method for toggling between views
    public void ToggleViews(bool showStudent)
    {
        SetStudentViewActive(showStudent);
        SetHallPassViewActive(!showStudent);
    }

    public void UpdateHallPassUI(HallPassData currentPass)
    {
        if (passStudentNameText != null) passStudentNameText.text = currentPass.studentName;
        if (passDateText != null) passDateText.text = currentPass.date;
        if (passLeaveAtText != null) passLeaveAtText.text = currentPass.leaveAt;
        if (passReturnAtText != null) passReturnAtText.text = currentPass.returnAt;
        if (passVisitingText != null) passVisitingText.text = currentPass.visiting;
        if (passTeacherNameText != null) passTeacherNameText.text = currentPass.teacherName;
    }

    public void ShowDecisionFeedback(bool wasCorrect, string truth, int reputation, System.Action onContinue)
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(true);
            
            string resultText = wasCorrect ? "Correct Decision!" : "Incorrect Decision!";
            string fullText = $"{resultText}\n\nTruth: {truth}";
            
            if (feedbackText != null)
                feedbackText.text = fullText;
                
            // Set up continue button
            if (continueButton != null)
            {
                continueButton.onClick.RemoveAllListeners();  // Clear previous listeners
                continueButton.onClick.AddListener(() => {
                    feedbackPanel.SetActive(false);  // Hide the panel
                    onContinue?.Invoke();  // Call the continuation callback
                });
            }

            reputationText.text = "Reputation: " + reputation;
        }
    }

    private IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(feedbackDisplayTime);
        if (feedbackPanel != null)
            feedbackPanel.SetActive(false);
    }

} 