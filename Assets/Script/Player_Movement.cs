using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Multiple Jumps should be DISABLED!
    public float speed = 10.0f;
    private float horizontalInput;
    public float jumpforce = 500.0f;
    private Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (!Globals.canCtrl) { return; }
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        if (Input.GetKeyDown(KeyCode.Space) && Globals.canJmp)
        {
            rb2d.AddForce(new Vector2(0, jumpforce));
            Globals.canJmp = false;
            StartCoroutine(DisableJmp());
        }
        if (transform.position.y < -50)
        {
            // Debug.Log("Falling");
            Globals.paused = true;
            Globals.gameState = "dead";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Collectable"))
        {
            if (Globals.powerUp)
            {
                Globals.collected += 1;
            }
            else if (Globals.playerStatus == "normal") // player not in after-damage protection
            {
                Globals.playerStatus = "damaged";
                Globals.canCtrl = false;
                StartCoroutine(DisableCtrl());
                rb2d.velocity = new Vector2(0, 0);
                int bounceDir = (transform.position.x - other.transform.position.x < 0) ? -1 : 1;
                // Debug.Log(bounceDir);
                rb2d.AddForce(new Vector2(300.0f * bounceDir, 300.0f));
            }
        }        
    }
    
    private IEnumerator DisableCtrl()
    {
        yield return new WaitForSeconds(0.5f);
        Globals.canCtrl = true;
    }

    private IEnumerator DisableJmp()
    {
        yield return new WaitForSeconds(1.0f);
        Globals.canJmp = true;
    }
}
