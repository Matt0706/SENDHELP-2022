using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpdatedInteractable : MonoBehaviour
{
    public void BaseInteract()
    {
        this.enabled = true;
        NewInteract();
    }
    public virtual void NewInteract()
    {
        //Will be overridden by subclasses
    }
   
}
