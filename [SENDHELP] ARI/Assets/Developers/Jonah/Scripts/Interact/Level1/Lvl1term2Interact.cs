using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Lvl1term2Interact : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask mask;
    public Animator interact;
    public Camera cam;
    private Camera cameraPrivate;
    bool interacting = false;

    //SwipeAccess Variables
    public AudioSource swipeSource;
    public AudioClip swipeClip;
    //public GameObject terminal;
    public Animator swipeAnim;
    public Collider swipeCollider;

    //Notepad for storage room variables
    public AudioSource note1Source;
    public AudioClip note1Clip;
    bool padRead1;
    public Collider notepad1Collider;

    //Notepad for  room variables
    public AudioSource note2Source;
    public AudioClip note2Clip;
    public Collider notepad2Collider;
    bool padRead2;

    //Dialogue
    public Animator[] dialogueAnimation;
    public Collider navCollider;
    public bool dialogueOver = false;

    //Terminal Variables
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;
    public Animator anim;
    public AudioSource terminalSource;
    public AudioClip terminalClip;
    public Collider terminalCollider;





    void Start()
    {
        cameraPrivate = GetComponent<Lvl1term2Interact>().cam;
        padRead1 = false;
        padRead2 = false;
    }


    void Update()
    {
        swipeCollider.enabled = true;
        notepad1Collider.enabled = true;
        notepad2Collider.enabled = true;
        navCollider.enabled = true;
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

                //SwipeAccess 
                if (hitInfo.collider.CompareTag("Swipe Access") && dialogueOver == false)
                {
                    var swipeDialogue = GetComponent<Lvl1Term2Dialogue>().swipeDialogue;
                    var swipeTrigger = FindObjectOfType<DialogueTrigger>();
                    swipeTrigger.dialogue = swipeDialogue;
                    swipeTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.CompareTag("Swipe Access") && dialogueOver == true)
                {
                    swipeSource.PlayOneShot(swipeClip, 7f);
                    Debug.Log("Sound Played");
                    swipeAnim.SetBool("hasAccessKey", true);
                    dialogueOver = false;
                }


                //Server Notepad
                if (hitInfo.collider.CompareTag("Storage Room Pad"))
                {
                    padRead1 = true;
                    terminalCollider.enabled = true;
                    var storagepadDialogue = GetComponent<Lvl1Term2Dialogue>().storagepadDialogue;
                    var storagepadTrigger = FindObjectOfType<DialogueTrigger>();
                    storagepadTrigger.dialogue = storagepadDialogue;
                    storagepadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Living Quarters Pad"))
                {
                    padRead2 = true;
                    terminalCollider.enabled = true;
                    var livingpadDialogue = GetComponent<Lvl1Term2Dialogue>().livingpadDialogue;
                    var livingpadTrigger = FindObjectOfType<DialogueTrigger>();
                    livingpadTrigger.dialogue = livingpadDialogue;
                    livingpadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("StorageSignDialogue"))
                {
                    var signDialogue = GetComponent<Lvl1Term2Dialogue>().storageDialogue;
                    var storageTrigger = FindObjectOfType<DialogueTrigger>();
                    storageTrigger.dialogue = signDialogue;
                    storageTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("TorpedosDialogue"))
                {
                    var torpedosDialogue = GetComponent<Lvl1Term2Dialogue>().torpedosDialogue;
                    var torpedosTrigger = FindObjectOfType<DialogueTrigger>();
                    torpedosTrigger.dialogue = torpedosDialogue;
                    torpedosTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("DeskDialogue"))
                {
                    var deskDialogue = GetComponent<Lvl1Term2Dialogue>().deskDialogue;
                    var deskTrigger = FindObjectOfType<DialogueTrigger>();
                    deskTrigger.dialogue = deskDialogue;
                    deskTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Access Node"))
                {
                    var navDialogue = GetComponent<Lvl1Term2Dialogue>().navSwipeDialogue;
                    var navTrigger = FindObjectOfType<DialogueTrigger>();
                    navTrigger.dialogue = navDialogue;
                    navTrigger.TriggerDialogue();
                }

                //Terminal
                if (hitInfo.collider.CompareTag("SaveNode") && padRead1 == true && padRead2 == true && dialogueOver == false)
                {
                    var terminalDialogue = GetComponent<Lvl1Term2Dialogue>().terminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.CompareTag("SaveNode") && padRead1 == true && padRead2 == true && dialogueOver == true)
                {
                    LevelSelection.levelListDone.Add(levelDone);
                    terminalSource.PlayOneShot(terminalClip, 7f);
                    anim.SetBool("MinigameWon", true);
                    Invoke("DelayedAction", delayTime);
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
