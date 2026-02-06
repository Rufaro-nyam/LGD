using UnityEngine;

public class Meteor : MonoBehaviour
{
    private bool has_been_visible = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        has_been_visible = true;
    }

    private void OnBecameInvisible()
    {
        if (has_been_visible)
        {
            print("meteor destroyed");
            Destroy(gameObject);
        }
    }
}
