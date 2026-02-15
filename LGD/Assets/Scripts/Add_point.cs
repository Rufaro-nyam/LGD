using System.Collections;
using UnityEngine;

public class Add_point : MonoBehaviour
{
    [SerializeField] LeanTweenType easetype;
    public int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(0.030f, 0.030f, 0.5f), 0.8f).setEase(easetype).setOnComplete(start_wait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_wait()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            player.TryGetComponent<ShuttleMvt>(out ShuttleMvt shtlmvt);
            if (shtlmvt)
            {
                shtlmvt.add_score(score);
            }
        }
        StartCoroutine(wait());
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        add_point();

    }

    public void add_point()
    {
        LeanTween.scale(gameObject, new Vector3(0.0f, 0.0f, 0.0f), 0.2f).setEase(easetype).setOnComplete(gone);

    }

    public void gone()
    {
        Destroy(gameObject);
    }
}
