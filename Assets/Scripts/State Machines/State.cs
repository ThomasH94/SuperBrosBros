using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

namespace SuperBrosBros.ScriptableObjects.States
{
    [CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/States")]
    public class State : ScriptableObject
    {
        //Check against this list as a faster way of checking if we have a state conflict
        public List<State> conflictingStates = new List<State>();

        //The events to be called when switching between states - usually followed by animation and sounds
        #region UnityAtomEvents
        public VoidEvent OnEnableEvent;
        public VoidEvent OnStateEnterEvent;
        public VoidEvent OnStateExitEvent;
        #endregion

        public void OnEnable()
        {
            if(OnEnableEvent != null)
            {
                OnEnableEvent.Raise();
            }
        }

        //Entering a state, should do presentations(I.E. animations, play sounds, etc) when entering a state
        //Each State method will trigger an event
        public virtual void OnStateEnter()
        {
            if(OnStateEnterEvent != null)
            {
                OnStateEnterEvent.Raise();
            }
        }

        public virtual void OnUpdateState()
        {
            //Do something..
        }

        public virtual void OnExitState()
        {
            if(OnStateExitEvent != null)
            {
                OnStateExitEvent.Raise();
            }
        }
    }
}