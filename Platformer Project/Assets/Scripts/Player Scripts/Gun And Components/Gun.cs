using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    private PlayerController playerController;

    [Header("Bullet Components")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;

    [Header("Ammo")]
    [SerializeField] private GameObject ammoSlotParent;
    [SerializeField] private AmmoSlot[] ammoSlots;
    [SerializeField] private int currentSlot = 0;

    public int CurrentSlot { get => currentSlot; set => currentSlot = value; }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        ammoSlots = ammoSlotParent.GetComponentsInChildren<AmmoSlot>();

    }
    
    /// <summary>
    /// Fires Bullet, will take from the currently selected ammo reserve
    /// </summary>
    public void Fire()
    {
        if (ammoSlots[currentSlot].AmmoCount > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), owner.GetComponent<BoxCollider2D>());
            bullet.GetComponent<ColorProperties>().UpdateColor(ammoSlots[currentSlot].SlotColor);

            ammoSlots[currentSlot].SetAmmoCount(ammoSlots[currentSlot].AmmoCount - 1);
            

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();


            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

            if(ammoSlots[currentSlot].AmmoCount <= 0)
            {
                ammoSlots[currentSlot].SetAmmoOnCooldown();
                Debug.Log("Ammo Less than 1");
            }
        }
    }

    public void SelfApply()
    {
        if (ammoSlots[currentSlot].AmmoCount > 0)
        {
            playerController.ColorProperties.UpdateColor(ammoSlots[currentSlot].SlotColor);
              ammoSlots[currentSlot].SetAmmoCount(ammoSlots[currentSlot].AmmoCount - 1);



        }
    }

    public void SelectAmmoType(int previousValue, int currentValue)
    {
        ammoSlots[previousValue].AmmoSlotSelectImage.color = Color.gray;
        ammoSlots[currentValue].AmmoSlotSelectImage.color = Color.white;

        currentSlot = currentValue;
    }
}
