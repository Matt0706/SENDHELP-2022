using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Part3Interact : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask mask;
    public Animator interact;
    public Camera cam;
    private Camera cameraPrivate;
    bool interacting = false;

    //Notepad Variables
    public AudioSource source;
    public AudioClip clip;
    bool padRead;
    public Collider enginepadCollider1;
    public Collider enginepadCollider2;
    public Collider enginepadCollider3;
    public Collider notepadCollider;

    //Terminal Variables
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;
    public Animator anim;
    public AudioSource terminalSource;
    public AudioClip terminalClip;
    public Collider terminalCollider;
    public Collider bedTerminalCollider;
    public bool dialogueOver = false;

    public Collider swipeCollider;
    public Collider airlockSwipe;
    public AudioSource swipeSource;
    public AudioClip swipeClip;
    public Animator swipeAnim;



    void Start()
    {
        swipeCollider.enabled = true;
        airlockSwipe.enabled = true;
        bedTerminalCollider.enabled = true;
        terminalCollider.enabled = true;
        notepadCollider.enabled = true;
        enginepadCollider1.enabled = true;
        enginepadCollider2.enabled = true;
        enginepadCollider3.enabled = true;
        cameraPrivate = GetComponent<Level2Part3Interact>().cam;
        var startDialogue = GetComponent<Level2Dialogue>().startDialogue;
        var startTrigger = FindObjectOfType<DialogueTrigger>();
        startTrigger.dialogue = startDialogue;
        startTrigger.TriggerDialogue();
        padRead = false;
    }


    void Update()
    {

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

                //Notepad Grabbed
                if (hitInfo.collider.CompareTag("Power Room Pad"))
                {
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().powerNotepad;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Observation Pad"))
                {
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().observationPad;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();
                    padRead = true;
                }

                if (hitInfo.collider.CompareTag("enginePad1"))
                {
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().enginePad1;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("enginePad2"))
                {
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().enginePad2;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();                   
                }

                if (hitInfo.collider.CompareTag("enginePad3"))
                {
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().enginePad3;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();                 
                }

                if (hitInfo.collider.CompareTag("Swipe Access") && dialogueOver == false)
                {
                    var swipeDialogue = GetComponent<Level2Dialogue>().swipeDialogue;
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

                if (hitInfo.collider.CompareTag("Airlock Terminal") && dialogueOver == false && padRead == false)
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().preterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                }
                else if (hitInfo.collider.CompareTag("Airlock Terminal") && dialogueOver == false && padRead == true)
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().airterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.CompareTag("Airlock Terminal") && dialogueOver == true)
                {
                    LevelSelection.levelListDone.Add(levelDone);
                    terminalSource.PlayOneShot(terminalClip, 7f);
                    anim.SetBool("MinigameWon", true);
                    Invoke("DelayedAction", delayTime);
                }

                if (hitInfo.collider.CompareTag("Power Terminal"))
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().powerterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Airlock Swipe Access"))
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().accessDeniedDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                }
            }
        }
    }


    //Displays "E" to interact
    public void UpdateUIAppear()
    {
        interact.SetBool("IsOpen", true);
        Debug.Log("New Interaction with Prop Found: ");
    }

    public void UpdateUIDisappear()
    {
        interact.SetBool("IsOpen", false);
    }

    //For Scene Change
    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
