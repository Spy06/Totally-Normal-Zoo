using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ASyncSceneLoader : MonoBehaviour
{
    public static ASyncSceneLoader Instance;
    public static event Action OnSceneFinishLoaded;

    [Header("References")]
    [SerializeField] private PanelAnimation panelLoading;
    [SerializeField] private Image imageBackground;
    [SerializeField] private Image imageFillAmount;
    [SerializeField] private Transform spinnerLoading;

    [Header("Settings")]
    [SerializeField] private float durasiFakeLoading = 1;
    [SerializeField] private Sprite[] loadingSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(nameof(RotateSpinnerLoading));
        imageBackground.sprite = loadingSprite.Length > 0 ? loadingSprite[Random.Range(0, loadingSprite.Length)] : null;

        AsyncOperation loadOperation = null;
        bool isLoading = false;
        panelLoading.OpenPanel(() =>
        {
            loadOperation = SceneManager.LoadSceneAsync(sceneName);
            loadOperation.allowSceneActivation = false;
            isLoading = true;
        });

        yield return new WaitWhile(() => !isLoading);
        yield return new WaitWhile(() => loadOperation.progress < 0.9f);

        float timer = 0;
        while (timer < durasiFakeLoading)
        {
            timer += Time.deltaTime;
            float percent = Mathf.Clamp01(timer / durasiFakeLoading);
            imageFillAmount.fillAmount = percent;
            yield return null;
        }

        loadOperation.allowSceneActivation = true;

        yield return new WaitForSeconds(1);
        panelLoading.ClosePanel();
        OnSceneFinishLoaded?.Invoke();
        GameEventManager.Instance.FinishChangingScene();
        StopCoroutine(nameof(RotateSpinnerLoading));
    }

    private IEnumerator RotateSpinnerLoading()
    {
        while (true)
        {
            spinnerLoading.Rotate(0f, 0f, 360 * Time.unscaledDeltaTime);
            yield return null;
        }
    }
}