using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

public class ShuttleMvt : MonoBehaviour
{
    public float movement_force = 5f;
    public float max_speed = 10f;
    public float rotationspeed = 200f;
    public float max_ang_velocity = 100;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public float ray_length = 1f;
    public LayerMask asteroid_mask;
    public GameObject landing_gear;

    private bool landed = false;
    private bool connected_to_asteroid = false;
    float connection_time = 0;


    public Image fuel_main;
    //public Image fuel_background;
    public Canvas fuel_canvas;
    private float max_fuel = 100;
    private float current_fuel = 0;
    private float consumption_rate = 8;

    public TextMeshProUGUI speedometer;
    public TextMeshProUGUI Out_of_bounds_time;
    public float o_o_b_time = 5f;
    private bool counting_down = false;

    public ShakeData Thrust_shake;
    public ParticleSystem[] thrust_particles;

    public AudioSource thrust;
    public AudioSource landing_sound;
    public AudioSource shuttle_impact;
    public AudioSource FUEL_RECHARGE;
    public AudioSource Speed_warning;
    public AudioSource Bounds_beep_main;
    public AudioSource bounds_return;

    public AudioSource[] speech;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_fuel = max_fuel;

    }

    // Update is called once per frame
    void Update()
    {
        fuel_main.fillAmount = Mathf.Clamp(current_fuel / max_fuel, 0, 1);
        FUEL_RECHARGE.pitch = Mathf.Clamp(FUEL_RECHARGE.pitch, 1, 6);
        fuel_canvas.transform.position = transform.position;
        //fuel_main.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
        //fuel_background.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);


        //print(rb.linearVelocity.magnitude * 10);
        speedometer.text = rb.linearVelocity.magnitude.ToString("0.0.0");
        if(rb.linearVelocity.magnitude >= 1.99)
        {
            Speed_warning.volume = 1;
        }
        else
        {
            Speed_warning.volume = 0;
        }


        if (counting_down && o_o_b_time >= 0)
        {
            Out_of_bounds_time.text = o_o_b_time.ToString("0.00");
            o_o_b_time -= Time.deltaTime;
            Bounds_beep_main.volume = 1;
        }
        else
        {
            Bounds_beep_main.volume = 0;
        }

            moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        landing_gear.transform.rotation = transform.rotation;

        Vector2 raydirection = transform.up;
        Vector2 rayorigin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayorigin, raydirection, ray_length, asteroid_mask);
        Debug.DrawRay(rayorigin, raydirection * ray_length, Color.red);

        if(hit.collider != null)
        {
            landed = true;
            //print("hit " + hit.collider.name);
        }
        else
        {
            landed = false;
        }

        if (connected_to_asteroid)
        {
            connection_time += Time.deltaTime;
            current_fuel = Mathf.Lerp(current_fuel, max_fuel, 0.005f);
            FUEL_RECHARGE.pitch += Time.deltaTime * 2 ;
            FUEL_RECHARGE.volume = 1;
            print(connection_time);
            if (connection_time >= 3)
            {
                FixedJoint2D joint = gameObject.GetComponent<FixedJoint2D>();
                if (joint != null)
                {
                    Destroy(joint);
                    FUEL_RECHARGE.volume = 0;
                    FUEL_RECHARGE.pitch = 1;
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid);
                        asteroid.explode();
                        
                        //current_fuel = max_fuel;
                    }
                    connection_time = 0;
                    connected_to_asteroid = false;
                    int rand_speech = Random.Range(0, speech.Length);
                    speech[rand_speech].Play();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(moveInput != Vector2.zero)
        {
            Vector2 force2add = moveInput * movement_force;
            rb.AddForce(force2add, ForceMode2D.Impulse);
            current_fuel -= consumption_rate * Time.fixedDeltaTime;
            CameraShakerHandler.Shake(Thrust_shake);
            
        }
        if(moveInput.y != 0 || moveInput.x != 0)
        {
            thrust.volume = 1;
        }
        else
        {
            thrust.volume = Mathf.Lerp(thrust.volume, 0, 0.1f);
        }

        if(moveInput.y > 0)
        {
            thrust_particles[0].Play();
 
            
        }
        else
        {
            thrust_particles[0].Stop();

        }
        if (moveInput.y < 0)
        {
            thrust_particles[1].Play();
            
        }
        else
        {
            thrust_particles[1].Stop();

        }
        if (moveInput.x < 0)
        {
            thrust_particles[3].Play();

            
        }
        else
        {
            thrust_particles[3].Stop();

        }
        if (moveInput.x > 0)
        {
            thrust_particles[2].Play();
            
        }
        else
        {
            thrust_particles[2].Stop();

        }

        if (Input.GetKey(KeyCode.Space))
        {
            //rb.linearVelocity.magnitude = Mathf.Lerp(rb.linearVelocity.magnitude, 0, 0.01f);
        }
        if(rb.linearVelocity.magnitude > max_speed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, max_speed);
        }
        if (Input.GetMouseButton(0))
        {
            rb.AddTorque(rotationspeed);
            current_fuel -= consumption_rate * Time.fixedDeltaTime;
            CameraShakerHandler.Shake(Thrust_shake);
            thrust_particles[4].Play();

            
        }
        else
        {
            thrust_particles[4].Stop();

        }
        if (Input.GetMouseButton(1))
        {
            rb.AddTorque(-rotationspeed);
            current_fuel -= consumption_rate * Time.fixedDeltaTime;
            CameraShakerHandler.Shake(Thrust_shake);
            thrust_particles[5].Play();
        }
        else
        {
            thrust_particles[5].Stop();

        }
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -max_ang_velocity, max_ang_velocity);
        if(Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            thrust.volume = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if (landed)
        {
            if(collision.relativeVelocity.magnitude > 2)
            {
                shuttle_impact.pitch = UnityEngine.Random.RandomRange(1f, 1.25f);
                shuttle_impact.Play();
                print("CRASHED");
            }
            else
            {
                print("Good_landing");
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                Rigidbody2D other_rb = collision.collider.GetComponent<Rigidbody2D>();
                if(other_rb != null)
                {
                    joint.connectedBody = other_rb;
                    connected_to_asteroid = true;
                    landing_sound.Play();
                    print("CONNECTED");
                }
            }
        }
        else
        {
            print("CRASHED");
            shuttle_impact.pitch = UnityEngine.Random.RandomRange(1f, 1.25f);
            shuttle_impact.Play();
        }

    }

    private void OnBecameInvisible()
    {
        print("WARNING : IN DEEP SPACE");
        Out_of_bounds_time.gameObject.SetActive(true);
        counting_down = true;
    }

    private void OnBecameVisible()
    {
        print("BACK IN MISSION ZONE");
        Out_of_bounds_time.gameObject.SetActive(false);
        counting_down = false;
        o_o_b_time = 5;
        bounds_return.Play();
    }

}
