using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scriptable object will hold the data for any character that needs data for
/// moving, jumping, etc.
/// </summary>
namespace SuperBrosBros.ScriptableObjects.CharacterStats
{
    // Create a new scriptable object via the create menu and set it to our predefined scriptable objects folder
    [Serializable,CreateAssetMenu(fileName = "New Character Stats", menuName = "Scriptable Objects/Character Stats")]
    public class BaseCharacterStats : ScriptableObject
    {
        // Name of the character/prop
        public string characterName;
        // Description of the character/prop
        [TextArea]
        public string characterDescription;
        // Default movement speed
        public float moveSpeed;
        // Restrict the maximum movement speed to keep things precise and predictable when moving
        public float maxMoveSpeed;
        // Used in cases where we need objects to fall faster or even fall up
        public float gravityMultiplier;

        // Set the layer mask this character should be on in Start - for physics
        public LayerMask characterLayer;

    }
}