using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_Ability : MonoBehaviour
{
    
    public SpriteRenderer playerRenderer;
    public Sprite player_normal;
    public Sprite player_powerUp;
    public float pwrTimeRemain;
    public float pwrCd;
    
    void Start()
    {
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
        pwrTimeRemain = 3.0f;
        pwrCd = 3.0f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !Globals.powerUp && !Globals.cd)
        {
            Globals.powerUp = true;
            playerRenderer.sprite = player_powerUp; // FIX: display different sprite
            // Display PowerUp Face on Canvas
            StartCoroutine(pwr_Timer());
        }
    }
    
    private IEnumerator pwr_Timer()
    {
        yield return new WaitForSeconds(pwrTimeRemain);
        Globals.powerUp = false;
        playerRenderer.sprite = player_normal; // FIX: display different sprite
        pwrTimeRemain = 3.0f;
        
        Globals.cd = true;
        // Display Tired Face on Canvas
        yield return new WaitForSeconds(pwrCd);
        Globals.cd = false;
    }
}
