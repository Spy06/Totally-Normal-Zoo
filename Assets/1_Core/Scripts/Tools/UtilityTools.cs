using UnityEngine;

public class UtilityTools : MonoBehaviour
{
    [SerializeField] private Transform shiningFX;
    [SerializeField] private JenisTools jenisTools;

    private void Start()
    {
        LeanTween.rotateAroundLocal(shiningFX.gameObject, Vector3.forward, 360f, 7.5f).setRepeat(-1).setEaseLinear();
        LeanTween.alpha(shiningFX.gameObject, 0.25f, 0.75f).setLoopPingPong().setEaseLinear();
    }

    public void ToolsTaken()
    {
        GameEventManager.Instance.GettingTools(jenisTools);
        LeanTween.cancel(shiningFX.gameObject);
        Destroy(gameObject);
    }
}