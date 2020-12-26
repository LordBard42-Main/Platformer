using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Scenes { Main_Menu = 0, Level_1 = 1, Level_2 = 2, Level_3 = 3, Level_4 = 4, Level_5 = 5, Level_6 = 6 }

public class SceneFader : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private AnimationCurve fadeOutCurve;
    [SerializeField] private AnimationCurve fadeInCurve;


    private void Start()
    {
       
    }

    public void FadeTo(Scenes scene)
    {
        StartCoroutine(FadeOut(scene));

    }

    public void Retry()
    {
        string scene = SceneManager.GetActiveScene().name;

        FadeTo((Scenes)System.Enum.Parse(typeof(Scenes), scene));

    }

    public void LevelSelect()
    {
        FadeTo(Scenes.Main_Menu);
    }

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
