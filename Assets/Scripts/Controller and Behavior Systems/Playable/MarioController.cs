using System.Collections;
using UnityEngine;
using UnityAtoms;
using SuperBrosBros.AnimationSystem;
using SuperBrosBros.ScriptableObjects.CharacterStats;

/// <summary>
/// This controller will be used to control Mario and all of his forms(Tiny,Big,Fire) etc.
/// </summary>
namespace SuperBrosBros.Controllers
{
    public class MarioController : BaseController, IPlayable
    {
        #region CharacterStats
        public BaseCharacterStats marioStats;
        #endregion
        #region IPlayable Required Variables
        public float MoveSpeed
        {
            get
            {
                return MoveSpeed;
            }

            set
            {
                if(value < 0)
                {
                    value = 0;
                }

                MoveSpeed = value;
            }
        }
        public float JumpForce
        {
            get
            {
                return JumpForce;
            }

            set
            {
                if(value < 0)
                {
                    value = 0;
                }

                JumpForce = value;
            }
        }
        #endregion

        #region UnityAtomEvents
        public StringVoidEventDictionary eventDictionary;
        #endregion

        #region Physics
        private bool runningRight = false;
        public float moveForce = 2f;
        private bool canJump = true;
        #endregion

        public BaseAnimationController marioAnimationController;

        private void Start()
        {
            // Might want to set this to idle if we expect Idle on start..
            myStateMachine.currentState = null;
        }

        // Runs every frame
        private void Update()
        {
            CheckForInputs();
        }

        // Physics Based Update
        private void FixedUpdate()
        {
            Run();
            //Jump();
        }

        // Temp method to check for inputs instead of using the Unity 2019 input system
        void CheckForInputs()
        {
            moveForce = 0f;
            // TEMP UNTIL WE UNDERSTAND UNITY 2019 INPUT
            if(Input.GetKeyDown(KeyCode.Space) && myStateMachine.currentState != allStates[2] && canJump)
            {
                Jump();
            }

            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                // IsMovingRight
                runningRight = true;
                moveForce = 2f;

            }
            else if(Input.GetAxisRaw("Horizontal") < 0)
            {
                // IsMovingLeft
                runningRight = false;
                moveForce = -2f;
            }
            else
            {
                // Stop Moving
            }

        }

        public void Run()
        {
            Vector2 newPosition = new Vector2(moveForce, myRigidbody.velocity.y);

            myRigidbody.MovePosition(myRigidbody.position + (newPosition * Time.deltaTime * marioStats.moveSpeed));  // Moved based left or right with the appropriate force via the runningRight bool
            // Old way of doing things...uses the blend tree and blends when the parameter(In this case RunSpeed)
            // is higher then the blend value (which is .5 in this case)
            // Uncomment if you want to use this for testing animations
            //marioAnimationController.SetAnimationFloat("RunSpeed", Input.GetAxisRaw("Horizontal"));
        }

        public void Jump()
        {
            // Check our dictionary, then play the event if we have it
            // Otherwise, just change states -- Uncomment to trigger events
            /*
            if(eventDictionary.ContainsKey("Mario_Jump_Event"))
            {
                myStateMachine.ChangeState(allStates[2], this.gameObject, eventDictionary["Mario_Jump_Event"]);
            }
            else
            {
                myStateMachine.ChangeState(allStates[2], this.gameObject);
            }
            */

            // Then procceed to do jump logic...
            if(!canJump)
            {
                return;
            }
            Vector2 jumpForce = new Vector2(myRigidbody.velocity.x, 3f);
            myRigidbody.velocity = jumpForce;
            StartCoroutine(JumpCoolDownRoutine());


        }

        private IEnumerator JumpCoolDownRoutine()
        {
            canJump = false;
            yield return new WaitForSeconds(0.3f);
            myRigidbody.gravityScale = 100f;
            yield return new WaitForSeconds(0.5f);
            myRigidbody.gravityScale = 1f;
            canJump = true;

        }

        public void OnDeath()
        {
            // Trigger the death event - lots of things will care about this

        }

        private bool IsGrounded()
        {
            //Temp..
            return true;
        }
    }
}