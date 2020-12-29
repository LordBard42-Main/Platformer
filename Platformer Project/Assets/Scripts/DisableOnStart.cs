using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class disables a gameobject after it initializes its data
/// </summary>
public class DisableOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
