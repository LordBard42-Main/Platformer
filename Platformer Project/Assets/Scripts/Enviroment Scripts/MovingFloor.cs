using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private bool isMoving;
    
    [SerializeField] [HideInInspector] private PlatformNodes[] nodes; //I need to serialize this to make it visuble in the custom inspector
    

    private float moveTimeTotal;
    private float moveTimeCurrent;

    [SerializeField] private Stack<Vector2> toPath;
    [SerializeField] private Stack<Vector2> newPath;

    [SerializeField] private bool isForward;
    private Vector2 currentPosition;
    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        toPath = new Stack<Vector2>();
        newPath = new Stack<Vector2>();

        for (int i = nodes.Length - 1; i >= 0; i--)
        {
            Debug.Log(i);
            toPath.Push(nodes[i].location);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            TraversePathWithScaledTime();
        }

    }

    /// <summary>
    /// Move the platform between nodes at a smooth and fixed rate
    /// </summary>
    private void TraversePathWithScaledTime()
    {
        if (moveTimeCurrent < moveTimeTotal)
        {
            moveTimeCurrent += Time.deltaTime;
            if (moveTimeCurrent > moveTimeTotal)
                moveTimeCurrent = moveTimeTotal;
            Debug.Log(currentPosition);
            if (isForward)
            {
                transform.position = Vector2.Lerp(currentPosition, toPath.Peek(), moveTimeCurrent / moveTimeTotal);
            }
            else
            {
                transform.position = Vector2.Lerp(currentPosition, newPath.Peek(), moveTimeCurrent / moveTimeTotal);
            }
        }
        else
        {
            if (isForward)
            {
                currentPosition = toPath.Pop();
                newPath.Push(currentPosition);

                if (toPath.Count == 0)
                {
                    isForward = false;
                }
                else
                {
                    moveTimeCurrent = 0;
                    moveTimeTotal = (currentPosition - toPath.Peek()).magnitude / speed;
                }
            }
            else
            {
                currentPosition = newPath.Pop();
                toPath.Push(currentPosition);

                if (newPath.Count == 0)
                {
                    isForward = true;
                }
                else
                {
                    moveTimeCurrent = 0;
                    moveTimeTotal = (currentPosition - newPath.Peek()).magnitude / speed;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.transform.parent.parent = transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent.parent = null;
        }
    }

    /// <summary>
    /// This draws gizmos at the location of path nodes
    /// </summary>
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < nodes.Length; i++)
        {

            Handles.Label(nodes[i].location, i + "");
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(nodes[i].location, .2f);
            
        }
    }
    
}
