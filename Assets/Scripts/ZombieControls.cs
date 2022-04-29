using UnityEngine.AI;
using UnityEngine;

public class ZombieControls : MonoBehaviour
{
    public float cooldown;
    public int type;
    public float hp = 4;
    public Camera cam;
    public GameObject player;
    public ZombieGunScript zombieGunScript;
    public NavMeshAgent agent;
    public TheGame game;
    public Animator am;
    void Start()
    {
        player = GameObject.Find("Player");
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
        agent.updateRotation = false;
    }
    void Update()
    {
        Vector3 dist = new Vector3(Mathf.Abs(transform.position.x - player.transform.position.x), Mathf.Abs(transform.position.y - player.transform.position.y), Mathf.Abs(transform.position.z - player.transform.position.z));
        if (Time.time >= cooldown && dist.x <= 10f && dist.y <= 10f && dist.z <= 10f)
        {
            cooldown = Time.time + 1f;
            agent.SetDestination(player.transform.position);
            am.SetBool("Moving", true);
            //if (transform)
            //Ray ray = cam.ScreenPointToRay(player.transform.position);
        }
        this.transform.LookAt(new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z));
        zombieGunScript.crt = transform.rotation.y;
        this.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        this.transform.Rotate(0f, -90f, 0f);
        if (hp <= 0)
        {
            int p = 1;
            float rng = Random.Range(0f, 100f);
            if (rng <= 30f)
            {
                p = 1;
            }
            else if (rng <= 60f)
            {
                p = 2;
            }
            else if (rng <= 75f)
            {
                p = 3;
            }
            else if (rng <= 100f && type == 2)
            {
                p = 4;
            }
            if (type == 3)
            {
                game.GameOver("GAME COMPLETED");
            }
            game.SpawnPickup(transform.position, p);
            Destroy(gameObject);
        }
    }
}
