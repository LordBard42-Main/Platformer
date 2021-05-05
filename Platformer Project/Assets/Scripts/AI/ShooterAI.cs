using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerMask;
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

            if(!reloading && CheckLineOfSight())
                StartCoroutine(Shoot());
        }

        AimGun();
    }


    private void OnTriggerStay2D(Collider2D collision)
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

    private bool CheckLineOfSight()
    {
       RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorTowardsPlayer, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, vectorTowardsPlayer);
        Debug.Log(hit.collider);
        if(hit != null) 
            return hit.collider.tag == "Player";
        else
        {
            return false;
        }

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
