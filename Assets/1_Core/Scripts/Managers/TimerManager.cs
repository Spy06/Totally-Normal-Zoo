using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private int gameDuration = 180;
    [SerializeField] private RectTransform bgTimer;
    [SerializeField] private Image imageFillAmount;
    [SerializeField] private TMP_Text timerText;

    private int timerLeft;
    public static int gameTick;
    private float originalPosXValue;

    private void OnEnable()
    {
        GameEventManager.Instance.OnGameStarted += StartGame;
        GameEventManager.Instance.OnSelectedKandang += MoveTimerToCenter;
        GameEventManager.Instance.OnBackToPos += MoveTimerToOriginalPos;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnGameStarted -= StartGame;
        GameEventManager.Instance.OnSelectedKandang -= MoveTimerToCenter;
        GameEventManager.Instance.OnBackToPos -= MoveTimerToOriginalPos;
    }

    private void Awake()
    {
        originalPosXValue = bgTimer.anchoredPosition.x;
    }

    private void MoveTimerToCenter(JenisHewan jenis)
    {
        LeanTween.value(bgTimer.gameObject, bgTimer.anchoredPosition.x, 0f, 0.35f).setOnUpdate((float x) =>
        {
            bgTimer.anchoredPosition = new Vector2(x, bgTimer.anchoredPosition.y);
        }).setEaseOutBack().setIgnoreTimeScale(true).delay = 0.25f;
    }

    private void MoveTimerToOriginalPos()
    {
        LeanTween.value(bgTimer.gameObject, bgTimer.anchoredPosition.x, originalPosXValue, 0.35f).setOnUpdate((float x) =>
        {
            bgTimer.anchoredPosition = new Vector2(x, bgTimer.anchoredPosition.y);
        }).setEaseOutBack().setIgnoreTimeScale(true);
    }

    private void StartGame()
    {
        timerLeft = gameDuration;
        UpdateTimerText();
        imageFillAmount.fillAmount = 1;
        gameTick = 0;

        StartCoroutine(nameof(StartGameTimer));
    }

    private IEnumerator StartGameTimer()
    {
        while (timerLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timerLeft--;
            gameTick++;

            UpdateTimerText();
            imageFillAmount.fillAmount = (float)timerLeft / gameDuration;
        }

        CoreManager.Instance.ShowNotif("Kamu Kalah, Waktumu Sudah Habis!");
        GameManager.Instance.GameOver (true);
    }

    private void UpdateTimerText()
    {
        TimeSpan time = TimeSpan.FromSeconds(timerLeft);
        timerText.text = $"{time.Minutes:00}:{time.Seconds:00}";
    }
}