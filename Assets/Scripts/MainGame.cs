using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class MainGame : MonoBehaviour
{
    [Header("Game Settings")]
    public int dayNumber = 1;
    public int studentsChecked = 0;
    public int correctDecisions = 0;

    public int incorrectDecisions = 0;
    public int reputation = 100;
    public int demeritsIssued = 0;
    
    [Header("Student Queue")]
    public int studentsPerDay = 5;
    public float timeBetweenStudents = 3f;
    private int remainingStudents;
    
    private bool isPassVisible = false;
    private bool isTeacherListVisible = false;
    private bool isDayComplete = false;
    private float gameTime = 480f; // 8:00 AM in minutes
    
    private UIManager uiManager;
    private StudentManager studentManager;
    private StudentProfile currentStudent;

    private string[] teachers = {
        "Ms. Smith",
        "Mr. Johnson",
        "Mrs. Williams",
        "Mr. Brown",
        "Mrs. Jones"
    };

    private void Awake() {
        uiManager = FindFirstObjectByType<UIManager>();
        studentManager = FindFirstObjectByType<StudentManager>();
    }

    private void Start() {
        InitializeDay();
    }
    public void SpawnNextStudent()
    {
        if (remainingStudents <= 0)
        {
            EndDay();
            return;
        }

        // Hide hall pass and show student
        if (uiManager != null)
            uiManager.ToggleViews(true);
        isPassVisible = false;

        // Generate random student and pass data
        currentStudent = studentManager.ShowRandomStudent();
        
        remainingStudents--;
        
        // Update UI through UIManager
        if (uiManager != null)
        {
            string timeString = FormatTimeString();
            uiManager.UpdateGameUI(dayNumber, remainingStudents, reputation, timeString);
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
        if (uiManager != null)
        {
            uiManager.ToggleViews(true);
            uiManager.SetHallPassViewActive(false);
        }
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
    

    public void ToggleHallPass()
    {
        isPassVisible = !isPassVisible;
        
        if (uiManager != null)
        {
            uiManager.ToggleViews(!isPassVisible);
            if (isPassVisible)
            {
                uiManager.UpdateHallPassUI(currentStudent.hallPass);

            }
        }

    }

    public void ToggleTeacherList()
    {
        isTeacherListVisible = !isTeacherListVisible;

        if(uiManager != null) {
            uiManager.ToggleTeacherListView(isTeacherListVisible);
            if(isTeacherListVisible) {
                uiManager.UpdateTeacherListUI(teachers);
            }
        }
    }

    public void ApproveStudent()
    {
        bool correctDecision = !currentStudent.isLying;
        ProcessDecision(correctDecision);
    }

    public void DenyStudent()
    {
        bool correctDecision = currentStudent.isLying;
        ProcessDecision(correctDecision);
    }

    private void ProcessDecision(bool correctDecision)
    {
        studentsChecked++;
        
        if (correctDecision)
        {
            correctDecisions++;
            reputation += 5*dayNumber;
        }
        else
        {
            incorrectDecisions++;
            reputation -= 10*dayNumber;
            demeritsIssued++;
        }
        
        // Clamp reputation
        reputation = Mathf.Clamp(reputation, 0, 100);
        
        // Disable buttons immediately
        if (uiManager != null)
        {
            uiManager.SetDecisionButtonsInteractable(false);
            uiManager.SetHallPassViewActive(false);
            uiManager.SetTeacherListViewActive(false);
            // Show feedback and wait for user confirmation
            uiManager.ShowDecisionFeedback(correctDecision, currentStudent.truth, reputation, () => {
                StartCoroutine(WaitAndContinue());
            });
        }
    }

    private IEnumerator WaitAndContinue()
    {
        // Small delay for visual feedback
        yield return new WaitForSeconds(0.5f);
        
        // Re-enable buttons and spawn next student
        if (uiManager != null)
            uiManager.SetDecisionButtonsInteractable(true);
        
        SpawnNextStudent();
    }

    private void EndDay()
    {
        isDayComplete = true;
        
        // Hide game UI
        if (uiManager != null)
        {
            uiManager.HideAllViews();
            uiManager.ShowDayEndPanel(dayNumber, studentsChecked, correctDecisions, reputation);
        }
        
        // Increment day number
        dayNumber++;
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
}