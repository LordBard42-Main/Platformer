using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region  Singleton

    public static GameManager instance;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("GameManager already exists");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

    }
    #endregion

   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
