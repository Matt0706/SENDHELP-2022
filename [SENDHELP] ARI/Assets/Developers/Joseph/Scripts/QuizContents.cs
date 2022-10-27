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

    int currQuestion = rnd.Next(20);
    int currLevel = 1;
    Button thisButton;

    public Animator anim;
    public int delayTime;

    Button nextButton;

    bool firstTry = true;
    bool finished = false;
    public GameObject caesarCipherImage;

    string sceneName;

    List<bool> userCorrectness = new List<bool>()
    {
        false, //1
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
        false  //20
    };

    List<bool> usedQuestions = new List<bool>() 
    {    
        false, //1
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
        false  //20
    };

    Text questionNumber, question, option1, option2, option3, option4, option5;
    Button button1, button2, button3, button4, button5;

    List<string> level1questionList = new List<string>()
    {
        "Which of the following is the strongest password?",                                                                                    //1
        "In general, the use of a passphrase is considered a best practice. Which of the following is the strongest example of a passphrase?",  //2
        "By following best practices, what is the best way to create and store passwords?",                                                     //3
        "It is generally a good strategy to reuse passwords across multiple sites.",                                                            //4
        "The study of hiding data from anyone but it's sender and recipients is called",                                                        //5
        "Can you decode this?",                                                                                                                 //6
        "Which of the following is not an aspect of multi-factor authentication?",                                                              //7
        "Requiring a fingerprint and a password is an example of:",                                                                             //8
        "Where should you change your password if a site that uses it has a data breach?",                                                      //9
        "It is ethical behavior to rely on transitive trust and passwords.",                                                                    //10
        "What is cryptography used for?",                                                                                                       //11
        "Cryptography is the same as encryption.",                                                                                              //12
        "You can only use a phone number for Multi-Factor Authentication",                                                                      //13
        "Multi-factor authentication can only come in the form of numbers",                                                                     //14
        "PIG LATIN is an ancient form of cryptography",                                                                                         //15
        "What is one of the first examples of cryptography?",                                                                                   //16
        "It is illegal to encrypt messages",                                                                                                    //17
        "What percentage of hacking related breaches are stolen or weak passwords?",                                                            //18
        "What should you do if you receieve a security code when you are not trying to log in?",                                                //19
        "When a new software update comes out, what should you do?"                                                                             //20
    };

    List<string> level2questionList = new List<string>() {
        "Which of the following should you do when receiving an email with a link or an attachment?",                                                                                                               //1
        "Which of the following is a common type of phishing attack?",                                                                                                                                              //2
        "Which of the following would be a good indicator that a website is a phishing site?",                                                                                                                      //3
        "You receive an email from your boss asking you to send him personal and private information of many of your top clients. He adds that this is urgent " +
        "and you must respond immediately. You should respond to him right away.",                                                                                                       //4
        "You receive an email that contains the following link: https://www.google.net/drive/folders. Is this a safe link?",                                                                                //5
        "Which of the following should you look for when browsing a website to ensure it is legitimate?",                                                                                                           //6
        "Attackers who are successful at social engineering may pretend to do the following, except",                                                                                                               //7
        "Which of the following is not a type of social engineering attack?",                                                                                                                                       //8
        "Which human instinct does social engineering most prey on?",                                                                                                                                               //9
        "Your friend receives an email stating they won $100 gift card and just need to run a program. What would be your advice?",                                                                                                                 //10
        "You receive a call from someone claiming to work in the IT department of your company, and they say they need your login credentials. What do you do?",                                                    //11
        "You receive a text message from your boss asking you to go out to a convenience store and purchase gift cards for a client. What should you do in this situation?",                                        //12
        "If you fall for a phishing scam, what should you do to limit the damage?",                                                                                                                                 //13
        "An email from your boss asks for all the information of the company’s top clients ASAP. You should give him the information right away.",              //14
        "You get a message from the IT department at your school or work, asking you to click on a link to renew your password so that you can log in to its website. You should:",                                 //15
        "You just received an email from a very wealthy person that needs your help moving money across an international border. For just a few dollars you can help him and he is " + 
        "offering to pay you many times more than your out of pocket expenses. What do you do?",                                                                                                                    //16
        "When browsing online, you get a pop-up saying \"A virus has been found on your computer. Click here to fix.\" What should you do?",                                                                        //17
        "You fall victim to a ransomware scam. The attacker has locked all of your files and threatens to destroy them unless you pay them in bitcoin. What should you do?",                                        //18
        "Which of these answers describes the best way to protect against tech support scams?",                                                                                                                     //19
        "You desperately need credentials to access an account at work so you can complete your tasks. Your boss has made it clear that you need to complete these tasks today," + 
        "but you have yet to hear back from the IT department about the account credentials. Your friend suggests that you draft an email to IT claiming to be in a higher position " +
        "than you are in order to get a response faster. Is your friend’s advice ethical, or not?"                                                                                                                  //20
    };
    List<List<string>> level1choiceList = new List<List<string>>()
    {
         new List<string> {
            "password",
            "Admin123",
            "p@ssw0rd!",
            "p@sSw0rD!"},  //1
        
        new List<string> {
            "Admin123",
            "P@ssw0rd!",
            "Qwerty123!",
            "i<3C$4L!fe"},  //2
        
        new List<string> {
            "Sticky Note",
            "Excel",
            "Password Manager",
            "In your head"},  //3

        new List<string> {
            "True",
            "False"},  //4

        new List<string> {
            "Encryption",
            "Cryptography",
            "Blockchain",
            "Firewall"},  //5

        new List<string> {
            "A RED CAT",
            "A PET CAT",
            "I CAN WIN",
            "I PET DOG"},  //6

        new List<string> {
            "Something you know",
            "Something you are",
            "Something you read",
            "Something you have"},  //7

        new List<string> {
            "Single-factor authentication",
            "Two-factor authentication",
            "Three-factor authentication",
            "None of the above"},  //8

        new List<string> {
            "None. You're probably safe",
            "On that specific site",
            "All sites using that password"},  //9

        new List<string> {
            "True",
            "False"},  //10

        new List<string> {
            "Provides a secure form of communication", //11
            "To discover clues about a crime that has been committed",
            "To make remembering your passowrd easier",
            "Hiding code in plain sight of others"},

        new List<string> {
            "True",
            "False"}, //12

        new List<string> {
            "True",
            "False"}, //13

        new List<string> {
            "True",
            "False"}, //14

        new List<string> {
            "True", //15
            "False"},
            
        new List<string> {
            "Rosetta Stone", //16
            "Federalist Papers",
            "Caesar Cipher",
            "Bitcoin"},
            
        new List<string> {
            "True",
            "False"}, //17
            
        new List<string> {
            "93",
            "81", //18
            "76",
            "69"},
            
        new List<string> {
            "Contact the IT department", //19
            "Do nothing it's probably fine",
            "Hack the person trying to log in",
            "Use the code you were given"},
            
        new List<string> {
            "Don't update",
            "Read the new things added to the update", //20
            "Read the ToS",
            "Remind me tomorrow"}
    };
    List<List<string>> level2choiceList = new List<List<string>>()
    {
         new List<string> {
            "Verify the sender", //1
            "Open it straight away",
            "Forward it to someone else",
            "None of the above"},
        
        new List<string> {
            "False website link",
            "URL Padding",
            "Email Phishing",
            "Spear Phishing",
            "All of the above"},  //2
        
        new List<string> {
            "Spelling mistakes", //3
            "Ads",
            "Both"},

        new List<string> {
            "True",
            "False"},  //4

        new List<string> {
            "Yes",
            "No"},  //5

        new List<string> {
            "Connection is secure (HTTPS)",
            "Certificate is issued by a trusted certificate authority",
            "Both"}, //6

        new List<string> {
            "Act as an expert",
            "Act in need of help",
            "Ask plainly for what they are looking for", //7
            "Act as a coworker or friend"},

        new List<string> {
            "Tailgating",
            "Phishing",
            "Hacking",  //8
            "Bailing"},

        new List<string> {
            "Trust", //9
            "Curiosity",
            "Control",
            "Fantasy"},

        new List<string> {
            "Run the program and lets go shopping!",
            "Walk away in jealousy and shrug your shoulders",
            "It sounds too good to be true so just ignore it."},

        new List<string> {
            "Give them what they are asking for",
            "Verify with IT that the request is legitimate"}, //11

        new List<string> {
            "Verify with your boss and report the incident", //12
            "Go out and purchase the gift cards"},

        new List<string> {
            "Delete the phishing email",
            "Unplug the computer. This will get rid of any viruses from the email",
            "Change any compromised passwords"}, //13

        new List<string> {
            "True",
            "False"}, //14

        new List<string> {
            "Reply to the message to confirm that you need to",
            "Confirm with the IT department the message was real", //15
            "Click on the link and change your password"},
            
        new List<string> {
            "Take him up on his offer. Easy money!!!",
            "Delete it and report it to IT if possible", //16
            "Forward the message to friends. Share the wealth!",
            "Reply saying you know its a scam"},
            
        new List<string> {
            "Click on the button to remove the virus",
            "Hover over the button to see the link",
            "Close both windows and do not return to that site", //17
            "Hit the back button and see if it goes away"},
            
        new List<string> {
            "Pay the ransom and retrieve your files",
            "Do not pay the ransom and take the loss"}, //18
            
        new List<string> {
            "Use a unique password for each of your online accounts",
            "Scan your computer for malicious software",
            "Hang up on anyone who wants to fix your computer",
            "All of the above"}, //19
            
        new List<string> {
            "Ethical",
            "Not ethical", //20
            "Neither"}
    };

    List<string> level1answerList = new List<string>()
    {
        "p@sSw0rD!",                                //1
        "i<3C$4L!fe",                               //2
        "Password Manager",                         //3
        "False",                                    //4
        "Cryptography",                             //5
        "A PET CAT",                                //6
        "Something you read",                       //7
        "Two-factor authentication",                //8
        "All sites using that password",            //9
        "False",                                    //10
        "Provides a secure form of communication",  //11
        "False",                                    //12
        "False",                                    //13
        "False",                                    //14
        "True",                                     //15
        "Rosetta Stone",                            //16
        "False",                                    //17
        "81",                                       //18
        "Contact the IT department",                //19
        "Read the new things added to the update"   //20

    };

    List<string> level2answerList = new List<string>()
    {
        "Verify the sender",                                                //1
        "All of the above",                                                 //2
        "Spelling mistakes",                                                //3
        "False",                                                            //4
        "No",                                                               //5
        "Both",                                                             //6
        "Ask plainly for what they are looking for",                        //7
        "Hacking",                                                          //8
        "Trust",                                                            //9
        "It sounds too good to be true so just ignore it.",                 //10
        "Verify with IT that the request is legitimate",                    //11
        "Verify with your boss and report the incident",                    //12
        "Change any compromised passwords",                                 //13
        "False",                                                            //14
        "Confirm with the IT department the message was real",              //15
        "Delete it and report it to IT if possible",                        //16
        "Close both windows and do not return to that site",                //17
        "Do not pay the ransom and take the loss",                          //18
        "All of the above",                                                 //19
        "Not ethical"                                                       //20

    };

     void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
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

        int numQuestionsUsed = 0;
        foreach (bool used in this.usedQuestions)
            if(used)
                numQuestionsUsed++;
        if(numQuestionsUsed < 10) {
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
            question.fontSize = 50;
            question.transform.localPosition = new Vector3(-4.2f, 288.6f, 0f);
            question.verticalOverflow = VerticalWrapMode.Truncate;
            this.usedQuestions[currQuestion] = true;
            while(this.usedQuestions[currQuestion] == true) {
                currQuestion = rnd.Next(20);
            }
            Debug.Log("CURR QUESTION: " + currQuestion);
            
            if(sceneName == "Pre Quiz 1" || sceneName == "Post Quiz 1") {
                List<string> currChoices = level1choiceList[currQuestion];
                questionNumber.text = "Question " + (numQuestionsUsed + 1);
                question.text = level1questionList[currQuestion];

                int i = 0;
                while(i < level1choiceList[currQuestion].Count){
                    optionsList[i].text = (i + 1) + ") " + currChoices[i];
                    i++;
                }
                caesarCipherImage.gameObject.SetActive(false);
                int choiceCount = level1choiceList[currQuestion].Count;
                switch(choiceCount){
                    case(2):
                        button3.gameObject.SetActive(false);
                        button4.gameObject.SetActive(false);
                        button5.gameObject.SetActive(false);
                        break;
                    case(3):
                        button4.gameObject.SetActive(false);
                        button5.gameObject.SetActive(false);
                        break;
                    case(4):
                        button5.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                if(currQuestion == 5) caesarCipherImage.gameObject.SetActive(true);
            }
            else {
                List<string> currChoices = level2choiceList[currQuestion];
                questionNumber.text = "Question " + (numQuestionsUsed + 1);
                question.text = level2questionList[currQuestion];

                int i = 0;
                while(i < level2choiceList[currQuestion].Count){
                    optionsList[i].text = (i +1) + ") " + currChoices[i];
                    i++;
                }

                caesarCipherImage.gameObject.SetActive(false);
                int choiceCount = level2choiceList[currQuestion].Count;
                switch(choiceCount){
                    case(2):
                        button3.gameObject.SetActive(false);
                        button4.gameObject.SetActive(false);
                        button5.gameObject.SetActive(false);
                        break;
                    case(3):
                        button4.gameObject.SetActive(false);
                        button5.gameObject.SetActive(false);
                        break;
                    case(4):
                        button5.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                if(currQuestion == 14 || currQuestion == 3) question.fontSize = 46;
                if(currQuestion == 15) question.fontSize = 40;
                if(currQuestion == 19) {
                    question.fontSize = 40;
                    question.transform.localPosition = new Vector3(-4.2f, 150f, 0f);
                    question.verticalOverflow = VerticalWrapMode.Overflow;
                }
            }
        }
    }

    public void CheckAnswer(Button buttonObj)
    {

        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        string userChoiceStr = buttonText.text;
        userChoiceStr = userChoiceStr.Substring(3);
        string sceneName = SceneManager.GetActiveScene().name;
        string rightAnswer;
        if(sceneName == "Pre Quiz 1" || sceneName == "Post Quiz 1") {
            rightAnswer = level1answerList[currQuestion];
        }
        else {
            rightAnswer = level2answerList[currQuestion];
        }
        thisButton = buttonObj;
        ColorBlock cb = thisButton.colors;

        if ( userChoiceStr.Equals(rightAnswer) )
        {
            if (firstTry)
            {
                userCorrectness[currQuestion] = true;
            }
            else
            {
                firstTry = true;
            }
            if(sceneName == "Pre Quiz 1" || sceneName == "Pre Quiz 2") {
                userCorrectness[currQuestion] = true;
            }
            else {
                buttonObj.GetComponent<Image>().color = Color.green;
            }
            nextButton.interactable = true;
        }
        else
        {
            firstTry = false;
            if(sceneName == "Post Quiz 1" || sceneName == "Post Quiz 2") {
                buttonObj.GetComponent<Image>().color = Color.red;
            }
            else {
                userCorrectness[currQuestion] = false;
                nextButton.interactable = true;
            }
        }
        Debug.Log("Correct Answer = " + userCorrectness[currQuestion]);
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
        int usedQuestions = 0;
        foreach (bool truth in this.usedQuestions)
            if(truth)
                usedQuestions++;
        Debug.Log("Amount of questions used" + usedQuestions);
        Invoke(nameof(UpdateDisplay), .5f);
        if (usedQuestions == 10)
        {
            int correctCount = 0;

            foreach (bool b in userCorrectness)
            {
                if (b)
                {
                    correctCount++;
                }
            }

            if(sceneName == "Post Quiz 1" || sceneName == "Post Quiz 2")
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