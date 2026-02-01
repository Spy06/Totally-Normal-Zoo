using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CoreManager : MonoBehaviour
{
    public static CoreManager Instance;

    [Header("Show Notif")]
    [SerializeField] private float widthOffset = 50;
    [SerializeField] private RectTransform popUpNotif;
    [SerializeField] private TMP_Text notifText;

    [Header("Fade Screen")]
    [SerializeField] private Image panelTransisi;

    [Header("Display FPS")]
    [SerializeField] private Image displayFpsBg;
    [SerializeField] private TMP_Text fpsText;

    private float notifAnimDuration = 0.25f;
    private float deltaTime;
    private float fps;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 240;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1 / deltaTime;

        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }

    public void ShowNotif(string message, float showDuration = 1)
    {
        LeanTween.cancel(popUpNotif.gameObject);
        notifText.text = message;
        popUpNotif.sizeDelta = new(notifText.preferredWidth + widthOffset, popUpNotif.rect.height);
        popUpNotif.LeanScale(Vector3.one, notifAnimDuration).setEaseOutBack().setIgnoreTimeScale(true);
        HideNotif(showDuration);
    }

    private void HideNotif(float delay)
    {
        popUpNotif.LeanScale(Vector3.zero, notifAnimDuration).setEaseInBack().setIgnoreTimeScale(true).delay = delay;
    }

    public void FadingKe(float setAlpha, Action onComplete = null, float durasi = 0.5f)
    {
        panelTransisi.raycastTarget = setAlpha != 0;
        LeanTween.value(panelTransisi.gameObject, panelTransisi.color.a, setAlpha, durasi).setOnUpdate(alpha =>
        {
            Color color = panelTransisi.color;
            color.a = alpha;
            panelTransisi.color = color;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }

    public void ShowFPS(bool activate)
    {
        displayFpsBg.enabled = activate;
        fpsText.enabled = activate;
    }

    public void BlockTouchInput(bool enable) => panelTransisi.raycastTarget = enable;
}