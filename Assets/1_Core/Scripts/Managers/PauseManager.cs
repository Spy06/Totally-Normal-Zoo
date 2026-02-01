using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Image panelPause;
    [SerializeField] private GameObject contentPause;

    private float animDuration = 0.25f;

    public void ShowPanel()
    {
        panelPause.gameObject.SetActive(true);
        contentPause.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(panelPause, 0, 1);
        Helper.LeanScaleOutBack(contentPause, Vector3.one, animDuration);
    }

    public void HidePanel()
    {
        Helper.AnimateAlpha(panelPause, 1, 0);
        Helper.LeanScaleInBack(contentPause, Vector3.zero, animDuration, () =>
        {
            panelPause.gameObject.SetActive(false);
        });
    }

    public void ChangeScene(string sceneName) => ASyncSceneLoader.Instance.ChangeScene(sceneName);
}