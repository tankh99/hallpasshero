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

    private int currentStudentIndex = 0;

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
        students = new StudentProfile[] {
            new StudentProfile {
                name = "Elliot",
                reason = "I need to deliver a sealed package to the chemistry lab.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Owens",
                    leaveAt = "8:45 AM",
                    returnAt = "9:15 AM",
                    visiting = "Chemistry Lab",
                    date = "2025-03-30",
                    studentName = "Elliot"
                },
                isValid = true,
                truth = "Elliot is indeed delivering something to the chemistry lab, but he has no idea the package contains a live frog that has been injected with caffeine.",
                displayImage = Resources.Load<Sprite>("Sprites/boy2"),
                date = "2025-03-30",
                time = "8:50 AM"
            },
            new StudentProfile {
                name = "Jenny",
                reason = "I left my phone at the cafeteria, I'd like to take it.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Lee",
                    leaveAt = "9:05 AM",
                    returnAt = "9:15 AM",
                    visiting = "Restroom",
                    date = "2025-03-30",
                    studentName = "Jenny"
                },
                isValid = false,
                isGirl = true,
                truth = "Unfortunately, Jenny's hall pass didn't mention that she was going to the restroom.",
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                date = "2025-03-30",
                time = "9:08 AM"
            },
            new StudentProfile {
                name = "Loro",
                reason = "I need to get an early lunch from the cafeteria.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Lee",
                    leaveAt = "9:35 AM",
                    returnAt = "10:35 AM",
                    visiting = "Cafeteria",
                    date = "2025-03-30",
                    studentName = "Loro"
                },
                isValid = true,
                isGirl = true,
                truth = "Jenny eventually bought her phone back from Loro for a ransom of $25",
                displayImage = Resources.Load<Sprite>("Sprites/boy2"),
                date = "2025-03-30",
                time = "9:40 AM"
            },
            new StudentProfile {
                name = "Tyler",
                reason = "I'm going to the library to research quantum physics for my project.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Wiles",
                    leaveAt = "11:00 AM",
                    returnAt = "11:45 AM",
                    visiting = "Library",
                    date = "2025-02-30",
                    studentName = "Tyler"
                },
                isValid = false,
                truth = "Tyler isn’t researching quantum physics. He just wants to skip class",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
                time = "11:05 AM"
            },
            // New day
            new StudentProfile {
                name = "Olivia",
                reason = "I need to deliver a report to Mr. Murdock in the science department.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Thatcher",
                    leaveAt = "11:30 AM",
                    returnAt = "2:00 PM",
                    visiting = "Science Department",
                    date = "2025-03-31",
                    studentName = "Oliver",
                },
                isValid = false,
                isGirl = true,
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                truth = "Olivia enlisted Oliver to help get a hall pass to the science department, claiming she was delivering a report to Mr. Murdock. In reality, she’d spilled coffee on the report and needed Oliver to distract Murdock long enough to swap it out with a clean copy. It wasn’t an urgent delivery — just a clever cover-up",
                date = "2025-03-31",
                time = "1:35 PM"
            },
            new StudentProfile {
                name = "Liam",
                reason = "AHHHH, I'M LITERALLY DYING AND YOU'RE STOPPING ME FROM GOING TO THE NURSE??!?!.",
                hallPass = new HallPassData {
                    signedBy = "Dr. Simmons",
                    leaveAt = "12:30 PM",
                    returnAt = "1:00 PM",
                    visiting = "Nurse",
                    date = "2025-03-31",
                    studentName = "Liam"
                },
                isValid = true,
                truth = "Liam's bleeding from his thumb, probably from playing too much with the stapler.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-31",
                time = "12:35 PM"
            },
            new StudentProfile {
                name = "Sophia",
                reason = "Here, take this...*slides a bag of weed to you*. I need to get to the bookstore for an errand. Pronto.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Harper",
                    leaveAt = "2:10 PM",
                    returnAt = "2:30 PM",
                    visiting = "Bookstore",
                    date = "2025-03-31",
                    studentName = "Sophia"
                },
                isValid = true,
                isGirl = true,
                truth = "Sophia's turning over a new leaf. She's been saving up to buy 'Beyond Addiction: A Guide to Recovery'. If only she stopped giving away bags of weed like a charity.",
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                date = "2025-03-31",
                time = "2:12 PM"
            },
            // Day 3

            // NOTE: Nina is supposed to be lying because her teacher's signedBy is not present
            new StudentProfile {
                name = "Nina",
                reason = "You literally do not understand, this is a matter of life and death. So STOP BLOCKING MY WAY TO THE GYM. OH MY GOD.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Cruz",
                    leaveAt = "12:20 PM",
                    returnAt = "12:40 PM",
                    visiting = "Gym",
                    date = "2025-04-01",
                    studentName = "Nina"
                },
                isValid = false,
                isGirl = true,
                truth = "Nina accidentally left her used tampons on the gym sink. Unfortunately for her, Mr. Cruz is vacationing in Cancun right now",
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                date = "2025-03-30",
                time = "12:23 PM"
            },
            new StudentProfile {
                name = "Max",
                reason = "I need to speak to the the principal...",
                hallPass = new HallPassData {
                    signedBy = "Mr. Carson",
                    leaveAt = "1:15 PM",
                    returnAt = "1:45 PM",
                    visiting = "Principal's Office",
                    date = "2025-04-01",
                    studentName = "Max"
                },
                isValid = true,
                truth = "You notice he's packing heat in his back pocket. Thankfully, seems like negotiations went well as he both he and the principal came out of the office laughing.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-04-01",
                time = "1:18 PM"
            },
            new StudentProfile {
                name = "Ryan",
                reason = "I was asked by Mr. Green to setup some stuff at the server room.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Green",
                    leaveAt = "3:00 PM",
                    returnAt = "3:30 PM",
                    visiting = "Server Room",
                    date = "2025-03-30",
                    studentName = "Ryan"
                },
                isValid = false,
                truth = "Ryan is actually a spy for the CIA. He's been spying on the school for the past month and he's finally ready to strike.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-04-01",
                time = "3:05 PM"
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
            uiManager.UpdateStudentUI(randomStudent);
            uiManager.UpdateOverlayUI(randomStudent.date + " " + randomStudent.time);
        }
        return randomStudent;
    }

    public StudentProfile ShowNextStudent() {
        if (currentStudentIndex >= students.Length) {
            Debug.LogError("No more students to show");
            return null;
        }
        StudentProfile nextStudent = students[currentStudentIndex++];

        if (uiManager != null) {
            uiManager.UpdateStudentUI(nextStudent);
            uiManager.UpdateOverlayUI(nextStudent.date + " " + nextStudent.time);
        }
        return nextStudent;
    }

}
