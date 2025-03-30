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
    public TextMeshProUGUI dateTimeText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI studentsRemainingText;
    
    [Header("Student View")]
    public GameObject studentView;
    public TextMeshProUGUI studentNameText;
    public TextMeshProUGUI reasonText;
    public Image studentPortrait;
    public Button inspectPassButton;
    public Button teacherListButton;
    public Button locationListButton;
    [Header("Hall Pass View")]
    public GameObject hallPassView;
    public TextMeshProUGUI passStudentNameText;
    public TextMeshProUGUI passDateText;
    public TextMeshProUGUI passLeaveAtText;
    public TextMeshProUGUI passReturnAtText;
    public TextMeshProUGUI passVisitingText;
    public TextMeshProUGUI passTeacherNameText;
    public Button closeButton;

    [Header("Teacher List View")]
    public GameObject teacherListView;
    public TextMeshProUGUI teacherLeftText;
    public TextMeshProUGUI teacherRightText;
    public Button teacherListCloseButton;

    [Header("Location List View")]
    public GameObject locationListView;
    public TextMeshProUGUI locationListLeftText;
    public TextMeshProUGUI locationListRightText;
    public Button locationListCloseButton;
    
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
    
    [Header("Text Streaming")]
    public AudioSource textSound;
    public float characterDelay = 0.05f;
    public float commaDelay = 0.2f;
    public float periodDelay = 0.4f;
    // Separate pitch ranges for different voices
    public float boyPitchMin = 0.95f;
    public float boyPitchMax = 1.05f;
    public float girlPitchMin = 1.1f;
    public float girlPitchMax = 1.2f;
    
    private MainGame gameManager;
    
    private Coroutine textStreamCoroutine;
    private string currentFullText;  // Store the full text
    private StudentProfile currentSpeaker;  // Add this field

    [Header("Feedback Sounds")]
    public AudioSource feedbackSound;
    public AudioClip successSound;
    public AudioClip failureSound;
    public AudioSource dayEndSound;
    
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
        
        if (teacherListButton != null)
            teacherListButton.onClick.AddListener(ShowTeacherList);

        if (locationListButton != null)
            locationListButton.onClick.AddListener(ShowLocationList);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(ReturnToStudent);

        if (teacherListCloseButton != null)
            teacherListCloseButton.onClick.AddListener(ShowTeacherList);

        if (locationListCloseButton != null)
            locationListCloseButton.onClick.AddListener(ShowLocationList);
        
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

    public void ShowTeacherList()
    {
        if (gameManager != null)
            gameManager.ToggleTeacherList();
    }

    public void ShowLocationList()
    {
        if (gameManager != null)
            gameManager.ToggleLocationList();
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
        if (dayEndSound != null) {
            dayEndSound.Play();
        }
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
    
    public void UpdateGameUI(int day, int studentsRemaining, int reputation, string dateTime)
    {
        dayText.text = "Day " + day;
        studentsRemainingText.text = "Students: " + studentsRemaining;
        reputationText.text = "Reputation: " + reputation;
        dateTimeText.text = dateTime;
    }

    private IEnumerator StreamText(string fullText, TextMeshProUGUI textComponent)
    {
        currentFullText = fullText;
        textComponent.text = "";
        
        for (int i = 0; i < fullText.Length; i++)
        {
            char c = fullText[i];
            
            if (textSound != null && c != ' ')
            {
                // Set pitch based on speaker
                if (currentSpeaker != null && currentSpeaker.isGirl)
                {
                    textSound.pitch = Random.Range(girlPitchMin, girlPitchMax);
                }
                else
                {
                    textSound.pitch = Random.Range(boyPitchMin, boyPitchMax);
                }
                textSound.Play();
            }
            
            textComponent.text += c;

            float pauseDuration = characterDelay;
            if (c == '.' || c == '!' || c == '?')
            {
                pauseDuration = periodDelay;
            }
            else if (c == ',')
            {
                pauseDuration = commaDelay;
            }

            yield return new WaitForSeconds(pauseDuration);
        }
        
        textStreamCoroutine = null;
    }

    // Method to update student UI with streaming text
    public void UpdateStudentUI(StudentProfile student)
    {
        currentSpeaker = student;  // Store the current speaker
        if (studentNameText != null)
            studentNameText.text = student.name;
        
        // Stop any existing text streaming
        if (textStreamCoroutine != null)
            StopCoroutine(textStreamCoroutine);
        
        // Start new text streaming for reason
        if (reasonText != null)
            textStreamCoroutine = StartCoroutine(StreamText(student.reason, reasonText));
        
        if (studentPortrait != null)
            studentPortrait.sprite = student.displayImage;
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

    public void SetTeacherListViewActive(bool active)
    {
        if (teacherListView != null)
            teacherListView.SetActive(active);
    }

    public void SetLocationListViewActive(bool active)
    {
        if (locationListView != null)
            locationListView.SetActive(active);
    }

    // Combined method for toggling between views
    public void ToggleViews(bool showStudent)
    {
        SetStudentViewActive(showStudent);
        SetHallPassViewActive(!showStudent);
    }

    public void ToggleTeacherListView(bool show)
    {
        SetTeacherListViewActive(show);
        SetStudentViewActive(!show);
    }

    public void ToggleLocationListView(bool show)
    {
        SetLocationListViewActive(show);
        SetStudentViewActive(!show);
    }

    public void HideAllViews()
    {
        SetStudentViewActive(false);
        SetHallPassViewActive(false);
    }

    public void UpdateHallPassUI(HallPassData currentPass)
    {
        if (passStudentNameText != null) passStudentNameText.text = currentPass.studentName;
        if (passDateText != null) passDateText.text = currentPass.date;
        if (passLeaveAtText != null) passLeaveAtText.text = currentPass.leaveAt;
        if (passReturnAtText != null) passReturnAtText.text = currentPass.returnAt;
        if (passVisitingText != null) passVisitingText.text = currentPass.visiting;
        if (passTeacherNameText != null) passTeacherNameText.text = currentPass.signedBy;
    }
    public void UpdateTeacherListUI(string[] teachers)
    {
        List<string> leftColumn = new List<string>();
        List<string> rightColumn = new List<string>();

        for (int i = 0; i < teachers.Length; i++)
        {
            if (i % 2 == 0)
                leftColumn.Add(teachers[i]);
            else
                rightColumn.Add(teachers[i]);
        }

        if (teacherLeftText != null)
            teacherLeftText.text = string.Join("\n", leftColumn);

        if (teacherRightText != null)
            teacherRightText.text = string.Join("\n", rightColumn);
    }

    public void UpdateLocationListUI(string[] locations)
    {

        List<string> leftColumn = new List<string>();
        List<string> rightColumn = new List<string>();

        for (int i = 0; i < locations.Length; i++)
        {
            if (i % 2 == 0)
                leftColumn.Add(locations[i]);
            else
                rightColumn.Add(locations[i]);
        }

        if (locationListLeftText != null)
            locationListLeftText.text = string.Join("\n", leftColumn);

        if (locationListRightText != null)
            locationListRightText.text = string.Join("\n", rightColumn);
    }

    public void UpdateOverlayUI(string dateTime)
    {
        if (dateTimeText != null)
            dateTimeText.text = dateTime;
    }

    public void ShowDecisionFeedback(bool wasCorrect, string truth, int reputation, System.Action onContinue)
    {
        if (feedbackPanel != null)
        {
            feedbackPanel.SetActive(true);
            
            if (feedbackSound != null) {
                AudioClip clipToPlay = wasCorrect ? successSound : failureSound;
                if (clipToPlay != null) {
                    feedbackSound.clip = clipToPlay;
                    feedbackSound.Play();
                }
            }
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