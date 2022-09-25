using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChipNode : UpdatedInteractable
{

    public AudioSource source;
    public AudioClip clip;
    public string objectTag;

    public GameObject terminal;
    //bool keyGrabbed;
    bool key;
    // Start is called before the first frame update
    void Start()
    {
        //this.enabled = false;
        terminal.GetComponent<SceneChangeInteract>().enabled = false;
        key = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            terminal.GetComponent<SceneChangeInteract>().enabled = true;
        }
    }

    public override void NewInteract()
    {

        key = true;
        terminal.GetComponent<SceneChangeInteract>().enabled = true;
        // Play sound on interact
        source.PlayOneShot(clip, 7f);
        // Place tag of object
        Destroy(GameObject.FindWithTag(objectTag));
        //Destroy(this.gameObject);
        Debug.Log("Destroy " + objectTag);
    }
}
