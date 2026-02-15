using UnityEngine;

public class Object_size_random : MonoBehaviour
{
    public float min;
    public float max;
    private TrailRenderer trail;
    private Rigidbody2D rb;
    public ParticleSystem col_part;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trail = gameObject.GetComponent<TrailRenderer>();
        float rand_scale = Random.Range(min, max);
        transform.localScale = new Vector3(rand_scale, rand_scale, rand_scale);
        trail.startWidth = transform.localScale.x;
        rb.AddTorque(Random.Range(-rand_scale * 10, rand_scale * 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (col_part)
        {
            col_part.transform.position = collision.GetContact(0).point;
            col_part.Play();
        }
        
    }
}
