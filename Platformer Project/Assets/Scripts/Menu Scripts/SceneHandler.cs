using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//TODO How do we want to handle Scenes?
public enum Scenes { Main_Menu = 0,
                    Level_1_PU = 1, Level_2_PU = 2, Level_3_PU = 3, Level_4_PU = 4, Level_5_PU = 5, Level_6_PU = 6,
                    Level_1_PL = 10001, Level_2_PL = 10002, Level_3_PL = 10003, Level_4_PL = 10004,
}

public class SceneHandler : MonoBehaviour
{
    [Header("Scene Fader Components")]
    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve fadeOutCurve;
    [SerializeField] private AnimationCurve fadeInCurve;

    [Header("Current Scente Information")]
    [ReadOnly] [SerializeField] private Scenes currentScene;

    public Scenes CurrentScene { get => currentScene; private set => currentScene = value; }

    //Initalizes Callback for when a scene is loaded
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
    }

    /// <summary>
    /// When Scene is loaded, set current Scene enum
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = (Scenes)Enum.Parse(typeof(Scenes), SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Fade to the designated Scene
    /// </summary>
    /// <param name="scene"></param>
    public void FadeTo(Scenes scene)
    {
        StartCoroutine(FadeOut(scene));

    }
    
    /// <summary>
    /// After scene is loaded, fade in from black
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime * .75f;
            float a = fadeInCurve.Evaluate(t);
            image.color = new Color(0f,0f,0f,a);
            yield return 0;
        }


    }

    /// <summary>
    /// Before loading scene, fade to black so it isnt jarring
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    IEnumerator FadeOut(Scenes scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeOutCurve.Evaluate(t);
            image.color = new Color(0f,0f,0f,a);
            yield return 0;
        }

        SceneManager.LoadScene(scene.ToString());
        StartCoroutine(FadeIn());
    }

    

}
