using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private float rotationalSpeed;
    [SerializeField] private float amplitude = .5f;
    [SerializeField] private float frequency = 1f;

    private Vector2 posOffset = new Vector2();
    private Vector2 tempPos = new Vector2();

    [SerializeField] private AudioClip goalSound;
    [SerializeField] private Scenes currentScene;
    [SerializeField] private Scenes goToScene;

    public AudioClip GoalSound { get => goalSound; set => goalSound = value; }
    public Scenes GoToScene { get => goToScene; set => goToScene = value; }
    public Scenes CurrentScene { get => currentScene; set => currentScene = value; }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        
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

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
        Debug.Log(tempPos);
        transform.position = tempPos;

    }

    public void PlayGoalSound()
    {
    }
}
