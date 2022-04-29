using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int gunType = 0;
    public float damage = 1f;
    public int ammo;
    public int maxAmmo;
    //public float shootSpeed = 1000f;
    public float range = 100f;
    public float shootRate = 5f;
    private float nextTime = 0f;
    public Renderer[] rds;
    public Color stateColor;
    public Camera cam;
    public CameraControls cameraControls;
    public GameObject bullet;
    public ParticleSystem particle;
    public GameObject explode;
    public Transform gunPoint;
    public TheGame game;
    public LayerMask lm;
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
        cameraControls = cam.GetComponent<CameraControls>();
        cam.Render();
        ammo = maxAmmo;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && ammo >= 1 && Time.timeScale >= 0.02f && Time.time >= nextTime)
        {
            nextTime = Time.time + 1f / shootRate;
            ammo -= 1;
            game.ammoText.text = ammo.ToString();
            Shoot();
        }
    }
    void Shoot()
    {
        //GameObject bulletSpawned = Instantiate(bullet, gunPoint.position, cam.transform.rotation);
        //crosshair.transform.localPosition = new Vector3(-0.4f, cam.transform.rotation.x, 0f);
        //bulletSpawned.transform.LookAt(gunPoint.position);
        //Rigidbody rb = bulletSpawned.GetComponent<Rigidbody>();
        //rb.AddForce(gunPoint.right * shootSpeed);
        particle.Play();
        source.PlayOneShot(clip);
        RaycastHit hit;
        cam.transform.Rotate(0f, -90f, 0f);
        //cam.transform.Translate(Vector3.right * 0.55f);
        if (Physics.Raycast(cam.transform.position, cam.transform.right, out hit, range, lm))
        {
            Debug.Log(hit.transform.name);
            TargetScript target = hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            ZombieControls zc = hit.transform.GetComponent<ZombieControls>();
            if (zc != null)
            {
                zc.hp -= damage;
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.right * range, Color.red, 1);
        //cam.transform.Translate(Vector3.left * 0.55f);
        cam.transform.Rotate(0f, 90f, 0f);
        GameObject impact = Instantiate(explode, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
        cam.Render();
    }
    public void ChangeGun()
    {
        if (gunType == 1)
        {
            gunType = 2;
            damage = 1f;
            shootRate = 10f;
            range = 40f;
            maxAmmo = 200;
            stateColor = new Color(248f / 255f, 104f / 255f, 0f, 1);
        }
        else
        {
            gunType = 1;
            damage = 4f;
            shootRate = 2f;
            range = 50f;
            maxAmmo = 100;
            stateColor = new Color(0f, 180f / 255f, 248f / 255f, 1);
        }
        if (ammo < maxAmmo)
        {
        ammo = maxAmmo;
        }
        rds[0].material.color = stateColor;
        rds[1].material.color = stateColor;
    }
}
