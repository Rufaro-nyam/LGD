using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class Asteroid_shard_spawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null && rb.tag == "SHARD")
            {
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                rb.AddForce(direction * 7, ForceMode2D.Impulse);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
