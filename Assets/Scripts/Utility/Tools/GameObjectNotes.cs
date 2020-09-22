using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will serve as a text object that can be attached to other objects so a developer/designer can leave notes
/// about the object I.E. what it does/can do
/// </summary>
namespace SuperBrosBros.Utility.Tools
{
    public class GameObjectNotes : MonoBehaviour
    {
        [TextArea(1,10)]    // Min/Max amount of lines
        public string Notes;
    }
}