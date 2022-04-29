using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatformScript : MonoBehaviour
{
    public TheGame game;
    public GunScript gunScript;
    public PlayerControls playerControls;
    public GameObject player;
    public int toLevel;
    // Start is called before the first frame update
    void Start()
    {
        gunScript = GameObject.Find("Player Gun").GetComponent<GunScript>();
        player = GameObject.Find("Player");
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //player.transform.position = Vector3.up * 200f;
            if (gunScript.ammo < gunScript.maxAmmo)
            {
                gunScript.ammo = gunScript.maxAmmo;
            }
            if (toLevel == 2)
            {
                game.level = 2;
                //game.LevelTwo();
                playerControls.respawn = new Vector3(48.4f, 4.1f, 11.23f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(48.4f, 4.1f, 11.23f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(48.4f, 4.1f, 11.23f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(48.4f, 4.1f, 11.23f);
                player.transform.position = playerControls.respawn;
            }
            else if (toLevel == 3)
            {
                game.level = 3;
                //game.LevelThree();
                playerControls.respawn = new Vector3(-15.5f, 1.9f, 67.5f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(-15.5f, 1.9f, 67.5f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(-15.5f, 1.9f, 67.5f);
                player.transform.position = playerControls.respawn;
                playerControls.respawn = new Vector3(-15.5f, 1.9f, 67.5f);
                player.transform.position = playerControls.respawn;
            }
            game.Clear();
            game.spawnedWhen = -4f;
            //game.completed = true;
            game.spawned = false;
        }
    }
}
