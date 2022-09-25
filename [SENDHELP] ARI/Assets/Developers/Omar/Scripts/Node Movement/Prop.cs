using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Node 
{
    //references
    public Location loc;
    Interactable inter;

    public void Start()
    {
        inter = GetComponent<Interactable>();
    }

    public override void Arrive()
    {
       
        if (inter != null && inter.enabled)
        {

            
            return;

        }
         base.Arrive();


        // make object interactable 
        if(inter != null) 
        {
            Cursor.lockState = CursorLockMode.None;
            col.enabled = true;
            inter.enabled = true;


        }
    }

    public override void Leave()
    {
        base.Leave();
        if (inter != null)
        {
            
            //inter.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void Update()
    {
        // Key contol for interaction
        if (Input.GetKeyDown(KeyCode.E) && inter.enabled) //&& inter.enabled
        {
            inter.Interact();
        }
    }
}
