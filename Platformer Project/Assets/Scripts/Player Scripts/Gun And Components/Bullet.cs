using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Wall")
        {
            if (collision.collider.TryGetComponent(out ColorProperties colorProperties))
            {
                colorProperties.UpdateColor(GetComponent<ColorProperties>().CurrentColor);
            }
        }

        Destroy(gameObject);
    }
}
