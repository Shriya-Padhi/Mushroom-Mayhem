using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnBtn : MonoBehaviour
{

    public Button respawnBtn;
    // Start is called before the first frame update
    void Start()
    {
        respawnBtn.GetComponent<Button>();
        respawnBtn.onClick.AddListener(Respawn);
    }

    void Respawn()
    {
        SceneManager.LoadScene(0);
    }
}
