using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //public Rigidbody rb;
    public Animator am;
    public CharacterController controller;
    public TheGame game;
    public Vector3 rotateInput;
    public Vector3 respawn;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpHeight;
    public float grav;
    public Transform gc;
    public float gd = 0.4f;
    public LayerMask gm;
    public int hp = 10;
    public int maxHp = 10;
    public float regenTimer = 0f;
    bool touchingGround;
    float x;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
        touchingGround = true;
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        touchingGround = Physics.CheckSphere(gc.position, gd, gm);
        if(touchingGround && grav < 0)
        {
            grav = -1.5f;
        }
        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal") * -1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            x *= 1.9f;
            z *= 1.9f;
        }
        //Vector3 force = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        Vector3 force = ((transform.right * z) + (transform.forward * x)) * moveSpeed * Time.deltaTime;
        controller.Move(force + (Vector3.up * grav * Time.deltaTime));
        x /= 1.5f;
        z /= 1.5f;
        if (x <= 0.01f && z <= 0.01f && x >= -0.01f && z >= -0.01f)
        {
            am.SetBool("Moving", false);
        }
        else
        {
            am.SetBool("Moving", true);
        }
        if (grav >= -4f)
        {
            grav -= 12.5f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround)
        {
            grav = Mathf.Sqrt(jumpHeight * 25f);
            touchingGround = false;
        }
        if (transform.position.y <= -5f)
        {
            transform.position = respawn;
            hp -= 5;
        }
        if (Time.time >= regenTimer && hp < maxHp)
        {
            hp += 1;
            regenTimer = Time.time + 5f;
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "EndPlatform")
    //    {
    //        game.Clear();
    //        game.LevelTwo();
    //        respawn = new Vector3(48.4f, 4.1f, 11.23f);
    //        transform.position = respawn;
    //    }
    //}
}
