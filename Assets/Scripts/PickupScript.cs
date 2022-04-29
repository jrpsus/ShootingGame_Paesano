using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int pickupType;
    public GunScript gunScript;
    public PlayerControls playerControls;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        gunScript = GameObject.Find("Player Gun").GetComponent<GunScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (pickupType == 1)
            {
                gunScript.ammo += 10;
            }
            else if (pickupType == 2)
            {
                playerControls.hp += 5;
                if (playerControls.hp >= playerControls.maxHp)
                {
                    playerControls.hp = playerControls.maxHp;
                }
            }
            else if (pickupType == 3)
            {
                playerControls.hp += 1;
                playerControls.maxHp += 1;
            }
            else if (pickupType == 4)
            {
                gunScript.ChangeGun();
            }
            Destroy(gameObject);
        }
    }
}
