using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level1Interact : MonoBehaviour
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
    public Collider notepadCollider;

    //Terminal Variables
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;
    public Animator anim;
    public AudioSource terminalSource;
    public AudioClip terminalClip;
    public Collider terminalCollider;
    public bool dialogueOver = false;

    public Collider startsignCollider;
    public Collider swipeCollider;



    void Start()
    {
        cameraPrivate = GetComponent<Level1Interact>().cam;
        var startDialogue = GetComponent<Level1Dialogue>().startDialogue;
        var startTrigger = FindObjectOfType<DialogueTrigger>();
        startTrigger.dialogue = startDialogue;
        startTrigger.TriggerDialogue();
        padRead = false;
    }


    void Update()
    {
        notepadCollider.enabled = true;
        startsignCollider.enabled = true;
        swipeCollider.enabled = true;
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
                if (hitInfo.collider.CompareTag("Greenhouse Pad"))
                {
                    padRead = true;
                    terminalCollider.enabled = true;
                    var greenhousepadDialogue = GetComponent<Level1Dialogue>().greenhouseNotepad;
                    var greenhousepadTrigger = FindObjectOfType<DialogueTrigger>();
                    greenhousepadTrigger.dialogue = greenhousepadDialogue;
                    greenhousepadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("GreenhouseDialogue"))
                {
                    var greenhouseDialogue = GetComponent<Level1Dialogue>().greenhouseDialogue;
                    var greenhouseTrigger = FindObjectOfType<DialogueTrigger>();
                    greenhouseTrigger.dialogue = greenhouseDialogue;
                    greenhouseTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Start Sign"))
                {
                    var signDialogue = GetComponent<Level1Dialogue>().signDialogue;
                    var signTrigger = FindObjectOfType<DialogueTrigger>();
                    signTrigger.dialogue = signDialogue;
                    signTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Swipe Access"))
                {
                    var swipeDialogue = GetComponent<Level1Dialogue>().swipeDialogue;
                    var swipeTrigger = FindObjectOfType<DialogueTrigger>();
                    swipeTrigger.dialogue = swipeDialogue;
                    swipeTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Terminal Node") && padRead == true && dialogueOver == false)
                {
                    
                    var terminalDialogue = GetComponent<Level1Dialogue>().terminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.CompareTag("Terminal Node") && padRead == true && dialogueOver == true)
                {

                    LevelSelection.levelListDone.Add(levelDone);
                    terminalSource.PlayOneShot(terminalClip, 7f);
                    anim.SetBool("MinigameWon", true);
                    Invoke("DelayedAction", delayTime);
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
