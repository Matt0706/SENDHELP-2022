using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1End : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask mask;
    public Animator interact;
    public Camera cam;
    private Camera cameraPrivate;
    bool interacting = false;

    //Exit SwipeAccess Variables
    public AudioSource exitswipeSource;
    public AudioClip exitswipeClip;
    public Animator exitswipeAnim;
    public Collider exitswipeCollider;
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;

    //Notepad for storage room variables
    public AudioSource note1Source;
    public AudioClip note1Clip;
    bool padRead1;
    public Collider notepad1Collider;


    public Animator anim;
    public Collider cabinetCollider;


    //Dialogue
    public Animator[] dialogueAnimation;

    bool dialogueOver = false;

    public AudioSource source;
    public AudioClip clip;
    bool keyGrabbed = false;


    void Start()
    {
        var startDialogue = GetComponent<Lvl1Term3Dialogue>().startDialogue;
        var startTrigger = FindObjectOfType<DialogueTrigger>();
        startTrigger.dialogue = startDialogue;
        startTrigger.TriggerDialogue();
        cameraPrivate = GetComponent<Level1End>().cam;
        padRead1 = false;
    }


    void Update()
    {
        if (keyGrabbed == false)
        {
            cabinetCollider.enabled = true;
        }
        notepad1Collider.enabled = true;
        exitswipeCollider.enabled = true;
        UpdateUIDisappear();

        //Creates an array at the center of the player camera
        Ray ray = new Ray(cameraPrivate.transform.position, cameraPrivate.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        //Collision information
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<UpdatedInteractable>() != null)
            {
                interacting = true;
                UpdateUIAppear();
            }
            else
            {
                interacting = false;
            }

            // Contol for interaction
            if (Input.GetKeyDown(KeyCode.E) && interacting == true)
            {
                Debug.LogWarning("E Was Pressed!");

                //Server Notepad
                if (hitInfo.collider.CompareTag("Nav Room Pad"))
                {
                    var notepadDialogue = GetComponent<Lvl1Term3Dialogue>().navNotepad;
                    var notepadTrigger = FindObjectOfType<DialogueTrigger>();
                    notepadTrigger.dialogue = notepadDialogue;
                    notepadTrigger.TriggerDialogue();
                    padRead1 = true;
                    exitswipeCollider.enabled = true;
                }


                if (hitInfo.collider.CompareTag("Swipe Access") && keyGrabbed == false && dialogueOver == false)
                {
                    var swipeDialogue = GetComponent<Lvl1Term3Dialogue>().accessDeniedDialogue;
                    var swipeTrigger = FindObjectOfType<DialogueTrigger>();
                    swipeTrigger.dialogue = swipeDialogue;
                    swipeTrigger.TriggerDialogue();
                }
                else if (hitInfo.collider.CompareTag("Swipe Access") && keyGrabbed == true && dialogueOver == false)
                {
                    var swipeDialogue = GetComponent<Lvl1Term3Dialogue>().swipeDialogue;
                    var swipeTrigger = FindObjectOfType<DialogueTrigger>();
                    swipeTrigger.dialogue = swipeDialogue;
                    swipeTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.CompareTag("Swipe Access") && keyGrabbed == true && dialogueOver == true)
                {
                    LevelSelection.levelListDone.Add(levelDone);
                    exitswipeSource.PlayOneShot(exitswipeClip, 7f);
                    exitswipeAnim.SetBool("MinigameWon", true);
                    Invoke("DelayedAction", delayTime);
                    dialogueOver = false;
                }

                if (hitInfo.collider.CompareTag("ComputerDeskDialogue"))
                {
                    var deskDialogue = GetComponent<Lvl1Term3Dialogue>().deskDialogue;
                    var deskTrigger = FindObjectOfType<DialogueTrigger>();
                    deskTrigger.dialogue = deskDialogue;
                    deskTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("VendingMachineDialogue"))
                {
                    var vendingDialogue = GetComponent<Lvl1Term3Dialogue>().vendingDialogue;
                    var vendingTrigger = FindObjectOfType<DialogueTrigger>();
                    vendingTrigger.dialogue = vendingDialogue;
                    vendingTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Phone") && dialogueOver == false)
                {

                    var phoneDialogue = GetComponent<Lvl1Term3Dialogue>().cabinetDialogue;
                    var phoneTrigger = FindObjectOfType<DialogueTrigger>();
                    phoneTrigger.dialogue = phoneDialogue;
                    phoneTrigger.TriggerDialogue();
                    dialogueOver = true;

                }
                else if (hitInfo.collider.CompareTag("Phone") && dialogueOver == true)
                {
                    keyGrabbed = true;

                    //Destroy object
                    Destroy(GameObject.FindWithTag("Phone"));
                    Debug.Log("Destroy " + "Phone");
                    dialogueOver = false;
                }
            }
        }
    }

    public void UpdateUIAppear()
    {
        interact.SetBool("IsOpen", true);
        Debug.Log("New Interaction with Prop Found: ");
    }

    public void UpdateUIDisappear()
    {
        interact.SetBool("IsOpen", false);
    }

    public void UIAppear(int num)
    {
        dialogueAnimation[num].SetBool("IsPrompted", true);
        Debug.Log("New Dialogue/Interaction Found: ");

    }

    // UI DISAPPEAR
    public void UIDisappear(int num)
    {
        dialogueAnimation[num].SetBool("IsPrompted", false);
        Debug.Log("Walked Out of Range of Dialogue/Interaction");
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
