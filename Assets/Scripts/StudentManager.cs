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
                    leaveAt = "10:45 AM",
                    returnAt = "11:15 AM",
                    visiting = "Chemistry Lab",
                    date = "2025-03-30",
                    studentName = "Elliot"
                },
                isValid = true,
                truth = "Elliot is indeed delivering something to the chemistry lab, but he has no idea the package contains a live frog that has been injected with caffeine.",
                displayImage = Resources.Load<Sprite>("Sprites/boy2"),
                date = "2025-03-30",
                time = "10:50 AM"
            },
            new StudentProfile {
                name = "Jenny",
                reason = "I left something at the cafeteria, I'd like to take it.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Lee",
                    leaveAt = "9:05 AM",
                    returnAt = "9:15 AM",
                    visiting = "Restroom",
                    date = "2025-03-30",
                    studentName = "Jenny"
                },
                isValid = true,
                isGirl = true,
                truth = "Jenny was speaking the truth, unfortunately, her hall pass didn't mention that she was going to the restroom.",
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                date = "2025-03-30",
                time = "9:08 AM"
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
                isValid = true,
                truth = "Tyler isn’t researching quantum physics. He just wants to skip class",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
                time = "11:05 AM"
            },
            new StudentProfile {
                name = "Olivia",
                reason = "I need to deliver a report to Mr. Murdock in the science department.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Thatcher",
                    leaveAt = "1:30 PM",
                    returnAt = "2:00 PM",
                    visiting = "Science Department",
                    date = "2025-03-30",
                    studentName = "Oliver",
                },
                isValid = true,
                isGirl = true,
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                truth = "Olivia enlisted Oliver to help get a hall pass to the science department, claiming she was delivering a report to Mr. Murdock. In reality, she’d spilled coffee on the report and needed Oliver to distract Murdock long enough to swap it out with a clean copy. It wasn’t an urgent delivery — just a clever cover-up",
                date = "2025-03-30",
                time = "1:35 PM"
            },
            new StudentProfile {
                name = "Liam",
                reason = "I'm going to the nurse for a quick check-up.",
                hallPass = new HallPassData {
                    signedBy = "Dr. Simmons",
                    leaveAt = "12:30 PM",
                    returnAt = "1:00 PM",
                    visiting = "Nurse",
                    date = "2025-03-30",
                    studentName = "Liam"
                },
                isValid = true,
                truth = "Liam's trip to the nurse is entirely unnecessary. He just wants to take a nap in the comfy nurse's office, having been told once by Dr. Simmons that the chairs 'are as close to clouds as possible.'",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
                time = "12:35 PM"
            },
            new StudentProfile {
                name = "Sophia",
                reason = "I need to pick up a new textbook from the bookstore.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Harper",
                    leaveAt = "2:10 PM",
                    returnAt = "2:30 PM",
                    visiting = "Bookstore",
                    date = "2025-03-30",
                    studentName = "Sophia"
                },
                isValid = false,
                isGirl = true,
                truth = "Sophia isn’t buying a textbook. She’s really going to the *allegedly* 'secret' underground club beneath the bookstore that promises students free pizza in exchange for 'intellectual debates.' Sophia believes she’s 'one step away' from winning a lifetime supply of pizza.",
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                date = "2025-03-30",
                time = "2:12 PM"
            },
            new StudentProfile {
                name = "Max",
                reason = "I need to fetch some papers from the principal's office.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Carson",
                    leaveAt = "10:15 AM",
                    returnAt = "10:45 AM",
                    visiting = "Principal's Office",
                    date = "2025-03-30",
                    studentName = "Max"
                },
                isValid = true,
                truth = "Max isn’t fetching papers from the principal's office. He’s going there to demand a meeting with the principal about the recent 'injustice' of not getting an extra-long lunch break. The meeting is more about his 'inexplicable thirst for 10 minutes more of freedom' than any real paperwork.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
                time = "10:18 AM"
            },
            new StudentProfile {
                name = "Nina",
                reason = "I need to speak with Mr. Martinez in the gym about a special event.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Cruz",
                    leaveAt = "12:20 PM",
                    returnAt = "12:40 PM",
                    visiting = "Gym",
                    date = "2025-03-30",
                    studentName = "Nina"
                },
                isValid = true,
                isGirl = true,
                truth = "Nina doesn’t need to talk to Mr. Martinez. She is simply trying to find her gym bag, which she claims to have accidentally swapped with a random student’s bag last week and now needs to find it before the *'gym soap incident'** is discovered.",
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                date = "2025-03-30",
                time = "12:23 PM"
            },
            new StudentProfile {
                name = "Ryan",
                reason = "I need to drop off some papers at the IT office.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Green",
                    leaveAt = "3:00 PM",
                    returnAt = "3:30 PM",
                    visiting = "IT Office",
                    date = "2025-03-30",
                    studentName = "Ryan"
                },
                isValid = true,
                truth = "Ryan isn’t dropping off papers. He’s running an underground 'computer sabotage' operation involving 'borrowing' computer cables from the IT office to build an impromptu robot. He’s only halfway through designing a 'self-moving stapler,' but his ambition is unmatched.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
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
