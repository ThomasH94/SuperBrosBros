using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will be a simple class that is attached to a gameobject and tells the gameobject to unparent
/// either this gameobject or other gameobjects
/// TODO: Add methods and checks for removing and unparenting at runtime
/// Might want to make this a static class
/// </summary>
namespace SuperBrosBros.Utility
{
    public class UnparentGameObject : MonoBehaviour
    {
        public bool detachOnStart = false;
        public List<GameObject> objectsToUnParent = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            if(detachOnStart)
            {
                UnParent();
            }
        }

        // Remove the parent completely by setting it's parent to null
        void UnParent()
        {
            for (int i = 0; i < objectsToUnParent.Count; i++)
            {
                if(objectsToUnParent[i] != null)
                {
                    objectsToUnParent[i].transform.parent = null;
                }
            }
        }
    }
}