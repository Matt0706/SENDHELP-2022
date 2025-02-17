// [NAME] Erin Alvarico
// [Dialogue Prompt] Handles Interaction UI for Story/Diagloeu Progression in Level via Triggers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour
{

    // VARIABLES - Currently inefficient way to store animators, will find another way to reference
    public Animator[] animators;
    //public Animator prompt5;
    

    // ONTRIGGER - ENTER
    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartUpDialogue"))
        {
            num = 0;
            UIAppear(num);
        }
        if (other.CompareTag("ChipDialogue"))
        {
            num = 1;
            UIAppear(num);
        }
        if (other.CompareTag("WindowDialogue"))
        {
            num = 2;
            UIAppear(num);
        }
        if (other.CompareTag("TerminalDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 3;
            UIAppear(num);
        }
        if (other.CompareTag("StartSignDialogue"))
        {
            num = 4;
            UIAppear(num);
        }
        if (other.CompareTag("StorageSignDialogue"))
        {
            num = 5;
            UIAppear(num);
        }
        if (other.CompareTag("GreenhouseDialogue"))
        {
            num = 6;
            UIAppear(num);
        }
        if (other.CompareTag("GreenhouseTerminalDialogue"))
        {
            num = 7;
            UIAppear(num);
        }
        if (other.CompareTag("ServerRoomAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 8;
            UIAppear(num);
        }
        if (other.CompareTag("ServerRoomTerminalDialogue"))
        {
            num = 9;
            UIAppear(num);
        }
        if (other.CompareTag("ServerRoomTerminalNWDialogue"))
        {
            num = 10;
            UIAppear(num);
        }
        if (other.CompareTag("LivingQuartersTerminalDialogue"))
        {
            num = 11;
            UIAppear(num);
        }
        if (other.CompareTag("XHallAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 12;
            UIAppear(num);
        }
        if (other.CompareTag("NavigationAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 13;
            UIAppear(num);
        }
        if (other.CompareTag("LevelExitDialogue"))
        {
            num = 14;
            UIAppear(num);
        }
        if (other.CompareTag("DeskDialogue"))
        {
            num = 15;
            UIAppear(num);
        }
        if (other.CompareTag("ComputerDeskDialogue"))
        {
            num = 16;
            UIAppear(num);
        }
        if (other.CompareTag("VendingMachineDialogue"))
        {
            num = 17;
            UIAppear(num);
        }
        if (other.CompareTag("TorpedosDialogue"))
        {
            num = 18;
            UIAppear(num);
        }
        if (other.CompareTag("EasterEggDialogue"))
        {
            num = 19;
            UIAppear(num);
        }
        if (other.CompareTag("GreenhouseLearningSheet"))
        {
            num = 20;
            UIAppear(num);
        }
        if (other.CompareTag("ServerRoomLearningSheet"))
        {
            num = 21;
            UIAppear(num);
        }
        if (other.CompareTag("NavigationRoomLearningSheet"))
        {
            num = 22;
            UIAppear(num);
        }
        if (other.CompareTag("StorageRoomLearningSheet"))
        {
            num = 23;
            UIAppear(num);
        }
        if (other.CompareTag("LivingQuatersLearningSheet"))
        {
            num = 24;
            UIAppear(num);
        }
        if (other.CompareTag("PlantDialogue"))
        {
            num = 25;
            UIAppear(num);
        }
        if (other.CompareTag("VendingMachineTutorialDialogue"))
        {
            num = 26;
            UIAppear(num);
        }
        if (other.CompareTag("ArtDialogue"))
        {
            num = 27;
            UIAppear(num);
        }
        if (other.CompareTag("BlackboardDialogue"))
        {
            num = 28;
            UIAppear(num);
        }
    }*/
    

    /*// ONTRIGGER - EXIT
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StartUpDialogue"))
        {
            num = 0;
            UIDisappear(num);
        }
        if (other.CompareTag("ChipDialogue"))
        {
            num = 1;
            UIDisappear(num);
        }
        if (other.CompareTag("WindowDialogue"))
        {
            num = 2;
            UIDisappear(num);
        }
        if (other.CompareTag("TerminalDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 3;
            UIDisappear(num);
        }
        if (other.CompareTag("StartSignDialogue"))
        {
            num = 4;
            UIDisappear(num);
        }
        if (other.CompareTag("StorageSignDialogue"))
        {
            num = 5;
            UIDisappear(num);
        }
        if (other.CompareTag("GreenhouseDialogue"))
        {
            num = 6;
            UIDisappear(num);
        }
        if (other.CompareTag("GreenhouseTerminalDialogue"))
        {
            num = 7;
            UIDisappear(num);
        }
        if (other.CompareTag("ServerRoomAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 8;
            UIDisappear(num);
        }
        if (other.CompareTag("ServerRoomTerminalDialogue"))
        {
            num = 9;
            UIDisappear(num);
        }
        if (other.CompareTag("ServerRoomTerminalNWDialogue"))
        {
            num = 10;
            UIDisappear(num);
        }
        if (other.CompareTag("LivingQuartersTerminalDialogue"))
        {
            num = 11;
            UIDisappear(num);
        }
        if (other.CompareTag("XHallAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 12;
            UIDisappear(num);
        }
        if (other.CompareTag("NavigationAccessDialogue"))
        {
            // Check Player Inventory If They Have Acquired the Chip
            // If not, 4 (Pre Chip Dialogue)
            // num = 4;
            // If yes, 5 (Post Chip Dialogue)
            num = 13;
            UIDisappear(num);
        }
        if (other.CompareTag("LevelExitDialogue"))
        {
            num = 14;
            UIDisappear(num);
        }
        if (other.CompareTag("DeskDialogue"))
        {
            num = 15;
            UIDisappear(num);
        }
        if (other.CompareTag("ComputerDeskDialogue"))
        {
            num = 16;
            UIDisappear(num);
        }
        if (other.CompareTag("VendingMachineDialogue"))
        {
            num = 17;
            UIDisappear(num);
        }
        if (other.CompareTag("TorpedosDialogue"))
        {
            num = 18;
            UIDisappear(num);
        }
        if (other.CompareTag("EasterEggDialogue"))
        {
            num = 19;
            UIDisappear(num);
        }
        if (other.CompareTag("GreenhouseLearningSheet"))
        {
            num = 20;
            UIDisappear(num);
        }
        if (other.CompareTag("ServerRoomLearningSheet"))
        {
            num = 21;
            UIDisappear(num);
        }
        if (other.CompareTag("NavigationRoomLearningSheet"))
        {
            num = 22;
            UIDisappear(num);
        }
        if (other.CompareTag("StorageRoomLearningSheet"))
        {
            num = 23;
            UIDisappear(num);
        }
        if (other.CompareTag("LivingQuatersLearningSheet"))
        {
            num = 24;
            UIDisappear(num);
        }
        if (other.CompareTag("PlantDialogue"))
        {
            num = 25;
            UIDisappear(num);
        }
        if (other.CompareTag("VendingMachineTutorialDialogue"))
        {
            num = 26;
            UIDisappear(num);
        }
        if (other.CompareTag("ArtDialogue"))
        {
            num = 27;
            UIDisappear(num);
        }
        if (other.CompareTag("BlackboardDialogue"))
        {
            num = 28;
            UIDisappear(num);
        }
    }*/

    // UI APPEAR
    public void UIAppear(int num)
    {
        animators[num].SetBool("IsPrompted", true);
        Debug.Log("New Dialogue/Interaction Found: ");
        
    }

    // UI DISAPPEAR
    public void UIDisappear(int num)
    {
        animators[num].SetBool("IsPrompted", false);
        Debug.Log("Walked Out of Range of Dialogue/Interaction");       
    }

}
