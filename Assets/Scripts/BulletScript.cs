using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public float damage;
    public float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan += Time.deltaTime;
        if (lifespan >= 0.3)
        {
            Destroy(gameObject);
        }
    }
}
