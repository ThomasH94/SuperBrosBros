using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will be a component to be added to any gameobject that uses an animation controller
/// Another script will signal this one(via events usually) to tell this component what animation to play
/// This script seems like overkill, but it prevents a gameobject from having to work too hard, and instead,
/// lets this component do all the animation work for them
/// </summary>
namespace SuperBrosBros.AnimationSystem
{
    public class BaseAnimationController : MonoBehaviour
    {
        //The animator for the object you want to animate
        public Animator animationController;

        //Play an animation by sending a string
        public void PlayAnimationWithString(string animationToPlay)
        {
            animationController.Play(animationToPlay);
        }

        //Play an animation by sending the string but hash for speed
        public void PlayAnimationWithStringHash(string animationToPlay)
        {
            int stringToHash = Animator.StringToHash(animationToPlay);
            animationController.Play(stringToHash);
        }

        // Play an animation based on a trigger
        public void PlayAnimationWithTrigger(string triggerToSet)
        {
            animationController.SetTrigger(triggerToSet);
            Debug.Log("Triggered animation with " + triggerToSet);
        }

        public void PlayerAnimationWithBool(string boolToSet, bool boolValue)
        {
            animationController.SetBool(boolToSet, boolValue);
        }

        // Set a value of a float parameter in the animator controller
        public void SetAnimationFloat(string floatToSet, float floatValue)
        {
            animationController.SetFloat(floatToSet, floatValue);
        }
    }
}