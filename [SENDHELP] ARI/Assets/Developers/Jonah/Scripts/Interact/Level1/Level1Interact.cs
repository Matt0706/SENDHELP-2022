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

    



    void Start()
    {
        cameraPrivate = GetComponent<Level1Interact>().cam;
        padRead = false;
    }


    void Update()
    {
        notepadCollider.enabled = true;
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
                    var dialogueTrigger = FindObjectOfType<DialogueTrigger>();
                    dialogueTrigger.TriggerDialogue();                 
                }

                if (hitInfo.collider.CompareTag("Terminal Node") && padRead == true)
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
