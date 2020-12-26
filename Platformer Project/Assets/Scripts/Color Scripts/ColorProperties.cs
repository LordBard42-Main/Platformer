using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PrimaryColors { Blue = 1, Yellow = 2, Red = 3}
public enum SecondaryColors { Green, Orange, Purple}

[ExecuteAlways]
public class ColorProperties : MonoBehaviour
{

   

    private new SpriteRenderer renderer;

    [SerializeField] private bool setColor;
    [SerializeField] private PrimaryColors currentColor;

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
