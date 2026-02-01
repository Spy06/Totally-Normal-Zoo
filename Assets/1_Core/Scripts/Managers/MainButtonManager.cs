using UnityEngine;
using UnityEngine.UI;

public class MainButtonManager : MonoBehaviour
{
    [SerializeField] private Image panelCredit;
    [SerializeField] private GameObject contentCredit;

    private float animDuration = 0.25f;

    public void ShowPanel()
    {
        panelCredit.gameObject.SetActive(true);
        contentCredit.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(panelCredit, 0, 1);
        Helper.LeanScaleOutBack(contentCredit, Vector3.one, animDuration);
    }

    public void HidePanel()
    {
        Helper.AnimateAlpha(panelCredit, 1, 0);
        Helper.LeanScaleInBack(contentCredit, Vector3.zero, animDuration, () =>
        {
            panelCredit.gameObject.SetActive(false);
        });
    }

    public void ChangeScene(string sceneName) => ASyncSceneLoader.Instance.ChangeScene(sceneName);
    public void ExitGame() => Application.Quit();
}