using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseTerminal : UpdatedInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void NewInteract()
    {
        Debug.Log("Terminal is working!");
    }
}
