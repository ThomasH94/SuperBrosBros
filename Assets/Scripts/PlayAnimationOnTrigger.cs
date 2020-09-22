using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using SuperBrosBros.AnimationSystem;
using SuperBrosBros.Controllers;

/// <summary>
/// This script is currently for debugging but may be refactored 
/// </summary>
public class PlayAnimationOnTrigger : MonoBehaviour
{
    public BaseAnimationController animationController;
    public BoolEvent TriggeredEvent;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponentInParent<MarioController>())
        {
            animationController.PlayAnimationWithTrigger("Triggered");
        }
    }
}
