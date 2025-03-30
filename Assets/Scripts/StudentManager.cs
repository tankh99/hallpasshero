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
    private UIManager uiManager;


    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        LoadStudents();

        // ShowRandomStudent();
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
                isGirl = false,
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
                isGirl = true,
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
                isGirl = true,
                truth = "Nina is digging through old papers about the school's history, but she's convinced that there are hidden messages in the margins that can only be deciphered by summoning the 'spirit of the headmaster.'"
            }
        };
    }

    public StudentProfile ShowRandomStudent() {
        if(students.Length == 0) {
            Debug.LogError("No students found");
            return null;
        }

        int randomIndex = Random.Range(0, students.Length);
        StudentProfile randomStudent = students[randomIndex];
        
        if (uiManager != null) {
            uiManager.UpdateStudentUI(randomStudent.name, randomStudent.reason, 
                randomStudent.displayImage, randomStudent);
        }
        return randomStudent;
    }

}
