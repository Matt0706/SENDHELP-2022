using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class QuizContents : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    public AudioSource source;
    public AudioClip clip;

    static Random rnd = new Random();

    int currQuestion = rnd.Next(20) + 2;
    int currLevel = 1;
    Button thisButton;

    public Animator anim;
    public int delayTime;

    Button nextButton;

    bool firstTry = true;
    bool finished = false;
    public GameObject caesarCipherImage;

    List<bool> level1UserCorrectness = new List<bool>()
    {
        //false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
        false, //12
        false, //13
        false, //14
        false, //15
        false, //16
        false, //17
        false, //18
        false, //19
        false, //20
        false //21
    };

    List<bool> level1UsedQuestions = new List<bool>() 
    {    
        //false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
        false, //12
        false, //13
        false, //14
        false, //15
        false, //16
        false, //17
        false, //18
        false, //19
        false, //20
        false //21
    };

    Text questionNumber, question, option1, option2, option3, option4, option5;
    Button button1, button2, button3, button4, button5;

    List<string> level1questionList = new List<string>()
    {
        //"What should a good password include?",  //THIS IS MULTIPLE CORRECT, SO IGNORE FOR NOW
        "Which of the following is the strongest password?",
        "In general, the use of a passphrase is considered a best practice. Which of the following is the strongest example of a passphrase?",
        "By following best practices, what is the best way to create and store passwords?",
        "It is generally a good strategy to reuse passwords across multiple sites.",
        "The study of hiding data from anyone but it's sender and recipients is called",
        "Can you decode this?",
        "Which of the following is not an aspect of multi-factor authentication?",
        "Requiring a fingerprint and a password is an example of:",
        "Where should you change your password if a site that uses it has a data breach?",
        "It is ethical behavior to rely on transitive trust and passwords.",
        "What is cryptography used for?",
        "Cryptography is the same as encryption.",
        "You can only use a phone number for Multi-Factor Authentication",
        "Multi-factor authentication can only come in the form of numbers",
        "PIG LATIN is an ancient form of cryptography",
        "What is one of the first examples of cryptography?",
        "It is illegal to encrypt messages",
        "What percentage of hacking related breaches are stolen or weak passwords?",
        "What should you do if you receieve a security code when you are not trying to log in?",
        "When a new software update comes out, what should you do?"
    };

    List<List<string>> level1questionChoiceList = new List<List<string>>()
    {
        /*new List<string> { // IGNORE FIRST QUESTION
            "Uppercase letters",
            "Lowercase letters",
            "Special characters",
            "Numbers",
            "Spaces"},  //1 */
        
        new List<string> {
            "password",
            "Admin123",
            "p@ssw0rd!",
            "p@sSw0rD!"},  //2
        
        new List<string> {
            "Admin123",
            "P@ssw0rd!",
            "Qwerty123!",
            "i<3C$4L!fe"},  //3
        
        new List<string> {
            "Sticky Note",
            "Excel",
            "Password Manager",
            "In your head"},  //4

        new List<string> {
            "True",
            "False"},  //5

        new List<string> {
            "Encryption",
            "Cryptography",
            "Blockchain",
            "Firewall"},  //6

        new List<string> {
            "A RED CAT",
            "A PET CAT",
            "I CAN WIN",
            "I PET DOG"},  //7

        new List<string> {
            "Something you know",
            "Something you are",
            "Something you read",
            "Something you have"},  //8

        new List<string> {
            "Single-factor authentication",
            "Two-factor authentication",
            "Three-factor authentication",
            "None of the above"},  //9

        new List<string> {
            "None. You're probably safe",
            "On that specific site",
            "All sites using that password"},  //10

        new List<string> {
            "True",
            "False"},  //11

        new List<string> {
            "Provides a secure form of communication", //12
            "To discover clues about a crime that has been committed",
            "To make remembering your passowrd easier",
            "Hiding code in plain sight of others"},

        new List<string> {
            "True",
            "False"}, //13

        new List<string> {
            "True",
            "False"}, //14

        new List<string> {
            "True",
            "False"}, //15

        new List<string> {
            "True", //16
            "False"},
            
        new List<string> {
            "Rosetta Stone", //17
            "Federalist Papers",
            "Caesar Cipher",
            "Bitcoin"},
            
        new List<string> {
            "True",
            "False"}, //18
            
        new List<string> {
            "93",
            "81", //19
            "76",
            "69"},
            
        new List<string> {
            "Contact the IT department", //20
            "Do nothing it's probably fine",
            "Hack the person trying to log in",
            "Use the code you were given"},
            
        new List<string> {
            "Download it immediately",
            "Read the new things added to the update", //21
            "Read the ToS",
            "Remind me tomorrow"}
    };

    List<string> level1questionAnswerList = new List<string>()
    {
        //"Uppercase letters",  //1
        "p@sSw0rD!",  //2
        "i<3C$4L!fe",  //3
        "Password Manager",  //4
        "False", //5
        "Cryptography", //6
        "A PET CAT", //7
        "Something you read", //8
        "Two-factor authentication", //9
        "All sites using that password", //10
        "False", //11
        "Provides a secure form of communication", //12
        "False", //13
        "False", //14
        "False", //15
        "True", //16
        "Rosetta Stone", //17
        "False", //18
        "81", //19
        "Contact the IT department", //20
        "Read the new things added to the update" //21

    };

    void Start()
    {
        button1 = GameObject.Find("[Button] Button 1").GetComponent<Button>();
        button2 = GameObject.Find("[Button] Button 2").GetComponent<Button>();
        button3 = GameObject.Find("[Button] Button 3").GetComponent<Button>();
        button4 = GameObject.Find("[Button] Button 4").GetComponent<Button>();
        button5 = GameObject.Find("[Button] Button 5").GetComponent<Button>();

        UpdateDisplay();
        nextButton = GameObject.Find("[Button] Next").GetComponent<Button>();
        nextButton.interactable = false;
    }

    void UpdateDisplay()
    {
        ResetButtonColor();

        int usedQuestions = 0;
        foreach (bool used in level1UsedQuestions)
            if(used)
                usedQuestions++;
        if(usedQuestions < 10) {
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            button4.gameObject.SetActive(true);
            button5.gameObject.SetActive(true);

            questionNumber = GameObject.Find("[Text] Question Number").GetComponent<Text>();
            question = GameObject.Find("[Text] Question").GetComponent<Text>();
            option1 = GameObject.Find("[Text] Option 1").GetComponent<Text>();
            option2 = GameObject.Find("[Text] Option 2").GetComponent<Text>();
            option3 = GameObject.Find("[Text] Option 3").GetComponent<Text>();
            option4 = GameObject.Find("[Text] Option 4").GetComponent<Text>();
            option5 = GameObject.Find("[Text] Option 5").GetComponent<Text>();

            button1 = GameObject.Find("[Button] Button 1").GetComponent<Button>();
            button2 = GameObject.Find("[Button] Button 2").GetComponent<Button>();
            button3 = GameObject.Find("[Button] Button 3").GetComponent<Button>();
            button4 = GameObject.Find("[Button] Button 4").GetComponent<Button>();
            button5 = GameObject.Find("[Button] Button 5").GetComponent<Button>();


            List<Text> optionsList = new List<Text>() { option1, option2, option3, option4, option5 };
            List<Button> buttonList = new List<Button>() { button1, button2, button3, button4, button5 };
            level1UsedQuestions[currQuestion - 2] = true;
            while(level1UsedQuestions[currQuestion - 2] == true) {
                currQuestion = rnd.Next(20) + 1;
            }
            Debug.Log("CURR QUESTION: " + currQuestion);
            List<string> thisQuestion = level1questionChoiceList[currQuestion - 2];

            questionNumber.text = "Question " + (usedQuestions + 1);
            question.text = level1questionList[currQuestion - 2];

            int i = 0;
            while(i < level1questionChoiceList[currQuestion-2].Count){
                optionsList[i].text = (i +1) + ") " + thisQuestion[i];
                i++;
            }

            caesarCipherImage.gameObject.SetActive(false);
            switch(currQuestion) {
                case(1):
                    Debug.Log("All Options are used in question 1");
                    break;
                case(2):
                    button5.gameObject.SetActive(false);
                    break;
                case(3):
                    button5.gameObject.SetActive(false);
                    break;
                case(4):
                    button5.gameObject.SetActive(false);
                    break;
                case(5):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(6):
                    button5.gameObject.SetActive(false);
                    break;
                case(7):
                    button5.gameObject.SetActive(false);
                    caesarCipherImage.gameObject.SetActive(true);
                    break;
                case(8):
                    button5.gameObject.SetActive(false);
                    break;
                case(9):
                    button5.gameObject.SetActive(false);
                    break;
                case(10):
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(11):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(12):
                    button5.gameObject.SetActive(false);
                    break;
                case(13):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(14):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(15):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(16):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(17):
                    button5.gameObject.SetActive(false);
                    break;
                case(18):
                    button3.gameObject.SetActive(false);
                    button4.gameObject.SetActive(false);
                    button5.gameObject.SetActive(false);
                    break;
                case(19):
                    button5.gameObject.SetActive(false);
                    break;
                case(20):
                    button5.gameObject.SetActive(false);
                    break;
                case(21):
                    button5.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void CheckAnswer(Button buttonObj)
    {
        string rightAnswer = level1questionAnswerList[currQuestion - 2];

        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        string userChoiceStr = buttonText.text;
        userChoiceStr = userChoiceStr.Substring(3);
        string sceneName = SceneManager.GetActiveScene().name;
        thisButton = buttonObj;
        ColorBlock cb = thisButton.colors;

        if ( userChoiceStr.Equals(rightAnswer) )
        {
            if (firstTry)
            {
                level1UserCorrectness[currQuestion - 2] = true;
            }
            else
            {
                firstTry = true;
            }
            if(sceneName == "Pre Quiz 1") {
                level1UserCorrectness[currQuestion - 2] = true;
            }
            else {
                buttonObj.GetComponent<Image>().color = Color.green;
            }
            nextButton.interactable = true;
        }
        else
        {
            firstTry = false;
            if(sceneName == "Post Quiz 1") {
                buttonObj.GetComponent<Image>().color = Color.red;
            }
            else {
                level1UserCorrectness[currQuestion - 2] = false;
                nextButton.interactable = true;
            }
        }
        Debug.Log("Correct Answer = " + level1UserCorrectness[currQuestion - 2]);
    }

    public void GoNext()
    {
        source.PlayOneShot(clip, 7f);

        ResetButtonColor();
        Invoke(nameof(SwitchQuestion), 0f);
        nextButton.interactable = false;
    }

    void SwitchQuestion()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int usedQuestions = 0;
        foreach (bool truth in level1UsedQuestions)
            if(truth)
                usedQuestions++;
        Debug.Log("Amount of questions used" + usedQuestions);
        Invoke(nameof(UpdateDisplay), .5f);
        if (usedQuestions == 10)
        {
            int correctCount = 0;

            foreach (bool b in level1UserCorrectness)
            {
                if (b)
                {
                    correctCount++;
                }
            }

            if(sceneName == "Post Quiz 1")
                question.text = "YOU HAVE ANSWERED ( " + correctCount + " ) OUT OF ( 10 ) CORRECT ON THE FIRST TRY.";
            else question.text = "YOU HAVE ANSWERED ( " + correctCount + " ) OUT OF ( 10 ) CORRECT";
            questionNumber.text = " ";
            option1.text = " ";
            option2.text = " ";

            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);

            currLevel++;
            //currQuestion++;
            nextButton.interactable = true;
            Invoke("setFinished", 1f);
            //currQuestion = 1;
        }
        if (finished)
        {
            anim.SetBool("MinigameWon", true);
            Invoke("DelayedAction", delayTime);
        }
    }

    void setFinished() {
        finished = true;
    }
    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

    void ResetButtonColor()
    {
        button1.GetComponent<Image>().color = Color.white;
        button2.GetComponent<Image>().color = Color.white;
        button3.GetComponent<Image>().color = Color.white;
        button4.GetComponent<Image>().color = Color.white;
        button5.GetComponent<Image>().color = Color.white;
    }

}