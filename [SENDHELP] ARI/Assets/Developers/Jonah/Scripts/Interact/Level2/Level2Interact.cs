using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Interact : MonoBehaviour
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
    public Collider bedTerminalCollider;
    public bool dialogueOver = false;



    void Start()
    {
        bedTerminalCollider.enabled = true;
        terminalCollider.enabled = true;
        notepadCollider.enabled = true;
        cameraPrivate = GetComponent<Level2Interact>().cam;
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
                    padRead = true;
                    terminalCollider.enabled = true;
                    var powerpadDialogue = GetComponent<Level2Dialogue>().powerNotepad;
                    var powerpadTrigger = FindObjectOfType<DialogueTrigger>();
                    powerpadTrigger.dialogue = powerpadDialogue;
                    powerpadTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Swipe Access"))
                {
                    var swipeDialogue = GetComponent<Level2Dialogue>().swipeDialogue;
                    var swipeTrigger = FindObjectOfType<DialogueTrigger>();
                    swipeTrigger.dialogue = swipeDialogue;
                    swipeTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Bedroom Terminal") && padRead == true && dialogueOver == false)
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().bedterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
                }                
                else if (hitInfo.collider.CompareTag("Bedroom Terminal") && padRead == true && dialogueOver == true)
                {
                    LevelSelection.levelListDone.Add(levelDone);
                    terminalSource.PlayOneShot(terminalClip, 7f);
                    anim.SetBool("MinigameWon", true);
                    Invoke("DelayedAction", delayTime);
                }
                else if (hitInfo.collider.CompareTag("Bedroom Terminal") && padRead == false)
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().preterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("Power Terminal"))
                {
                    var terminalDialogue = GetComponent<Level2Dialogue>().powerterminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
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
