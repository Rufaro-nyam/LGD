using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public ShakeData exp_data;
    public GameObject shards;
    public Transform shard_rot;

    public GameObject revolving_meteor;
    private int rot_speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shard_rot.transform.Rotate(0, 0, 90 * Time.deltaTime);
    }

    public void enable_meteor(int speed)
    {
        rot_speed = speed;
        revolving_meteor.SetActive(true);
    }

    public void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
        foreach(Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if(rb != null && rb.tag != "Gear")
            {
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                rb.AddForce(direction * 2, ForceMode2D.Impulse);
            }
        }
        CameraShakerHandler.Shake(exp_data);
        Instantiate(shards, transform.position, Quaternion.identity);
        GameObject spawner = GameObject.FindGameObjectWithTag("SPAWN");
        if (spawner)
        {
            spawner.TryGetComponent<Spawn_manager>(out Spawn_manager spmg);
            spmg.asteroid_down();
        }
        Destroy(gameObject);

    }
}
