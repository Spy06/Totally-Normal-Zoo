using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Image panelMap;
    [SerializeField] private GameObject contentMap;
    [SerializeField] private List<CameraKandang> listCameraKandang = new();

    private GameObject selectedKandang;
    private float animDuration = 0.25f;

    private void OnEnable()
    {
        GameEventManager.Instance.OnGameStarted += ShowPanel;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnGameStarted -= ShowPanel;
    }

    private void ShowPanel()
    {
        panelMap.gameObject.SetActive(true);
        contentMap.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(panelMap, 0, 1, 0.25f);
        Helper.LeanScaleOutBack(contentMap, Vector3.one, animDuration, 0.25f);
    }

    private void HidePanel(Action onComplete = null)
    {
        Helper.AnimateAlpha(panelMap, 1, 0);
        Helper.LeanScaleInBack(contentMap, Vector3.zero, animDuration, () =>
        {
            panelMap.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    public void BackToPos()
    {
        selectedKandang.SetActive(false);
        ShowPanel();
        GameEventManager.Instance.BackToPos();
    }

    private void SelectKandang(JenisHewan jenisHewan)
    {
        HidePanel(() =>
        {
            selectedKandang = listCameraKandang.Find(x => x.jenisHewan == jenisHewan).cameranya;
            selectedKandang.SetActive(true);

        });

        GameEventManager.Instance.SelectedKandang(jenisHewan);
    }

    public void SelectKandangSinga() => SelectKandang(JenisHewan.Lion);
    public void SelectKandangPanda() => SelectKandang(JenisHewan.Panda);
    public void SelectKandangGorilla() => SelectKandang(JenisHewan.Gorilla);
    public void SelectKandangKudaNil() => SelectKandang(JenisHewan.Hippopotamus);
    public void SelectKandangLlama() => SelectKandang(JenisHewan.Llama);
}