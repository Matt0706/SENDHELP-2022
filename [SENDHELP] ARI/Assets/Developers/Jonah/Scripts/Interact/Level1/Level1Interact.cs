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
    public GameObject terminal;
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
        cameraPrivate = GetComponent<Level1Interact>().cam;
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
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
                    //var dialogue = FindObjectOfType<DialoguePrompt>();
                    UIAppear(0);
                }

                if (hitInfo.collider.CompareTag("Terminal Node") && padRead == true)
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
