using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugRespawn : MonoBehaviour //debug class just to restart the scene if the player is destroyed(probably should be done in another way in future builds)
{
    public GameObject player;

    private void Update()
    {
        if (player != null) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
