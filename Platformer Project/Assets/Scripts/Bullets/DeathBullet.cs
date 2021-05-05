using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision.collider.TryGetComponent(out ColorProperties colorProperties))
            {
                collision.rigidbody.AddForce(GetComponent<Rigidbody2D>().velocity * 40);
            }
        }

        Destroy(gameObject);
    }
}
