using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    private bool isPassVisible = false;
    private bool isDayComplete = false;
    private float gameTime = 480f; // 8:00 AM in minutes
    
    private UIManager uiManager;
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }



}