using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (Globals.powerUp)
            {
                // Globals.collected += 1;
                Destroy(gameObject);
            }
            else if (Globals.playerStatus == "normal") // player not in after-damage protection
            {
                // Globals.playerStatus = "damaged";
            }
        }
    }
}
