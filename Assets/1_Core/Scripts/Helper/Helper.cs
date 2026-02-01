using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UIOutline = UnityEngine.UI.Outline;

public static class Helper
{
    #region Raycast
    public static Ray RayFromCamera(Camera cam) => cam.ViewportPointToRay(new(0.5f, 0.5f, 0));
    public static bool PerformRaycast(Camera cam, out RaycastHit hit, float distance) => Physics.Raycast(RayFromCamera(cam), out hit, distance);
    public static bool PerformRaycast(Camera cam, out RaycastHit hit, float distance, LayerMask layerMask) => Physics.Raycast(RayFromCamera(cam), out hit, distance, layerMask);
    #endregion

    #region Animate Lean Tween
    #region Animate Alpha
    public static void AnimateAlpha(Image image, float from, float to, float animDuration = 0.25f)
    {
        image.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateAlpha(Image image, float from, float to, Action onComplete, float animDuration = 0.25f)
    {
        image.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }
    public static void AnimateAlpha(Image image, float from, float to, float delay, float animDuration = 0.25f)
    {
        image.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }).setIgnoreTimeScale(true).delay = delay;
    }
    public static void AnimateAlpha(TextMeshProUGUI text, float from, float to, float animDuration = 0.25f)
    {
        text.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateAlpha(TextMeshProUGUI text, float from, float to, Action onComplete, float animDuration = 0.25f)
    {
        text.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }
    #endregion

    #region Animate Alpha and Scale
    public static void AnimateAlphaAndScale(Image image, float from, float to, GameObject target, Vector3 scaleTo, Action onComplete, float animDuration = 0.25f)
    {
        AnimateAlpha(image, from, to, animDuration);
        LeanTween.scale(target, scaleTo, animDuration).setEaseOutCubic().setIgnoreTimeScale(true).setOnComplete(() => onComplete?.Invoke());
    }
    public static void AnimateAlphaAndScale(Image image, float from, float to, GameObject target, Vector3 scaleTo, float animDuration = 0.25f)
    {
        AnimateAlpha(image, from, to);
        LeanTween.scale(target, scaleTo, animDuration).setEaseInOutCubic().setIgnoreTimeScale(true);
    }
    public static void AnimateAlphaAndScale(TextMeshProUGUI text, float from, float to, GameObject target, Vector3 scale, float animDuration = 0.25f)
    {
        AnimateAlpha(text, from, to);
        LeanTween.scale(target, scale, animDuration).setEaseInOutCubic().setIgnoreTimeScale(true);
    }
    #endregion

    #region Animate Canvas Group
    public static void AnimateCanvasGroup(CanvasGroup canvas, float from, float to, float animDuration = 0.25f)
    {
        canvas.alpha = from;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            canvas.alpha = alpha;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateCanvasGroup(CanvasGroup canvas, float from, float to, Action onComplete, float animDuration = 0.25f)
    {
        canvas.alpha = from;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            canvas.alpha = alpha;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }
    #endregion

    #region Animasi LeanTween Old
    public static void MoveLocalX(Transform transform, float to, float durasi, Action onComplete = null)
    {
        transform.LeanMoveLocalX(to, durasi).setEaseOutBack().setOnComplete(onComplete);
    }

    public static void MoveLocalX(Transform transform, float to, float durasi, bool isOut)
    {
        if (isOut) transform.LeanMoveLocalX(to, durasi).setEaseOutBack();
        else transform.LeanMoveLocalX(to, durasi).setEaseInBack();
    }

    public static void MoveLocalX(Transform transform, float to, float durasi, float delay)
    {
        transform.LeanMoveLocalX(to, durasi).setEaseOutBack().delay = delay;
    }

    public static void LeanScale(Transform transform, Vector3 to, float durasi, float delay)
    {
        transform.LeanScale(to, durasi).setEaseInOutExpo().delay = delay;
    }

    public static void LeanScale(Transform transform, Vector3 to, float durasi, Action onComplete = null)
    {
        transform.LeanScale(to, durasi).setEaseInOutExpo().setOnComplete(onComplete);
    }

    public static void MoveLocalY(Transform transform, float to, float durasi, Action onComplete = null)
    {
        transform.LeanMoveLocalY(to, durasi).setEaseInOutExpo().setOnComplete(onComplete);
    }

    public static void MoveLocalY(Transform transform, float to, float durasi, float delay)
    {
        transform.LeanMoveLocalY(to, durasi).setEaseInOutExpo().delay = delay;
    }
    #endregion

    #region Animasi LeanScale
    public static void LeanScaleOutBack(GameObject obj, Vector3 to, float transisiDurasi, Action onComplete = null)
    {
        obj.LeanScale(to, transisiDurasi).setEaseOutBack().setIgnoreTimeScale(true).setOnComplete(onComplete);
    }

    public static void LeanScaleInBack(GameObject obj, Vector3 to, float transisiDurasi, Action onComplete = null)
    {
        obj.LeanScale(to, transisiDurasi).setEaseInBack().setIgnoreTimeScale(true).setOnComplete(onComplete);
    }

    public static void LeanScaleOutBack(GameObject obj, Vector3 to, float transisiDurasi, float delay, Action onComplete = null)
    {
        obj.LeanScale(to, transisiDurasi).setEaseOutBack().setIgnoreTimeScale(true).setOnComplete(onComplete).delay = delay;
    }

    public static void LeanScaleInBack(GameObject obj, Vector3 to, float transisiDurasi, float delay, Action onComplete = null)
    {
        obj.LeanScale(to, transisiDurasi).setEaseInBack().setIgnoreTimeScale(true).setOnComplete(onComplete).delay = delay;
    }
    #endregion
    #endregion

    #region Set Button
    public static void SetButton(Button button, bool enable)
    {
        TMP_Text textnya = button.GetComponentInChildren<TMP_Text>(true);
        Image imgnya = button.GetComponent<Image>();

        bool adaText = textnya != null;
        if (adaText && textnya.enabled != enable) textnya.enabled = enable;
        if (imgnya.enabled != enable) imgnya.enabled = enable;
    }
    public static void SetButton(Button button, bool enable, string nama)
    {
        TMP_Text textnya = button.GetComponentInChildren<TMP_Text>(true);

        textnya.text = nama;
        bool adaText = textnya != null;
        if (adaText && textnya.enabled != enable) textnya.enabled = enable;
        if (button.enabled != enable) button.GetComponent<Image>().enabled = enable;
    }
    #endregion

    public static void EnableAllColliders(GameObject gObj, bool enable)
    {
        Collider[] colliders = gObj.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = enable;
        }
    }

    public static void ChangeLayer(GameObject obj, int layerMask)
    {
        obj.layer = layerMask;
        Renderer[] meshRend = obj.GetComponentsInChildren<Renderer>(true);

        foreach (Renderer mr in meshRend)
        {
            if (mr is not ParticleSystemRenderer) mr.gameObject.layer = layerMask;
        }
    }
}

public enum AxisVector3
{
    x,
    y,
    z
}

public enum AutoSizedRect
{
    width,
    height,
    both,
}

public enum JenisHewan
{
    Lion,
    Panda,
    Gorilla,
    Hippopotamus,
    Llama,
}

public enum JenisTools
{
    Duit,
    Kopi,
}

[Serializable]
public class ItemTools
{
    public bool readyToUse;
    public bool onCooldown;

    [Space(10)]
    public JenisTools jenisTools;
    public UIOutline outline;
    public Image iconImage;
    public Image fillAmount;
}

[Serializable]
public class CameraKandang
{
    public JenisHewan jenisHewan;
    public GameObject cameranya;
}