using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
public class PhishingGameData : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    public Animator anim;
    public int delayTime;
    int currMessage = 0;
    bool onStartScreen = true;
    List<string> emailAddresses = new List<string>() { "noreply@aamazon.com", "microsoftbilling@c2asdhj398.com", "ITsupport@xyz.net", "trustedbank@trustedbank.com", "HR@fakecompany.com", "spacesoftware@spacesoftware.com", "Bisa<EZheng@Pacific.com>", "Fitzgerald Docking & Co.\n<Fits@FitzDockCo.com>", "CaptainAI@cyber.net"};
    List<string> messages = new List<string>()  /*0*/ {"Hello ARI,\nYou have been chosen as a recipient of a 100 \ndollars gift card! Click the link below to claim it:\namazom.com/giftcard",
                                                /*1*/"Hello, it is required for you to update your \ncredit card information. Please use the link \nbelow to enter your credit card number.\nbilling.micros0ft.com", 
                                                /*2*/"Hello ARI, there seems to be an issue with your \naccount login credentials. Please reply to this \nmessage with your username and password to this \nemail to rectify this issue.\nThank you, IT", 
                                                /*3*/"Hello valued customer,\nWe regret to inform you that your online banking access has been restricted. Please use the link \nbelow to sign in and fix this issue.\ntrustedbanklogin.scamsite.net", 
                                                /*4*/"Dear employee,\nYou are required to immediately review and \nelectronically sign our updated employee code of conduct. CLICK HERE to open the document.\nThank you, HR, FakeCompany",
                                                /*5*/"Dear user, we are ending support for all outdatedversions of the current software you are on as of 2/8/2072 12:00:00 A.M.\nClick the link below immediately to install the\nlatest version of the software",
                                                /*6*/"Review your information\nDue to recent activity, we have temporarily \nsuspended your account until verification. Pls\nsend us your information by 11/2/2072 4:55:28 PM To continue using our service, we advise you to\nupdate your account information.\nbanking.info.com",
                                                /*7*/"Your docking itinerary\nDocking Duration: 6 days | Arrival: March 2, 2072Name: Ari | Confirmation Number: DJ93N5\nDeparture: March 7, 2072 | Location: CKE Port 309",
                                                /*8*/"Ari, remember to complete the tasks in the note Igave you in the power room.\nThanks"};

    
    /*
    static List<string> choices = new List<string>() {
        " 1. sender address",      
        " 2. suspicious link",     
        " 3. poor writing",        
        " 4. generic greeting",    
        " 5. personal information",
        " 6. urgency",              
        " 7. nothing"               
    };
    Answer Key
    0:  123
    1:  125
    2:  156
    3:  12
    4:  1245
    5:  24
    6:  235
    7:  nothing  
    8:  nothing
    */

    List<bool> usedMessages = new List<bool> {false, false, false, false, false, false, false, false, false, false};
    int numCorrect = 0;
    static Random rnd = new Random();
    int usedCount = 0;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PasswordTerminalTerminal.ClearScreen();
        PasswordTerminalTerminal.WriteLine("ARI, it seems we have scammers trying to \ninfiltrate our systems.");
        PasswordTerminalTerminal.WriteLine("\nCheck your inbox and identify each thing wrong \nwith the emails.");
        PasswordTerminalTerminal.WriteLine("\nPossible choices include \"sender address\", \n\"suspicious link\", \"spelling errors\", \nand \"nothing\"");
        PasswordTerminalTerminal.WriteLine("\nType open to get started");
        currMessage = rnd.Next(messages.Count);
        while(this.usedMessages[currMessage] == true) {
            currMessage = rnd.Next(messages.Count);
        }
    }   

    void UpdateScreen() {
        this.usedMessages[currMessage] = true;
        PasswordTerminalTerminal.ClearScreen();
        if(usedCount < messages.Count) {
            Debug.Log(currMessage);
            PasswordTerminalTerminal.WriteLine("You have " + (messages.Count - usedCount) + " new messages");
            PasswordTerminalTerminal.WriteLine("Phishing signs: \n1. sender address  | 4. generic greeting\n2. suspicious link | 5. personal information\n3. poor writing    | 6. urgency");
            PasswordTerminalTerminal.WriteLine("\nFrom: " + emailAddresses[currMessage]);
            PasswordTerminalTerminal.WriteLine(messages[currMessage]);
            if(currMessage == 6) 
                PasswordTerminalTerminal.WriteLine("Type the number of each sign, then hit enter.");
            else
                PasswordTerminalTerminal.WriteLine("\nType the number of each sign, then hit enter.");
            PasswordTerminalTerminal.WriteLine("Otherwise type \"nothing\"");
        }
        else endScreen();
    }

    void endScreen(){
        PasswordTerminalTerminal.WriteLine("You got " + numCorrect + " out of " + messages.Count + " completely correct.");
        double percentCorrect = numCorrect / (double)messages.Count;
        if(percentCorrect > .5) {
            PasswordTerminalTerminal.WriteLine("Great work ARI!");
            SceneToLoad = "Level2PhishingSuccess";
        }
        else{
            PasswordTerminalTerminal.WriteLine("Warning! Detecting intruders in the network.");
        }
        Invoke("end", 5f);
    }

    void end() {
        // WIN CONDITION!
        anim.SetBool("MinigameWon", true);
        Invoke("DelayedAction", delayTime);
    }


    void OnUserInput(string Input)
    {
        if(onStartScreen) {
            if(Input.ToLower() == "open") {
                onStartScreen = false;
                UpdateScreen();
            }
            else Start();
        }
        else {
            Input = Input.ToLower();
            if(usedCount < messages.Count) {
                switch(currMessage) {
                    case(0):
                        if(Input.Contains("1") && Input.Contains("2") && Input.Contains("3"))
                            if(!Input.Contains("4") && !Input.Contains("5") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(1):
                        if(Input.Contains("1") && Input.Contains("2") && Input.Contains("5"))
                            if(!Input.Contains("3") && !Input.Contains("4") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(2):
                        if(Input.Contains("1") && Input.Contains("5") && Input.Contains("6"))
                            if(!Input.Contains("2") && !Input.Contains("3") && !Input.Contains("4"))
                                numCorrect++;
                        break;
                    case(3):
                        if(Input.Contains("1") && Input.Contains("2"))
                            if(!Input.Contains("3") && !Input.Contains("4") && !Input.Contains("5") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(4):
                        if(Input.Contains("1") && Input.Contains("2") && Input.Contains("4") && Input.Contains("5"))
                            if(!Input.Contains("3") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(5):
                        if(Input.Contains("2") && Input.Contains("4"))
                            if(!Input.Contains("1") && !Input.Contains("3") && !Input.Contains("5") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(6):
                        if(Input.Contains("2") && Input.Contains("3") && Input.Contains("5"))
                            if(!Input.Contains("1") && !Input.Contains("4") && !Input.Contains("6"))
                                numCorrect++;
                        break;
                    case(7):
                        if(Input.ToLower().Contains("nothing") || Input == "")
                            numCorrect++;
                        break;
                    case(8):
                        if(Input.Contains("nothing") || Input == "")
                            numCorrect++;
                        break;
                    default:
                        break;
                }
            }
            Debug.Log("Num Correct: " + numCorrect);
            usedMessages[currMessage] = true;
            if(this.usedCount < (messages.Count - 1))
                while(this.usedMessages[currMessage]) {
                    currMessage = rnd.Next(messages.Count);
                }
            usedCount++;
            UpdateScreen();
        }
    }
    

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

}
