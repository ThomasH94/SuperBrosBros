using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will exist as a component on any object that will use gravity
/// We can control the gravity of the object to improve game "feel"
/// This component will mostly be used for character controllers to make jumping and falling feel better
/// Link to the Unity Tutorial(playlist) for reference: https://www.youtube.com/watch?v=wGI2e3Dzk_w&list=PLX2vGYjWbI0SUWwVPCERK88Qw8hpjEGd8&index=1
/// THE ADVANTAGE OF THIS SCRIPT IS WE GET FULL CONTROL OVER OUR PHYSICS BY WRITING THEM BY HAND!
/// This will not work with physics materials, so bounciness, sliding, etc. need to be done by hand as well
/// </summary>
namespace SuperBrosBros.Physics
{
    public class CharacterGravityController : MonoBehaviour
    {
        #region Gravity Affectors
        [Header("Gravity Affectors")]
        // Used to modify gravity when jumping to fall faster, float up, etc.
        public float gravityModifier = 1.0f;
        protected Vector2 gravityForce;
        public float minGroundNormalY;

        protected bool isGrounded;
        protected Vector2 groundNormal;
        #endregion
        // ================================================================================================================================

        #region Rigidbody Information
        protected Rigidbody2D gravityBasedRigidBody;
        // Necessary for rigidbody casting results
        protected ContactFilter2D contactFilter;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];  //Using 16 as our default max amount but could change this
        protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);    // To match our hitBuffer, but only for the contacts that actually hit
        #endregion
        //=================================================================================================================================

        #region Consts
        // Make sure we don't move if we aren't passing this threshold
        protected const float MINIMUM_MOVE_DISTANCE = 0.001f;
        // Padding to prevent us from passing to another collider
        protected const float SHELL_RADIUS = 0.01f;
        #endregion

        private void OnEnable()
        {
            gravityBasedRigidBody = GetComponent<Rigidbody2D>();
        }

        protected void Start()
        {
            CalculateContactFilter();
        }

        // This method will retrieve all of the information for our contact filters when casting Rays via Rigidbodies
        protected void CalculateContactFilter()
        {
            // Don't look for triggers when applying gravity
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));  // Set this gameobjects layer to ensure this object is on the layer you want for collisions
            contactFilter.useLayerMask = true;

            groundNormal = new Vector2(0f, 1f); // Assumes there's a flat ground somewhere beneath there object to allow the player to move when falling for the first time
        }

        protected void FixedUpdate()
        {
            CalculateGravity();
        }

        // Called each physics step to constatly apply gravity
        // The formula takes our current projects gravity and multiplies by Time.deltaTime
        // which in FixedUpdate will be Time.fixedDeltaTime
        protected void CalculateGravity()
        {
            gravityForce += gravityModifier * Physics2D.gravity * Time.deltaTime;
            isGrounded = false; // Grounded starts as false until we detect collision

            Vector2 deltaPosition = gravityForce * Time.deltaTime;
            Vector2 calculatedGravity = Vector2.up * deltaPosition.y;

            ApplyGravity(calculatedGravity, true);
        }

        // After gravity is calculated, apply it by moving our gameobject based on the calculation
        protected void ApplyGravity(Vector2 calculatedGravity, bool movingOnY)
        {
            // We don't want to check gravity every frame, especially if we're idling
            // so we will check if our distance is greater then our minimum move distance
            float distance = calculatedGravity.magnitude;   //sqrMagnitude is faster but we're using this for now(?)
            float calculatedDistance = 0;
            if(distance > MINIMUM_MOVE_DISTANCE)
            {
                calculatedDistance = CheckForCollisions(calculatedGravity, distance, movingOnY);
            }

            gravityBasedRigidBody.position += calculatedGravity.normalized * calculatedDistance;
        }

        // Check our collisions and return the distance for how far we need to move
        protected float CheckForCollisions(Vector2 calculatedGravity, float distance, bool movingOnY)
        {
            // Cast from our rigidbody using our contact filter, checking how many things we hit, then using our shell radius
            // to prevent us from casting outside of any hit colliders
            int hitCount = gravityBasedRigidBody.Cast(calculatedGravity, contactFilter, hitBuffer, distance + SHELL_RADIUS);
            hitBufferList.Clear();  // Clear the list on every physics check
            // We only care about what we actually hit from the hitCount calculation
            for (int i = 0; i < hitCount; i++)
            {
                // Copy every hit from the array to our list
                hitBufferList.Add(hitBuffer[i]);
            }

            // Check the normals of all of our raycast hits against a minimum value to determine if we're grounded
            // This is to check the normal so we know we're grounded only when standing on top of the object, not next to it
            // or any other checks

            // This will only allow us to move on slopes when moving, so we need to adjust if we want to slide constantly
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    if(movingOnY)
                    {
                        // Set our normals
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                // Dot product (difference between velocity and current normal) to see if we need to subtract from our velocity
                // to prevent going through other colliders
                // I.E. - hit our head and get moved down from the ceiling
                float projection = Vector2.Dot(gravityForce, currentNormal);
                if(projection < 0)  // Is our calculation negative?
                {
                    gravityForce -= projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - SHELL_RADIUS;
                distance = modifiedDistance < distance ? modifiedDistance : distance;   // Check if our distance is smaller than the modified distance and set accordingly 
            }

            return distance;
        }
    }
}