using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class holds the button component and the scene that the button targets
/// </summary>
public class LevelButton : MonoBehaviour
{
    
    [SerializeField] private Scenes goTo;

    public Button Button { get; set; }
    public Scenes GoTo { get => goTo; set => goTo = value; }

    // Start is called before the first frame update
    void Awake()
    {
       Button = GetComponent<Button>();
    }
    
}
