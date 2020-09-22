using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script can spawn any item and can be used differently props, playable characters, etc.
/// </summary>
namespace SuperBrosBros.Props
{
    public class ItemSpawner : MonoBehaviour
    {
        public GameObject itemToSpawn;
        //AudioSource audioToPlay?

        private void Start()
        {
            itemToSpawn.gameObject.SetActive(false);
        }

        // Spawn the item, play any animations/sounds, and do whatever logic necessary
        public void SpawnItem()
        {
            itemToSpawn.SetActive(true);
        }

    }
}