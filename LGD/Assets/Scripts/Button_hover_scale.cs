using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_hover_scale : MonoBehaviour
{
    public float size_diff = 1.05f;
    private Vector2 original_size;
    private RectTransform rect_transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect_transform = GetComponent<RectTransform>();
        original_size = rect_transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scale_up()
    {
        rect_transform.localScale = original_size * size_diff;
        print("mouse detected");
    }

    void scale_down()
    {
        rect_transform.localScale = original_size;
    }
}
