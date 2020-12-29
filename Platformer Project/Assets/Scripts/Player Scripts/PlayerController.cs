using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    ///Player Components
    private Rigidbody2D rb;
    private new Camera camera;
    private AudioSource audioSource;

    ///Components in the Scene
    private SceneHandler sceneFader;

    /// Script Variables
    private int colorSelector = 0;
    private Vector2 vectorTowardsMouse;

    ///Player Gun Components
    private GameObject gunObject;
    private Gun gun;

    [Header("Jump And Move Speed")]
    [SerializeField] private float thrustX = 5;
    [SerializeField] private float thrustY = 5;

    private readonly float MAXSPEED = 18f;
    private int movementX;
    private int movementY;
    private bool jump;
    private bool isGrounded;

    public ColorProperties ColorProperties { get; private set; }

    //Get Components attached to the player
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gun = GetComponentInChildren<Gun>();
        gunObject = GetComponentInChildren<Gun>().gameObject;
        ColorProperties = GetComponent<ColorProperties>();
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // Get Componenets elsewhere in the scene
    void Start()
    {
        sceneFader = GameObject.FindGameObjectWithTag("Scene_Handler").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = (int)Input.GetAxisRaw("Horizontal");
        Debug.Log(camera);
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
        ScrollThroughColors();
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
    /// Move Mouse wheel to scroll through colors
    /// </summary>
    private void ScrollThroughColors()
    {
        var value = colorSelector + (int)Input.mouseScrollDelta.y * 1;

        if (value > 2)
        {
            value = 0;
        }
        else if (value < 0)
        {
            value = 2;
        }
        if (colorSelector != value)
        {
            gun.SelectAmmoType(colorSelector, value);
            colorSelector = value;
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
