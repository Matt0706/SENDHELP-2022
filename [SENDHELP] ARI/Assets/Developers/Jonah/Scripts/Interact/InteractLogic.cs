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
        cameraPrivate = GetComponent<InteractLogic>().cam;
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        //terminalCollider = GetComponent<Collider>();
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
                Debug.Log("UI is working!");
            }
            else
            {
                interacting = false;
            }

            // Key contol for interaction
            if (Input.GetKeyDown(KeyCode.E) && interacting == true)
            {
                Debug.Log("E Was Pressed!");

                //Keycard Grabbed
                if (hitInfo.collider.name == "Chip Node")
                {
                    keyGrabbed = true;

                    terminalCollider.enabled = true;

                    // Play sound on interact
                    source.PlayOneShot(clip, 7f);
                    //Destroy object
                    Destroy(GameObject.FindWithTag("Chip Node"));
                    Debug.Log("Destroy " + "Chip Node");
                }

                if (hitInfo.collider.name == "Terminal Node" && keyGrabbed == true)
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
        Debug.Log("Walked Out of Range of Prop");
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
