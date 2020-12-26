using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    private PlayerController playerController;
    private readonly string  COLORQUEUETAG = "Color_Queue"; 

    [SerializeField] private ColorQueue colorQueue;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public ColorQueue ColorQueue { get => colorQueue; set => colorQueue = value; }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        colorQueue = GameObject.FindGameObjectWithTag(COLORQUEUETAG).GetComponent<ColorQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (colorQueue.ColorSlots.Length > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), owner.GetComponent<BoxCollider2D>());
            bullet.GetComponent<ColorProperties>().UpdateColor(colorQueue.ColorSlots[0].currentColor);
            colorQueue.UseColor();
            

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();


            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }
    }

    public void SelfApply()
    {
        if (colorQueue.ColorSlots.Length > 0)
        {
            playerController.ColorProperties.UpdateColor(colorQueue.ColorSlots[0].currentColor);
            colorQueue.UseColor();



        }
    }
}
