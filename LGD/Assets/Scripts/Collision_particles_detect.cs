using UnityEngine;

public class Collision_particles_detect : MonoBehaviour
{
    public ParticleSystem col_part;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        col_part.transform.position = collision.GetContact(0).point;
        col_part.Play();
    }
}
