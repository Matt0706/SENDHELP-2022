using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Lvl1term1Interact : MonoBehaviour
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
    public GameObject terminal;
    public Animator swipeAnim;
    public Collider swipeCollider;

    //Notepad Variables
    public AudioSource noteSource;
    public AudioClip noteClip;
    bool padRead;
    public Collider notepadCollider;

    //Dialogue
    public Animator[] dialogueAnimation;


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
        cameraPrivate = GetComponent<Lvl1term1Interact>().cam;
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        padRead = false;
    }


    void Update()
    {
        swipeCollider.enabled = true;
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

                //SwipeAccess 
                if (hitInfo.collider.CompareTag("Swipe Access"))
                {
                    swipeSource.PlayOneShot(swipeClip, 7f);
                    Debug.Log("Sound Played");
                    swipeAnim.SetBool("hasAccessKey", true);
                }


                //Server Notepad
                if (hitInfo.collider.CompareTag("ServerRoomLearningSheet"))
                {
                    padRead = true;
                    terminalCollider.enabled = true;
                }


                //Terminal
                if (hitInfo.collider.CompareTag("SaveNode") && padRead == true)
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
