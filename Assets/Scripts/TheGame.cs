using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TheGame : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;
    public GameObject player;
    public PlayerControls playerControls;
    public GameObject[] zombieControls;
    public GameObject[] targetScripts;
    public GunScript gunScript;
    public GameObject gameOverUI;
    public GameObject[] pickups;
    public GameObject[] stuff;
    public Text gameOverText;
    public Vector3 dist;
    public int level;
    public bool spawned;
    public bool completed = false;
    public bool gameOver = false;
    public float spawnedWhen;
    public float bossSpawn;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        zombieControls = GameObject.FindGameObjectsWithTag("Zombie");
        gunScript = GameObject.Find("Player Gun").GetComponent<GunScript>();
        gameOverUI.SetActive(false);
        //LevelOne();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale >= 0.02f)
        {
            healthText.text = playerControls.hp + " / " + playerControls.maxHp;
            ammoText.text = gunScript.ammo.ToString();
        }
        spawnedWhen += Time.deltaTime;
        if (spawnedWhen >= -3.7f && spawnedWhen <= -3.5f)
        {
            player.transform.position = playerControls.respawn;
        }
        if (spawnedWhen >= 1f && !spawned)
        {
            Clear();
            if (level == 1)
            {
                LevelOne();
            }
            else if (level == 2)
            {
                LevelTwo();
            }
            else if (level == 3)
            {
                LevelThree();
            }
            spawned = true;
        }
        if (spawnedWhen >= 10f && spawnedWhen <= 15f)
        {
            completed = false;
        }
        if (spawnedWhen >= bossSpawn && level == 3)
        {
            bossSpawn += Random.Range(5f, 15f);
            Add(1, new Vector3(-6.5f, 4.5f, 68f));
        }
        if (playerControls.hp <= 0)
        {
            GameOver("GAME OVER");
        }
    }
    public void Explosive(Vector3 ex)
    {
        zombieControls = GameObject.FindGameObjectsWithTag("Zombie");
        dist = new Vector3 (Mathf.Abs(ex.x - player.transform.position.x), Mathf.Abs(ex.y - player.transform.position.y), Mathf.Abs(ex.z - player.transform.position.z));
        if (dist.x <= 2f && dist.y <= 2f && dist.z <= 2f)
        {
            playerControls.hp -= 4;
        }
        else if (dist.x <= 4f && dist.y <= 4f && dist.z <= 4f)
        {
            playerControls.hp -= 2;
        }
        for (int i = 0; i < zombieControls.Length; i += 1)
        {
            dist = new Vector3(Mathf.Abs(ex.x - zombieControls[i].transform.position.x), Mathf.Abs(ex.y - zombieControls[i].transform.position.y), Mathf.Abs(ex.z - zombieControls[i].transform.position.z));
            if (dist.x <= 2f && dist.y <= 2f && dist.z <= 2f)
            {
                zombieControls[i].GetComponent<ZombieControls>().hp -= 4;
            }
            else if (dist.x <= 4f && dist.y <= 4f && dist.z <= 4f)
            {
                zombieControls[i].GetComponent<ZombieControls>().hp -= 2;
            }
        }
    }
    public void SpawnPickup(Vector3 sp, int type)
    {
        Instantiate(pickups[type - 1], sp, Quaternion.identity);
    }
    public void Clear()
    {
        zombieControls = GameObject.FindGameObjectsWithTag("Zombie");
        targetScripts = GameObject.FindGameObjectsWithTag("Explosive");
        for (int i = 0; i < zombieControls.Length; i += 1)
        {
            Destroy(zombieControls[i].gameObject);
        }
        for (int i = 0; i < targetScripts.Length; i += 1)
        {
            Destroy(targetScripts[i].gameObject);
        }
    }
    public void Add(int type, Vector3 pos)
    {
        Instantiate(stuff[type], pos, Quaternion.identity);
    }
    public void LevelOne()
    {
        //level = 1;
        Add(0, new Vector3(-0.45f, 0.56f, 7.17f));
        Add(0, new Vector3(2.95f, 1.75f, 17.36f));
        Add(0, new Vector3(11.8f, 2.5f, 11.06f));
        Add(1, new Vector3(2f, 2.5f, 18.21f));
        Add(1, new Vector3(6.62f, 2.5f, 12.83f));
        Add(1, new Vector3(6.46f, 2.5f, 15.47f));
        Add(2, new Vector3(14.1f, 3.2f, 9.22f));
        spawned = true;
    }
    public void LevelTwo()
    {
        //level = 2;
        Add(0, new Vector3(67.5f, 3.34f, 16.6f));
        Add(0, new Vector3(71.4f, 3.34f, 34f));
        Add(0, new Vector3(69.8f, 3.34f, 47.5f));
        Add(0, new Vector3(53f, 3.34f, 78f));
        Add(0, new Vector3(39f, 3.34f, 67f));
        Add(1, new Vector3(63.4f, 4f, 29f));
        Add(1, new Vector3(64f, 4f, 40f));
        Add(1, new Vector3(67f, 4f, 43.3f));
        Add(1, new Vector3(51.75f, 4f, 71.7f));
        Add(1, new Vector3(49f, 4f, 69f));
        Add(2, new Vector3(63.5f, 4f, 17.8f));
        Add(2, new Vector3(53.4f, 4f, 57.2f));
        Add(2, new Vector3(33f, 4f, 81f));
        spawned = true;
    }
    public void LevelThree()
    {
        //level = 3;
        Add(0, new Vector3(-5.2f, 0.82f, 69.3f));
        Add(0, new Vector3(-5.2f, 0.82f, 66.7f));
        Add(0, new Vector3(-7.8f, 0.82f, 66.7f));
        Add(0, new Vector3(-7.8f, 0.82f, 69.3f));
        Add(3, new Vector3(-6.5f, 1.5f, 68f));
    }
    public void GameOver(string text)
    {
        //player.transform.position = new Vector3(200f, 200f, 200f);
        Time.timeScale = 0f;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
        gameOverText.text = text;
    }
}
