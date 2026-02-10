using UnityEngine;

public class Tutorial_canvas : MonoBehaviour
{
    public GameObject[] slides;
    private int current_slide = 0;
    public GameObject main_menu_stuff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset_slides();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset_slides()
    {
        foreach(GameObject g in slides)
        {
            g.SetActive(false);
        }
        current_slide = 0;
        slides[current_slide].SetActive(true);
    }
    public void next()
    {
        if(current_slide >= slides.Length - 1)
        {
            current_slide = 0;
        }
        print(current_slide);
        current_slide++;
        foreach (GameObject g in slides)
        {
            g.SetActive(false);
        }
        slides[current_slide].SetActive(true);
    }

    public void back()
    {
        Reset_slides();
        gameObject.SetActive(false);
        main_menu_stuff.SetActive(true);
    }

    public void activate()
    {
        gameObject.SetActive(true);
        Reset_slides();
        main_menu_stuff.SetActive(false);
    }
}
