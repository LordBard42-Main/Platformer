using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class AmmoSlot : MonoBehaviour
{
    [SerializeField] private PrimaryColors slotColor;
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private Text ammoCountText;
    [SerializeField] private Image ammoSlotSelectImage;
    [SerializeField] private Image ammoSlotMask;

    [Header("Cooldown")]
    [SerializeField] private AnimationCurve cooldownCurve;

    public PrimaryColors SlotColor { get => slotColor; set => slotColor = value; }
    public Text AmmoCountText { get => ammoCountText; set => ammoCountText = value; }
    public Image AmmoSlotSelectImage { get => ammoSlotSelectImage; set => ammoSlotSelectImage = value; }
    public int AmmoCount { get => ammoCount; set => ammoCount = value; }

    /// <summary>
    /// Sets Ammo On Cooldown
    /// </summary>
    public void SetAmmoOnCooldown()
    {
        ammoSlotMask.fillAmount = 1;
        StartCoroutine(CoolDown());
    }

    /// <summary>
    /// Externally sets sthe count for ammo and updates 
    /// Game Text
    /// </summary>
    /// <param name="value"></param>
    public void SetAmmoCount(int value)
    {
        ammoCount = value;
        ammoCountText.text = value + "";
    }

    /// <summary>
    /// After scene is loaded, fade in from black
    /// </summary>
    /// <returns></returns>
    IEnumerator CoolDown()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime * .1f;
            Debug.Log(t);
            float a = cooldownCurve.Evaluate(t);
            ammoSlotMask.fillAmount = a;
            yield return 0;
        }

        SetAmmoCount(10);
        

    }

}
