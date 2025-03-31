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
            // new StudentProfile {
            //     name = "Elliot",
            //     reason = "I need to deliver a sealed package to the science department.",
            //     hallPass = new HallPassData {
            //         signedBy = "Ms. Owens",
            //         leaveAt = "8:45 AM",
            //         returnAt = "9:15 AM",
            //         visiting = "Science Department",
            //         date = "2025-03-30",
            //         studentName = "Elliot"
            //     },
            //     isValid = true,
            //     truth = "You notice the package he's holding is shaking, as though something is trying to escape",
            //     displayImage = Resources.Load<Sprite>("Sprites/boy2"),
            //     date = "2025-03-30",
            //     time = "8:50 AM",
            //     minPitch = 0.97f,  // Slightly higher male voice, sounds earnest
            //     maxPitch = 1.02f,
            //     volume = 0.5f
            // },
            // Wrong signedBy, wrong location and wrong time
            new StudentProfile {
                name = "Jenny",
                reason = "I left my phone at the cafeteria, I'd like to take it, please!",
                hallPass = new HallPassData {
                    signedBy = "Mr. Lee",
                    leaveAt = "9:05 AM",
                    returnAt = "9:15 AM",
                    visiting = "Restroom",
                    date = "2025-03-30",
                    studentName = "Jenny"
                },
                isValid = false,
                isGirl = true,
                truth = "Jenny's hall pass didn't mention that she was going to the restroom.",
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                date = "2025-03-30",
                time = "9:08 AM",
                 minPitch = 1.55f,  // Medium female pitch
                maxPitch = 1.65f,
                volume = 0.6f
            },
            new StudentProfile {
                name = "Liam",
                reason = "AHHHH, I'M LITERALLY DYING AND YOU'RE STOPPING ME FROM GOING TO THE NURSE??!?!",
                hallPass = new HallPassData {
                    signedBy = "Dr. Simmons",
                    leaveAt = "12:30 PM",
                    returnAt = "1:00 PM",
                    visiting = "Nurse",
                    date = "2025-03-30",
                    studentName = "Liam"
                },
                isValid = false,
                truth = "Liam accidentally stabbed his thumb with a staple and decided it was a great chance to forge a hall pass and skip class. Unfortunately, he forgot that Mr Simmons is not a doctor.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-30",
                time = "12:35 PM",
                minPitch = 0.7f,   // Slightly higher male pitch due to panic
                maxPitch = 1.3f,
                volume = 1.0f  
            },
            // Wrong signedBy (should be Mr. Simmons)
            new StudentProfile {
                name = "Sophia",
                reason = "Here, take this...*slides a bag of weed to you*. I need to get to the bookstore for an errand. Pronto.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Harper",
                    leaveAt = "2:10 PM",
                    returnAt = "2:30 PM",
                    visiting = "Bookstore",
                    date = "2025-03-30",
                    studentName = "Sophia"
                },
                isValid = true,
                isGirl = true,
                truth = "Sophia's turning over a new leaf. She's been saving up to buy 'Beyond Addiction: A Guide to Recovery'. If only she stopped giving away bags of weed like a charity.",
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                date = "2025-03-30",
                time = "2:12 PM",
                minPitch = 1.5f,   // Lower female pitch, trying to be discrete
                maxPitch = 1.6f,
                volume = 0.35f 
            },
            
            // DAY 2

            new StudentProfile {
                name = "Loro",
                reason = "I need to get an early lunch from the cafeteria.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Lee",
                    leaveAt = "9:35 AM",
                    returnAt = "10:35 AM",
                    visiting = "Cafeteria",
                    date = "2025-03-31",
                    studentName = "Loro"
                },
                isValid = true,
                isGirl = true,
                truth = "Loro ate her lunch and found Jenny's phone. She gave it back to Jenny for a random of $25",
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                date = "2025-03-31",
                time = "9:40 AM",
                minPitch = 1.75f,  // Higher pitch, confident tone
                maxPitch = 1.85f,
                volume = 0.5f 
            },

            // Wrong student name, wrong signedBy
            new StudentProfile {
                name = "Olivia",
                reason = "I need to deliver an URGENT report to Mr. Murdock in the science department.",
                hallPass = new HallPassData {
                    signedBy = "Ms. Lee",
                    leaveAt = "11:30 AM",
                    returnAt = "2:00 PM",
                    visiting = "Science Department",
                    date = "2025-03-31",
                    studentName = "Oliver",
                },
                isValid = false,
                isGirl = true,
                displayImage = Resources.Load<Sprite>("Sprites/girl1"),
                truth = "Olivia and Oliver are twins. She's using his hall pass because he got stuck inside a vending machine trying to get free snacks. The 'urgent report' is just a distraction while she tries to figure out how to get him unstuck before the next class. Mr. Murdock teaches physics - she's hoping he knows something about leverage.",
                date = "2025-03-31",
                time = "1:35 PM",
                minPitch = 1.6f,   // Medium-high female pitch
                maxPitch = 1.7f,
                volume = 0.8f  
            },
            // Wrong signedBy, wrong hall pass date
            new StudentProfile {
                name = "Tyler",
                reason = "I got permission from Mr. Simmons to go to the library to research on quantum physics for my project.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Simmons",
                    leaveAt = "2:00 AM",
                    returnAt = "2:45 AM",
                    visiting = "Library",
                    date = "2025-02-31",
                    studentName = "Tyler"
                },
                isValid = false,
                truth = "Tyler isn’t researching quantum physics. He just wants to skip class",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-03-31",
                time = "2:05 AM",
                minPitch = 0.95f,  // Standard male pitch
                maxPitch = 1.0f,
                volume = 0.6f 
            },

            // Day 3

            // Wrong signedBy
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
                truth = "Nina accidentally left her used tampons on the gym sink. Unfortunately for her, Mr. Cruz is in Cancun right now",
                displayImage = Resources.Load<Sprite>("Sprites/girl2"),
                date = "2025-04-01",
                time = "12:23 PM",
                minPitch = 1.5f,   // High pitch due to urgency
                maxPitch = 2.5f,
                volume = 1.0f 
            },
            new StudentProfile {
                name = "Max",
                reason = "So, like...I need to speak to the the principal...\n\n*You notice he's packing heat in his back pocket*",
                hallPass = new HallPassData {
                    signedBy = "Mr. Carson",
                    leaveAt = "1:15 PM",
                    returnAt = "1:45 PM",
                    visiting = "Principal's Office",
                    date = "2025-04-01",
                    studentName = "Max"
                },
                isValid = true,
                truth = "Thankfully, both he and the principal came out of the office laughing after a while.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-04-01",
                time = "1:18 PM",
                 minPitch = 0.95f,  // Deep male voice, trying to sound serious
                maxPitch = 0.98f,
                volume = 0.25f  
            },
            // Wrong visiting location
            new StudentProfile {
                name = "Ryan",
                reason = "Oh, what am I out for? It's nothing much, just asked by Mr. Lion for the usual help with IT stuff.",
                hallPass = new HallPassData {
                    signedBy = "Mr. Lion",
                    leaveAt = "3:00 PM",
                    returnAt = "3:30 PM",
                    visiting = "shhhh...It's a secret",
                    date = "2025-03-30",
                    studentName = "Ryan"
                },
                isValid = false,
                truth = "Ryan is actually a spy for the CIA. He's been spying on the school for the past month and he's finally ready to strike.",
                displayImage = Resources.Load<Sprite>("Sprites/boy1"),
                date = "2025-04-01",
                time = "3:05 PM",
                minPitch = 0.98f,  // Professional male voice
                maxPitch = 1.03f,
                volume = 0.4f 
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
