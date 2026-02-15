using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Spawn_manager spawner;
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
        if(collision.transform.tag != "Player" || collision.transform.tag != "ASTEROID")
        {
            //print(collision.gameObject.name + "destroyed");
            Destroy(collision.gameObject);
        }
        if(collision.transform.tag == "ASTEROID")
        {
            spawner.asteroid_down();
        }
        

    }
}
