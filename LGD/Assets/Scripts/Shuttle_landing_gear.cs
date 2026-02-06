using UnityEngine;

public class Shuttle_landing_gear : MonoBehaviour
{
    private AudioSource Click;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Click = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ASTEROID")
        {
            Click.Play();
            //print("asteroid_detected");
        }
    }
}
