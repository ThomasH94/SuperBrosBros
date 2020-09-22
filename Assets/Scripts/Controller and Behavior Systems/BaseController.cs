using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperBrosBros.ScriptableObjects.States;
using SuperBrosBros.StateMachines;

/// <summary>
/// This class will be the base for any object/actor/prop/projectile that will need to move
/// All functionality will be decided based on the inheriting objects
/// </summary>
namespace SuperBrosBros.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        public Rigidbody2D myRigidbody;

        // Lets change this to a custom serializable dictionary?
        public List<State> allStates = new List<State>();
        public StateMachine myStateMachine;
    }
}