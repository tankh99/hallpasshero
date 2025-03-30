using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int dayNumber = 1;
    public int studentsChecked = 0;
    public int correctDecisions = 0;
    public int incorrectDecisions = 0;
    public int reputation = 100;
    public int demeritsIssued = 0;
    
    [Header("Student Queue")]
    public int studentsPerDay = 10;
    public float timeBetweenStudents = 3f;
    private int remainingStudents;
    
    [Header("UI References")]
    public GameObject studentView;
    public GameObject hallPassView;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI studentsRemainingText;
    public Button approveButton;
    public Button denyButton;
    public Button inspectPassButton;
    
    [Header("Student Data")]
    public Student currentStudent;
    public TextMeshProUGUI studentNameText;
    public TextMeshProUGUI studentGradeText;
    public Image studentPortrait;
    
    [Header("Hall Pass Data")]
    public HallPass currentPass;
    public TextMeshProUGUI passNameText;
    public TextMeshProUGUI passDateText;
    public TextMeshProUGUI passTimeText;
    public TextMeshProUGUI passDestinationText;
    public TextMeshProUGUI passTeacherText;
    public Image passStamp;
    
    private bool isPassVisible = false;
    private bool isDayComplete = false;
    private float gameTime = 480f; // 8:00 AM in minutes
    
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        // No need to initialize day here anymore, UIManager will call it
    }

    private void Update()
    {
        if (!isDayComplete)
        {
            UpdateGameTime();
        }
    }

    public void InitializeDay()
    {
        // Reset day variables
        studentsChecked = 0;
        correctDecisions = 0;
        incorrectDecisions = 0;
        remainingStudents = studentsPerDay;
        gameTime = 480f; // 8:00 AM
        isDayComplete = false;
        
        // Update UI through UIManager
        if (uiManager != null)
        {
            string timeString = FormatTimeString();
            uiManager.UpdateGameUI(dayNumber, remainingStudents, reputation, timeString);
        }
        
        // Show student view, hide hall pass view
        if (studentView != null) studentView.SetActive(true);
        if (hallPassView != null) hallPassView.SetActive(false);
        isPassVisible = false;
        
        // Spawn first student
        SpawnNextStudent();
    }

    private void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        
        // Update UI through UIManager
        if (uiManager != null)
        {
            string timeString = FormatTimeString();
            uiManager.UpdateGameUI(dayNumber, remainingStudents, reputation, timeString);
        }
    }
    
    private string FormatTimeString()
    {
        int hours = Mathf.FloorToInt(gameTime / 60f);
        int minutes = Mathf.FloorToInt(gameTime % 60f);
        string ampm = hours >= 12 ? "PM" : "AM";
        hours = hours > 12 ? hours - 12 : hours;
        hours = hours == 0 ? 12 : hours;
        return string.Format("{0}:{1:00} {2}", hours, minutes, ampm);
    }

    public void SpawnNextStudent()
    {
        if (remainingStudents <= 0)
        {
            EndDay();
            return;
        }

        // Hide hall pass and show student
        if (studentView != null) studentView.SetActive(true);
        if (hallPassView != null) hallPassView.SetActive(false);
        isPassVisible = false;

        // Generate random student and pass data
        GenerateRandomStudent();
        
        remainingStudents--;
        
        // Update UI through UIManager
        if (uiManager != null)
        {
            string timeString = FormatTimeString();
            uiManager.UpdateGameUI(dayNumber, remainingStudents, reputation, timeString);
        }
    }

    private void GenerateRandomStudent()
    {
        // In a full implementation, this would create more varied students and hall passes
        string[] firstNames = { "Emma", "Liam", "Olivia", "Noah", "Ava", "William", "Sophia", "James", "Isabella", "Logan" };
        string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
        string[] destinations = { "Bathroom", "Nurse", "Office", "Library", "Counselor" };
        string[] teachers = { "Ms. Johnson", "Mr. Smith", "Mrs. Wilson", "Mr. Davis", "Ms. Brown" };
        
        // Create student
        currentStudent = new Student();
        currentStudent.firstName = firstNames[Random.Range(0, firstNames.Length)];
        currentStudent.lastName = lastNames[Random.Range(0, lastNames.Length)];
        currentStudent.grade = Random.Range(9, 13);
        
        // Create hall pass (80% chance to be valid)
        currentPass = new HallPass();
        currentPass.studentName = currentStudent.firstName + " " + currentStudent.lastName;
        currentPass.date = "5/" + Random.Range(1, 31) + "/2023";
        
        int passHour = Mathf.FloorToInt(gameTime / 60f);
        int passMinute = Mathf.FloorToInt(gameTime % 60f);
        
        // Create a random discrepancy (20% chance)
        bool hasDiscrepancy = Random.value < 0.2f;
        
        if (hasDiscrepancy)
        {
            // Randomly choose a type of discrepancy
            int discrepancyType = Random.Range(0, 3);
            
            switch (discrepancyType)
            {
                case 0: // Wrong name
                    currentPass.studentName = firstNames[Random.Range(0, firstNames.Length)] + " " + 
                                            lastNames[Random.Range(0, lastNames.Length)];
                    break;
                case 1: // Wrong date
                    currentPass.date = "4/" + Random.Range(1, 31) + "/2023";
                    break;
                case 2: // Wrong time
                    passHour = (passHour + Random.Range(1, 3)) % 12;
                    passMinute = Random.Range(0, 60);
                    break;
            }
        }
        
        string ampm = passHour >= 12 ? "PM" : "AM";
        passHour = passHour > 12 ? passHour - 12 : passHour;
        passHour = passHour == 0 ? 12 : passHour;
        currentPass.time = string.Format("{0}:{1:00} {2}", passHour, passMinute, ampm);
        
        currentPass.destination = destinations[Random.Range(0, destinations.Length)];
        currentPass.teacherName = teachers[Random.Range(0, teachers.Length)];
        currentPass.isValid = !hasDiscrepancy;
        
        // Update UI
        if (studentNameText != null) studentNameText.text = currentStudent.firstName + " " + currentStudent.lastName;
        if (studentGradeText != null) studentGradeText.text = "Grade " + currentStudent.grade;
        
        // Update hall pass UI as well, even though it's not visible yet
        UpdateHallPassUI();
    }
    
    private void UpdateHallPassUI()
    {
        if (passNameText != null) passNameText.text = currentPass.studentName;
        if (passDateText != null) passDateText.text = "Date: " + currentPass.date;
        if (passTimeText != null) passTimeText.text = "Time: " + currentPass.time;
        if (passDestinationText != null) passDestinationText.text = "Destination: " + currentPass.destination;
        if (passTeacherText != null) passTeacherText.text = "Teacher: " + currentPass.teacherName;
    }

    public void ToggleHallPass()
    {
        isPassVisible = !isPassVisible;
        
        if (studentView != null) studentView.SetActive(!isPassVisible);
        if (hallPassView != null) hallPassView.SetActive(isPassVisible);
        
        if (isPassVisible)
        {
            UpdateHallPassUI();
        }
    }

    public void ApproveStudent()
    {
        bool correctDecision = currentPass.isValid;
        ProcessDecision(correctDecision);
    }

    public void DenyStudent()
    {
        bool correctDecision = !currentPass.isValid;
        ProcessDecision(correctDecision);
    }

    private void ProcessDecision(bool correctDecision)
    {
        studentsChecked++;
        
        if (correctDecision)
        {
            correctDecisions++;
            reputation += 5;
        }
        else
        {
            incorrectDecisions++;
            reputation -= 10;
            demeritsIssued++;
        }
        
        // Clamp reputation
        reputation = Mathf.Clamp(reputation, 0, 100);
        
        // In a full implementation, show feedback before continuing
        StartCoroutine(WaitAndContinue());
    }

    private IEnumerator WaitAndContinue()
    {
        // Disable buttons during wait
        if (approveButton != null) approveButton.interactable = false;
        if (denyButton != null) denyButton.interactable = false;
        if (inspectPassButton != null) inspectPassButton.interactable = false;
        
        yield return new WaitForSeconds(1.0f);
        
        // Re-enable buttons
        if (approveButton != null) approveButton.interactable = true;
        if (denyButton != null) denyButton.interactable = true;
        if (inspectPassButton != null) inspectPassButton.interactable = true;
        
        SpawnNextStudent();
    }

    private void EndDay()
    {
        isDayComplete = true;
        
        // Hide game UI
        if (studentView != null) studentView.SetActive(false);
        if (hallPassView != null) hallPassView.SetActive(false);
        
        // Show day end panel through UIManager
        if (uiManager != null)
        {
            uiManager.ShowDayEndPanel(dayNumber, studentsChecked, correctDecisions, incorrectDecisions, reputation);
        }
        
        // Increment day number
        dayNumber++;
    }
}

[System.Serializable]
public class Student
{
    public string firstName;
    public string lastName;
    public int grade;
    // In a full implementation, add more properties like appearance, history, etc.
}

[System.Serializable]
public class HallPass
{
    public string studentName;
    public string date;
    public string time;
    public string destination;
    public string teacherName;
    public bool isValid;
    // In a full implementation, add more properties for additional validation
} 