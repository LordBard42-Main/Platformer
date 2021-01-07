using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGun : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [ReadOnly][SerializeField] private PrimaryColors color;

    [Header("Bullet Components")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        color = owner.GetComponent<ColorProperties>().CurrentColor;
    }
    
    /// <summary>
    /// Fires Bullet, will take from the currently selected ammo reserve
    /// </summary>
    public void Fire()
    {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), owner.GetComponent<BoxCollider2D>());
            bullet.GetComponent<ColorProperties>().UpdateColor(color);

        bullet.transform.localScale = transform.localScale / 1.5f;
            

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();


            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        
        
    }

}
