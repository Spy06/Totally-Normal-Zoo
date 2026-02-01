using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Image panelDecide;
    [SerializeField] private GameObject contentDecide;
    [SerializeField] private TMP_Text isiTextDecide;

    private float animDuration = 0.25f;

    [Tooltip("Cuma Buat Debugging Aja, Gausah Diisi Apa-Apa")]
    [SerializeField] private int activeKandangEnumIndex = -1;

    private void OnEnable()
    {
        GameEventManager.Instance.OnBackToPos += BackToPos;
        GameEventManager.Instance.OnSelectedKandang += SelectedKandang;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnBackToPos -= BackToPos;
        GameEventManager.Instance.OnSelectedKandang -= SelectedKandang;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowPanel()
    {
        isiTextDecide.text = $"Are you sure <color=#34FF44>{(JenisHewan)activeKandangEnumIndex}</color> is the Human?";

        panelDecide.gameObject.SetActive(true);
        contentDecide.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(panelDecide, 0, 1);
        Helper.LeanScaleOutBack(contentDecide, Vector3.one, animDuration);
    }

    public void HidePanel()
    {
        Helper.AnimateAlpha(panelDecide, 1, 0);
        Helper.LeanScaleInBack(contentDecide, Vector3.zero, animDuration, () =>
        {
            panelDecide.gameObject.SetActive(false);
        });
    }

    public void ChangeScene(string sceneName) => ASyncSceneLoader.Instance.ChangeScene(sceneName);
    private void SelectedKandang(JenisHewan jenisHewan) => activeKandangEnumIndex = (int)jenisHewan;
    private void BackToPos() => activeKandangEnumIndex = -1;

    [Header("GameOver UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image img;
    
    [SerializeField] private Sprite kudaNilAsli, kudaNilPalsu,
                                    gorillaAsli, gorillaPalsu,
                                    singaAsli, singaPalsu, singaMakanSalad,
                                    llamaAsli, llamaPalsu,
                                    pandaAsli, pandaPalsu,
                                    abisWaktu;

    public void GameOver(bool timeout = false)
    {
        gameOverPanel.SetActive (true);

        Helper.LeanScaleOutBack (gameOverPanel, Vector3.one, 0.4f);
        AnimalNPC animal = EnvironmentManager.ActiveEnvironment.npcManager.CurrentAnimal;
        
        if (!timeout)
        {
            if (animal.CompareTag(Selector.instance.impostorAnimalTag)) //Win
            {
                AudioManager.instance.PlaySFX("win");

                switch (animal.name)
                {
                    case "Kuda Nil(Clone)":
                        img.sprite = kudaNilPalsu;
                        break;
                    case "Gorilla(Clone)":
                        img.sprite = gorillaPalsu;
                        break;
                    case "Singa(Clone)":
                        img.sprite = singaPalsu;
                        break;
                    case "Llama(Clone)":
                        img.sprite = llamaPalsu;
                        break;
                    case "Panda(Clone)":
                        img.sprite = pandaPalsu;
                        break;
                }
            }
            else if (animal.CompareTag(Selector.instance.realAnimalTag)) //Lose
            {
                AudioManager.instance.PlaySFX("lose");

                switch (animal.name)
                {
                    case "Kuda Nil(Clone)":
                        img.sprite = kudaNilAsli;
                        break;
                    case "Gorilla(Clone)":
                        img.sprite = gorillaAsli;
                        break;
                    case "Singa(Clone)":
                        img.sprite = UnityEngine.Random.value > 0.5f ? singaAsli : singaMakanSalad;
                        break;
                    case "Llama(Clone)":
                        img.sprite = llamaAsli;
                        break;
                    case "Panda(Clone)":
                        img.sprite = pandaAsli;
                        break;
                }
            }
        } else if (timeout)
        {
            AudioManager.instance.PlaySFX("lose");
            img.sprite = abisWaktu;
        }
    }
}