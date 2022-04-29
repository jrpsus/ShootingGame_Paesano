using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public TheGame game;
    public float health;
    void Start()
    {
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
    }
    public void TakeDamage(float amt)
    {
        health -= amt;
        if (health <= 0)
        {
            game.Explosive(transform.position);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
