using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGunScript : MonoBehaviour
{
    public int damage = 1;
    public int ammo;
    public int maxAmmo;
    public float shootSpeed = 1000f;
    public float range = 50f;
    public float shootRate = 1f;
    private float nextTime = 0f;
    public float crt;
    public Camera cam;
    public CameraControls cameraControls;
    public GameObject bullet;
    public ParticleSystem particle;
    public GameObject explode;
    public GameObject player;
    public Transform gunPoint;
    public TheGame game;
    public LayerMask lm;
    public AudioSource source;
    public AudioClip clip;
    bool lol;
    void Start()
    {
        player = GameObject.Find("Player");
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
        //cameraControls = cam.GetComponent<CameraControls>();
        //cam.Render();
        ammo = maxAmmo;
    }
    void Update()
    {
        if (ammo >= 1 && Time.time >= nextTime)
        {
            nextTime = Time.time + 1f / shootRate;
            Shoot();
            if (maxAmmo == 15 && ammo <= 0)
            {
                ammo = maxAmmo;
                nextTime += 7.5f;
            }
        }
    }
    void Shoot()
    {
        //GameObject bulletSpawned = Instantiate(bullet, gunPoint.position, cam.transform.rotation);
        //crosshair.transform.localPosition = new Vector3(-0.4f, cam.transform.rotation.x, 0f);
        //bulletSpawned.transform.LookAt(gunPoint.position);
        //Rigidbody rb = bulletSpawned.GetComponent<Rigidbody>();
        //rb.AddForce(gunPoint.right * shootSpeed);
        RaycastHit hit;
        //cam.transform.rotation = Quaternion.Euler();
        //cam.transform.Rotate(0f, -90f, crt * -1);
        cam.transform.localRotation = Quaternion.Euler(0f, Random.Range(-2f, 2f), Random.Range(-4f, 4f));
        //cam.transform.Translate(Vector3.right * 0.55f);
        if (Physics.Raycast(cam.transform.position, cam.transform.right, out hit, range, lm))
        {
            Debug.Log(hit.transform.name);
            TargetScript target = hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            PlayerControls pc = hit.transform.GetComponent<PlayerControls>();
            if (pc != null)
            {
                pc.hp -= damage;
                ammo -= 1;
                lol = true;
                particle.Play();
                source.PlayOneShot(clip);
            }
            else
            {
                lol = false;
            }
        }
        if (maxAmmo == 15)
        {
            ammo -= 1;
            source.PlayOneShot(clip);
        }
        Debug.DrawRay(cam.transform.position, cam.transform.right * range, Color.red, 1);
        cam.transform.localRotation = Quaternion.Euler(0f, Random.Range(88f, 92f), Random.Range(-4f, 4f));
        //cam.transform.Rotate(0f, 90f, crt);
        //if (lol)
        //{
            //particle.Play();
            //GameObject impact = Instantiate(explode, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impact, 1f);
        //}
    }
}
