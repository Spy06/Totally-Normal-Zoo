using TMPro;
using UnityEngine;

public class AutoSizePrefferedText : MonoBehaviour
{
    [SerializeField] private TMP_Text prefferedText;
    [SerializeField] private RectTransform autoSizedRectTf;

    [Space(10)]
    [SerializeField] private AutoSizedRect chooseAutoSized;
    [SerializeField] private float widthOffset;
    [SerializeField] private float heightOffset;

    public void ChangeText(string newText)
    {
        Vector2 newSize = autoSizedRectTf.sizeDelta;
        prefferedText.text = newText;

        switch (chooseAutoSized)
        {
            case AutoSizedRect.width:
                newSize.x = prefferedText.preferredWidth + widthOffset;
                break;
            case AutoSizedRect.height:
                newSize.y = prefferedText.preferredHeight + heightOffset;
                break;
            case AutoSizedRect.both:
                newSize.x = prefferedText.preferredWidth + widthOffset;
                newSize.y = prefferedText.preferredHeight + heightOffset;
                break;
        }

        autoSizedRectTf.sizeDelta = newSize;
    }
}