using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{
    [SerializeField] private Image parentPanel;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private Image imagePanel;
    [SerializeField] private bool freezeTime;

    [SerializeField] private float animDuration = 0.25f;
    [SerializeField] private float fadeValue = 0.95f;

    public void SetPanel(bool status)
    {
        if (status) OpenPanel();
        else ClosePanel();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        contentPanel.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(parentPanel, 0, fadeValue, animDuration);
        Helper.AnimateAlphaAndScale(imagePanel, 0, 1f, contentPanel, Vector3.one, () =>
        {
            if (freezeTime) Time.timeScale = 0;
        }, animDuration);
    }

    public void ClosePanel()
    {
        Helper.AnimateAlpha(parentPanel, fadeValue, 0, animDuration);
        Helper.AnimateAlphaAndScale(imagePanel, 1f, 0, contentPanel, Vector3.zero, () =>
        {
            if (freezeTime) Time.timeScale = 1;
            gameObject.SetActive(false);
        }, animDuration);
    }

    public void OpenPanel(Action onComplete = null)
    {
        gameObject.SetActive(true);
        contentPanel.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(parentPanel, 0, fadeValue, animDuration);
        Helper.AnimateAlphaAndScale(imagePanel, 0, 1f, contentPanel, Vector3.one, () =>
        {
            onComplete?.Invoke();
            if (freezeTime) Time.timeScale = 0;
        }, animDuration);
    }

    public void ClosePanel(Action onComplete = null)
    {
        Helper.AnimateAlpha(parentPanel, fadeValue, 0, animDuration);
        Helper.AnimateAlphaAndScale(imagePanel, 1f, 0, contentPanel, Vector3.zero, () =>
        {
            if (freezeTime) Time.timeScale = 1;
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }, animDuration);
    }
}