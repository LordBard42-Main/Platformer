using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    ///Player Components
    private Rigidbody2D rb;
    private ColorProperties colorProperties;
    [SerializeField] private new Camera camera;
    private AudioSource audioSource;

    ///Components in the Scene
    private SceneFader sceneFader;

    /// Script Variables
    [SerializeField] private int colorSelector = 1;
    private Vector2 vectorTowardsMouse;

    ///Player Gun Components
    [SerializeField] private GameObject gunObject;
    private Gun gun;


    private readonly float MAXSPEED = 18f;
    private int movementX;
    private int movementY;
    public float thrustX = 5;
    public float thrustY = 5;
    private bool jump;
    private bool isGrounded;

    public ColorProperties ColorProperties { get => colorProperties; set => colorProperties = value; }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colorProperties = GetComponent<ColorProperties>();
        gun = GetComponentInChildren<Gun>();
        audioSource = GetComponent<AudioSource>();
        sceneFader = GameObject.FindGameObjectWithTag("Scene_Fader").GetComponent<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = (int)Input.GetAxisRaw("Horizontal");
        vectorTowardsMouse = camera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10) - gunObject.transform.position;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if(Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            gun.Fire();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            gun.SelfApply();
        }
        
        AimGun();

    }
    

    private void FixedUpdate()
    {

        rb.AddForce(transform.right * thrustX * movementX);
        
        if(jump && isGrounded)
        {
            rb.AddForce(transform.up * thrustY * 100);
            jump = false;
        }

        if (rb.velocity.magnitude > MAXSPEED)
        {
            rb.velocity = rb.velocity.normalized * MAXSPEED;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Setting Grounded");
        }

        if(collision.tag == "Goal")
        {
            Goal goal = collision.gameObject.GetComponent<Goal>();
            audioSource.clip = goal.GoalSound;
            audioSource.Play();
            sceneFader.FadeTo(goal.GoToScene);
            Destroy(collision.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    /// <summary>
    /// Move Mouse wheel to scroll through colros
    /// </summary>
    private void ScrollThroughColors()
    {
        var value = colorSelector + (int)Input.mouseScrollDelta.y * 1;

        if (value > 3)
        {
            value = 1;
        }
        else if (value < 1)
        {
            value = 3;
        }
        if (colorSelector != value)
        {
            colorSelector = value;
            colorProperties.UpdateColor(colorSelector);
        }
    }

    /// <summary>
    /// This Rotates the Players gun bases on the Mouse Position
    /// </summary>
    private void AimGun()
    {
        float angle = Mathf.Atan2(vectorTowardsMouse.y, vectorTowardsMouse.x) * Mathf.Rad2Deg;
        gunObject.transform.position = transform.position;
        gunObject.GetComponent<Rigidbody2D>().rotation = angle;
        

    }
}
