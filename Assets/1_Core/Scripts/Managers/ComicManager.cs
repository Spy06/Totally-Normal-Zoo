using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ComicManager : MonoBehaviour
{
    [SerializeField] private GameObject panelComicNTutor;
    [SerializeField] private Image comicNTutorImage;
    [SerializeField] private TMP_Text judulPanel;

    [Space(10)]
    [SerializeField] private GameObject nextButtonObj;
    [SerializeField] private GameObject prevButtonObj;
    [SerializeField] private GameObject tutorialButtonObj;

    [Space(10)]
    [SerializeField] private Sprite letsFindOut;
    [SerializeField] private Sprite letsStartGame;

    [Space(10)]
    [SerializeField] private List<Sprite> listSpriteComic = new();
    [SerializeField] private List<Sprite> listSpriteTutor = new();

    private int currentComicIndex;
    private int currentTutorIndex;
    private float leanTransisiDuration = 0.2f;
    private float leanDelayDuration = 0.1f;

    private void Start()
    {
        tutorialButtonObj.SetActive(true);
        // AutoSizePrefferedText tutorButtonText = tutorialButtonObj.GetComponent<AutoSizePrefferedText>();
        // tutorButtonText?.ChangeText($"Lets Find Out!");

        RectTransform rectTutor = tutorialButtonObj.GetComponent<RectTransform>();
        rectTutor.sizeDelta = new Vector2(425, rectTutor.rect.height);

        Image tutorImageButton = tutorialButtonObj.GetComponent<Image>();
        tutorImageButton.sprite = letsFindOut;

        tutorialButtonObj.SetActive(false);

        ShowComicFromBeginning(true);
    }

    private void ShowComicFromBeginning(bool enable)
    {
        currentComicIndex = 0;
        comicNTutorImage.sprite = listSpriteComic[currentComicIndex];

        UnityAction nextButtonAction = () => ShowNextComic(1);
        UnityAction prevButtonAction = () => ShowNextComic(-1);
        UnityAction tutorButtonAction = () => TutorButtonPressed();

        Button prevButton = prevButtonObj.GetComponent<Button>();
        Button nextButton = nextButtonObj.GetComponent<Button>();
        Button tutorialButton = tutorialButtonObj.GetComponent<Button>();

        nextButton.onClick.AddListener(nextButtonAction);
        prevButton.onClick.AddListener(prevButtonAction);
        tutorialButton.onClick.AddListener(tutorButtonAction);

        nextButtonObj.SetActive(true);
        prevButtonObj.SetActive(false);
        
        ShowComic(enable);
    }

    private void ShowComic(bool enable)
    {
        CoreManager.Instance.BlockTouchInput(true);

        LeanTween.cancel(comicNTutorImage.gameObject);
        LeanTween.cancel(nextButtonObj);
        
        if (enable)
        {
            panelComicNTutor.SetActive(true);
            Helper.LeanScaleOutBack(panelComicNTutor, Vector3.one, leanTransisiDuration, () =>
            {
                float transisiDurasiNext = nextButtonObj.activeInHierarchy ? leanTransisiDuration : 0;
                float delayDurasiNext = nextButtonObj.activeInHierarchy ? leanDelayDuration : 0;

                Helper.LeanScaleOutBack(nextButtonObj, Vector3.one, transisiDurasiNext, delayDurasiNext, () =>
                {
                    Helper.LeanScaleOutBack(comicNTutorImage.gameObject, Vector3.one, leanTransisiDuration, leanDelayDuration, () =>
                    {
                        float transisiDurasiPrev = prevButtonObj.activeInHierarchy ? leanTransisiDuration : 0;
                        float delayDurasiPrev = prevButtonObj.activeInHierarchy ? leanDelayDuration : 0;

                        Helper.LeanScaleOutBack(prevButtonObj, Vector3.one, transisiDurasiPrev, delayDurasiPrev);
                    });
                });
            });
        }
        else
        {
            float transisiDurasiNext = nextButtonObj.activeInHierarchy ? leanTransisiDuration : 0;

            Helper.LeanScaleInBack(nextButtonObj, Vector3.zero, transisiDurasiNext, () =>
            {
                Helper.LeanScaleInBack(comicNTutorImage.gameObject, Vector3.zero, leanTransisiDuration, leanDelayDuration, () =>
                {
                    float transisiDurasiPrev = prevButtonObj.activeInHierarchy ? leanTransisiDuration : 0;
                    float delayDurasiPrev = prevButtonObj.activeInHierarchy ? leanDelayDuration : 0;

                    Helper.LeanScaleInBack(prevButtonObj, Vector3.zero, transisiDurasiPrev, delayDurasiPrev, () =>
                    {
                        Helper.LeanScaleInBack(panelComicNTutor, Vector3.zero, leanTransisiDuration, leanDelayDuration, () =>
                        {
                            panelComicNTutor.SetActive(false);
                        });
                    });
                });
            });
        }

        CoreManager.Instance.BlockTouchInput(false);
    }

    private void ShowNextComic(int addIndex)
    {
        currentComicIndex += addIndex;
        currentComicIndex = Mathf.Clamp(currentComicIndex, 0, listSpriteComic.Count - 1);
        comicNTutorImage.sprite = listSpriteComic[currentComicIndex];

        prevButtonObj.SetActive(currentComicIndex > 0);
        nextButtonObj.SetActive(currentComicIndex < listSpriteComic.Count - 1);
        tutorialButtonObj.SetActive(currentComicIndex >= listSpriteComic.Count -1);

        AudioManager.instance.PlaySFX("nextComic", true);
    }

    private void TutorButtonPressed()
    {
        CoreManager.Instance.BlockTouchInput(true);

        Helper.LeanScaleInBack(nextButtonObj, Vector3.zero, 0, () =>
        {
            nextButtonObj.SetActive(true);
            Helper.LeanScaleInBack(comicNTutorImage.gameObject, Vector3.zero, leanTransisiDuration, leanDelayDuration);
            Helper.LeanScaleInBack(tutorialButtonObj.gameObject, Vector3.zero, leanTransisiDuration, leanDelayDuration);
            Helper.LeanScaleInBack(judulPanel.gameObject, Vector3.zero, leanTransisiDuration, leanDelayDuration, () =>
            {
                Helper.LeanScaleInBack(prevButtonObj, Vector3.zero, leanTransisiDuration, leanDelayDuration, () =>
                {
                    Helper.LeanScaleOutBack(nextButtonObj, Vector3.one, leanTransisiDuration, leanDelayDuration, () =>
                    {
                        judulPanel.text = $"How to Play";
                        comicNTutorImage.sprite = listSpriteTutor[currentTutorIndex];
                        prevButtonObj.SetActive(false);
                        tutorialButtonObj.SetActive(false);

                        // AutoSizePrefferedText tutorButtonText = tutorialButtonObj.GetComponent<AutoSizePrefferedText>();
                        // tutorButtonText?.ChangeText($"Lets Start The Game!");

                        RectTransform rectTutor = tutorialButtonObj.GetComponent<RectTransform>();
                        rectTutor.sizeDelta = new Vector2(300, rectTutor.rect.height);

                        Image tutorImageButton = tutorialButtonObj.GetComponent<Image>();
                        tutorImageButton.sprite = letsStartGame;

                        Helper.LeanScaleOutBack(tutorialButtonObj, Vector3.one, 0);
                        Helper.LeanScaleOutBack(comicNTutorImage.gameObject, Vector3.one, leanTransisiDuration, leanDelayDuration);
                        Helper.LeanScaleOutBack(prevButtonObj, Vector3.one, leanTransisiDuration, leanDelayDuration);
                        Helper.LeanScaleOutBack(judulPanel.gameObject, Vector3.one, leanTransisiDuration, leanDelayDuration, () =>
                        {
                            UnityAction prevButtonAction = () => ShowNextTutor(-1);
                            UnityAction nextButtonAction = () => ShowNextTutor(1);

                            Button prevButton = prevButtonObj.GetComponent<Button>();
                            Button nextButton = nextButtonObj.GetComponent<Button>();
                            Button tutorialButton = tutorialButtonObj.GetComponent<Button>();

                            prevButton.onClick.RemoveAllListeners();
                            nextButton.onClick.RemoveAllListeners();    
                            tutorialButton.onClick.RemoveAllListeners();

                            prevButton.onClick.AddListener(prevButtonAction);
                            nextButton.onClick.AddListener(nextButtonAction);
                            tutorialButton.onClick.AddListener(StartGamePressed);

                            CoreManager.Instance.BlockTouchInput(false);
                        });
                    });
                });
            });
        });
    }

    private void ShowNextTutor(int addIndex)
    {
        currentTutorIndex += addIndex;
        currentTutorIndex = Mathf.Clamp(currentTutorIndex, 0, listSpriteTutor.Count - 1);
        comicNTutorImage.sprite = listSpriteTutor[currentTutorIndex];

        prevButtonObj.SetActive(currentTutorIndex > 0);
        nextButtonObj.SetActive(currentTutorIndex < listSpriteTutor.Count - 1);
        tutorialButtonObj.SetActive(currentTutorIndex >= listSpriteTutor.Count - 1);

        AudioManager.instance.PlaySFX("nextComic", true);
    }

    private void StartGamePressed()
    {
        ShowComic(false);
        AudioManager.instance.PlayBGM("BGM 1");
        GameEventManager.Instance.GameStarted();
    }
}