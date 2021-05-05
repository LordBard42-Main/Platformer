using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float rotationalSpeed;
    [SerializeField] private float amplitude = .5f;
    [SerializeField] private float frequency = 1f;
    
    [SerializeField] private int direction;

    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();
    

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationalSpeed * Time.deltaTime);
        
    }

}
