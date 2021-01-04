using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PrimaryColors { Blue = 1, Yellow = 2, Red = 3}
public enum SecondaryColors { Green, Orange, Purple}

[ExecuteAlways]
public class ColorProperties : MonoBehaviour
{

    [Header("Changeable Color Properties")]
    [SerializeField] private bool setColor;
    [SerializeField] private PrimaryColors currentColor;


    private new SpriteRenderer renderer;

    public PrimaryColors CurrentColor { get => currentColor; set => currentColor = value; }


    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        if(setColor)
        {
            renderer.color = ColorDictionaries.primaryColors[currentColor];
            gameObject.layer = (int)currentColor + 7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor && setColor)
        {
            renderer.color = ColorDictionaries.primaryColors[currentColor];
            gameObject.layer = (int)currentColor + 7;
        }
    }

    public void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Updates the objects color and the layer its on
    /// </summary>
    /// <param name="value"></param>
    public void UpdateColor(int value)
    {
        currentColor = (PrimaryColors)value;
        renderer.color = ColorDictionaries.primaryColors[currentColor];
        gameObject.layer = (int)currentColor + 7;
    }

    public void UpdateColor(PrimaryColors color)
    {
        currentColor = color;
        renderer.color = ColorDictionaries.primaryColors[currentColor];
        gameObject.layer = (int)currentColor + 7;
    }
}
