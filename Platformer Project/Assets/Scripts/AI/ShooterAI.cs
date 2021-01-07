using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{

    [SerializeField] private Transform target;
    private Vector2 vectorTowardsPlayer;

    ///AI Gun Components
    [SerializeField] private float fireSpeed;
    private bool reloading;
    private GameObject gunObject;
    private AIGun gun;

    private void Awake()
    {
        gun = GetComponentInChildren<AIGun>();
        gunObject = gun.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            vectorTowardsPlayer = target.position - gunObject.transform.position;
            if(!reloading)
                StartCoroutine(Shoot());
        }

        AimGun();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
        }
    }

    /// <summary>
    /// This Rotates the AI gun bases on the Target Position
    /// </summary>
    private void AimGun()
    {
        float angle = Mathf.Atan2(vectorTowardsPlayer.y, vectorTowardsPlayer.x) * Mathf.Rad2Deg;
        gunObject.transform.position = transform.position;
        gunObject.GetComponent<Rigidbody2D>().rotation = angle;


    }

    /// <summary>
    /// Start shooting at a set interval, fireSpeed is the reload time
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        float timer = 0;
        reloading = true;
        while (timer < fireSpeed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        gun.Fire();
        reloading = false;
    }
}
