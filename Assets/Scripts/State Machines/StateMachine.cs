using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using SuperBrosBros.ScriptableObjects.States;

/// <summary>
/// This class will act as a generic component that should be attached to any gameobject that will 
/// utilize a state machine
/// This script will control changing states and keeping track of the states this gameobject is/was in
/// as well as all the possible states to switch to
/// </summary>
namespace SuperBrosBros.StateMachines
{
    public class StateMachine : MonoBehaviour
    {
        //What state is this game object in?
        public State currentState;
        //What state was this game object in?
        public State previousState;
        //What are all the states this game object has?

        private void OnEnable()
        {
            currentState = null;
            previousState = null;
        }

        // Called every time we switch to a state
        // Switch states, debug the sender, and trigger events, if any
        public void ChangeState(State stateToTransitionTo, GameObject messageSender, VoidEvent eventToTrigger = null)
        {
            previousState = currentState;
            currentState = stateToTransitionTo;
            stateToTransitionTo.OnStateEnter();

            if(eventToTrigger)
            {
                eventToTrigger.Raise();
            }
            Debug.Log(messageSender.name + " transitioned to " + stateToTransitionTo.name + " state.");
        }

        public void ChangeToPreviousState()
        {
            currentState = previousState;
        }

        //Update the game state in sync with MonoBehaviours Update method
        public void UpdateState()
        {
            if(currentState == null) return;
            currentState.OnUpdateState();
        }
    }
}