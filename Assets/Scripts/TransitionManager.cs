using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public Image solidColorImage;
    public GameObject youDiedPanel;
    AsyncOperation asyncLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeWhite(string sceneName)
    {
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        StartCoroutine(FadeToWhite(1f));
    }

    IEnumerator FadeToWhite(float duration)
    {
        float t = 0;
        Color originalColor = solidColorImage.color;
        Color targetColor = Color.white;
        while (t < duration)
        {
            t += Time.deltaTime;
            solidColorImage.color = Color.Lerp(originalColor, targetColor, t / duration);
            yield return null;
        }
        solidColorImage.color = targetColor;
        asyncLoad.allowSceneActivation = true;
        StartCoroutine(FadeToClear(1f));
    }

    IEnumerator FadeToClear(float duration)
    {
        float t = 0;
        Color originalColor = solidColorImage.color;
        Color targetColor = new Color(1, 1, 1, 0);
        while (t < duration)
        {
            t += Time.deltaTime;
            solidColorImage.color = Color.Lerp(originalColor, targetColor, t / duration);
            yield return null;
        }
        solidColorImage.color = targetColor;
    }


    public void ShowYouDiedPanel()
    {
        youDiedPanel.SetActive(true);
        StartCoroutine(FadeDiedScreen());
    }

    IEnumerator FadeDiedScreen()
    {
        CanvasGroup canvasGroup = youDiedPanel.GetComponent<CanvasGroup>();
        float duration = 1f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }
        yield return new WaitForSeconds(0.75f);
        
    }
}
