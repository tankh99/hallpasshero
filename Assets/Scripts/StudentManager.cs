using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI reasonText;
    public TextMeshProUGUI hallPassText;
    public Image displayImage;

    private StudentProfile[] students;

    void Start()
    {
        LoadStudents();
        ShowRandomStudent();
    }

    void Update()
    {
        
    }

    void LoadStudents()
    {
        students = new StudentProfile[]
        {
            new StudentProfile {
                name = "Charlie",
                reason = "I’m off to meet with the school board about a new student surveillance initiative. You don’t understand, but I have the inside scoop.",
                hallPass = new HallPassData {
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Nurse",
                    date = "2025-03-30",
                    studentName = "Charlie"
                },
                displayImage = Resources.Load<Sprite>("Sprites/boy1")
            },
            new StudentProfile {
                name = "Leo Fernandez",
                reason = "I’m off to meet with the school board about a new student surveillance initiative. You don’t understand, but I have the inside scoop.",
                hallPass = new HallPassData {
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Locker",
                    date = "2025-03-30",
                    studentName = "Leo"
                },
                displayImage = Resources.Load<Sprite>("Sprites/boy2")
            },
            new StudentProfile {
                name = "Maya Patel",
                reason = "I’m off to meet with the school board about a new student surveillance initiative. You don’t understand, but I have the inside scoop.",
                hallPass = new HallPassData {
                    teacherName = "Ms. Smith",
                    leaveAt = "10:00 AM",
                    returnAt = "11:00 AM",
                    visiting = "Bathroom",
                    date = "2025-03-30",
                    studentName = "Maya"
                },
                displayImage = Resources.Load<Sprite>("Sprites/girl1")
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
        nameText.text = randomStudent.name;
        reasonText.text = randomStudent.reason;
        hallPassText.text = randomStudent.hallPass.teacherName + " " + randomStudent.hallPass.leaveAt + " - " + randomStudent.hallPass.returnAt + " " + randomStudent.hallPass.visiting;
        displayImage.sprite = randomStudent.displayImage;
    }
}
