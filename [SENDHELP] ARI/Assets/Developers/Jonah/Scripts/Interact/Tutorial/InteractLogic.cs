using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractLogic : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask mask;
    public Animator interact;
    public Camera cam;
    private Camera cameraPrivate;

    //Keycard Variables
    public AudioSource source;
    public AudioClip clip;
    public string objectTag;
    public GameObject terminal;
    bool keyGrabbed;
    bool interacting = false;
    bool dialogueOver = false;

    //Terminal Variables
    public string SceneToLoad;
    public int delayTime;
    public int levelDone;
    public Animator anim;
    public AudioSource terminalSource;
    public AudioClip terminalClip;
    public Collider terminalCollider;

    private Dialogue startDialogue;



    void Start()
    {
        cameraPrivate = GetComponent<InteractLogic>().cam;
        
        startDialogue = GetComponent<TutorialDialogue>().startDialogue;
        DialogueTrigger startTrigger = FindObjectOfType<DialogueTrigger>();
        startTrigger.dialogue = startDialogue;
        startTrigger.TriggerDialogue();
        Debug.LogWarning(startDialogue.names);
        

        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        keyGrabbed = false;
        

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

            // Key contol for interaction
            if (Input.GetKeyDown(KeyCode.E) && interacting == true)
            {

                if (hitInfo.collider.CompareTag("Vending Machine"))
                {
                    var vendingDialogue = GetComponent<TutorialDialogue>().vendingDialogue;
                    var vendingTrigger = FindObjectOfType<DialogueTrigger>();
                    vendingTrigger.dialogue = vendingDialogue;
                    vendingTrigger.TriggerDialogue();
                }

                //Chip Grabbed
                if (hitInfo.collider.name == "Chip Node" && dialogueOver == false)
                {

                    var chipDialogue = GetComponent<TutorialDialogue>().chipDialogue;
                    var chipTrigger = FindObjectOfType<DialogueTrigger>();
                    chipTrigger.dialogue = chipDialogue;
                    chipTrigger.TriggerDialogue();
                    dialogueOver = true;
                    
                }
                else if (hitInfo.collider.name == "Chip Node" && dialogueOver == true)
                {
                    keyGrabbed = true;

                    terminalCollider.enabled = true;

                    // Play sound on interact
                    source.PlayOneShot(clip, 7f);
                    
                    //Destroy object
                    Destroy(GameObject.FindWithTag("Chip Node"));
                    Debug.Log("Destroy " + "Chip Node");

                    dialogueOver = false;
                }

                if (hitInfo.collider.CompareTag("WindowDialogue"))
                {
                    Debug.LogWarning("Triggered");
                    var windowDialogue = GetComponent<TutorialDialogue>().windowDialogue;
                    var windowTrigger = FindObjectOfType<DialogueTrigger>();
                    windowTrigger.dialogue = windowDialogue;
                    windowTrigger.TriggerDialogue();
                    
                }

                if (hitInfo.collider.CompareTag("PlantDialogue"))
                {
                    var plantDialogue = GetComponent<TutorialDialogue>().plantDialogue;
                    var plantTrigger = FindObjectOfType<DialogueTrigger>();
                    plantTrigger.dialogue = plantDialogue;
                    plantTrigger.TriggerDialogue();
                }               

                if (hitInfo.collider.CompareTag("ArtDialogue"))
                {
                    var artDialogue = GetComponent<TutorialDialogue>().artDialogue;
                    var artTrigger = FindObjectOfType<DialogueTrigger>();
                    artTrigger.dialogue = artDialogue;
                    artTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.CompareTag("BlackboardDialogue"))
                {
                    var boardDialogue = GetComponent<TutorialDialogue>().boardDialogue;
                    var boardTrigger = FindObjectOfType<DialogueTrigger>();
                    boardTrigger.dialogue = boardDialogue;
                    boardTrigger.TriggerDialogue();
                }

                if (hitInfo.collider.name == "Terminal Node" && keyGrabbed == true && dialogueOver == false)
                {
                    var terminalDialogue = GetComponent<TutorialDialogue>().terminalDialogue;
                    var terminalTrigger = FindObjectOfType<DialogueTrigger>();
                    terminalTrigger.dialogue = terminalDialogue;
                    terminalTrigger.TriggerDialogue();
                    dialogueOver = true;
                }
                else if (hitInfo.collider.name == "Terminal Node" && keyGrabbed == true && dialogueOver == true)
                {
                    LevelSelection.levelListDone.Add(levelDone);
                    source.PlayOneShot(clip, 7f);
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

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
