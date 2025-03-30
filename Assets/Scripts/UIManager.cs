using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // [Header("Panels")]
    // public GameObject mainMenuPanel;
    // public GameObject gamePanel;
    // public GameObject dayEndPanel;
    
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
        if (studentView != null) studentView.SetActive(true);
        if (hallPassView != null) hallPassView.SetActive(false);
        
        ShowMainMenu();
        LoadStudents();
        ShowRandomStudent();
    }
    
    public void ShowMainMenu()
    {
        // mainMenuPanel.SetActive(true);
        // gamePanel.SetActive(false);
        // dayEndPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        // mainMenuPanel.SetActive(false);
        // gamePanel.SetActive(true);
        // dayEndPanel.SetActive(false);
        
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
        // mainMenuPanel.SetActive(false);
        // gamePanel.SetActive(false);
        // dayEndPanel.SetActive(true);
        
        dayEndTitleText.text = "Day " + day + " Complete!";
        studentsCheckedText.text = "Students Checked: " + studentsChecked;
        correctDecisionsText.text = "Correct Decisions: " + correctDecisions;
        incorrectDecisionsText.text = "Incorrect Decisions: " + incorrectDecisions;
        finalReputationText.text = "Reputation: " + reputation;
    }
    
    public void NextDay()
    {
        // dayEndPanel.SetActive(false);
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

    void LoadStudents()
    {
        students = new StudentProfile[]
        {
            new StudentProfile {
                name = "Charlie",
                reason = "I've been given access to the school's secret underground bunker. I have to go in for a routine check. No big deal. Just government stuff. You wouldn't understand.",
                hallPass = new HallPassData {
                    excuse = "Meeting with the student council about a school event",
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Nurse",
                    date = "2025-03-30",
                    studentName = "Charlie"
                },
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                isLying = true,
                truth = "Charlie is meeting with a group of students to discuss how to secretly remove a popular vending machine from the school without anyone noticing."
            },
            new StudentProfile {
                name = "Linda",
                reason = "I'm heading to the science lab to finish up an experiment. What is it about? Oh, it's just a small project on controlling the weather. I can't explain any more.",
                hallPass = new HallPassData {
                    excuse = "Working on a science project for class",
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Locker",
                    date = "2025-03-30",
                    studentName = "Linda"
                },
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                isLying = true,
                truth = "Linda is secretly attempting to turn herself into a werewolf using some dubious chemistry experiments. So far, she's only managed to get extremely hairy elbows."
            },
            new StudentProfile {
                name = "Nina",
                reason = "I was told by the janitor that the school's archives are haunted by the ghost of a former headmaster who, before dying, promised to protect the school from a hidden cult that's been inside the walls for over a century. I have to collect the necessary artifacts to make peace with him.",
                hallPass = new HallPassData {
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Bathroom",
                    date = "2025-03-30",
                    studentName = "Nina"
                },
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                isLying = true,
                truth = "Nina is digging through old papers about the school's history, but she's convinced that there are hidden messages in the margins that can only be deciphered by summoning the 'spirit of the headmaster.'",
            }
        };
    }

    public void ShowRandomStudent() {
        if(students.Length == 0) {
            Debug.LogError("No students found");
            return;
        }

        int randomIndex = Random.Range(0, students.Length);
        
        StudentProfile randomStudent = students[randomIndex];
        studentNameText.text = randomStudent.name;
        reasonText.text = randomStudent.reason;
        studentPortrait.sprite = randomStudent.displayImage;
    }

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
        if (passNameText != null) passNameText.text = currentPass.studentName;
        if (passDateText != null) passDateText.text = "Date: " + currentPass.date;
        if (passTimeText != null) passTimeText.text = "Leave At: " + currentPass.leaveAt + " Return At: " + currentPass.returnAt;
        if (passDestinationText != null) passDestinationText.text = "Visiting: " + currentPass.visiting;
        if (passTeacherText != null) passTeacherText.text = "Teacher: " + currentPass.teacherName;
    }

} 