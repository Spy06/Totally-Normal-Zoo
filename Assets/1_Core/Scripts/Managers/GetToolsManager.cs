using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetToolsManager : MonoBehaviour
{
    [SerializeField] private float coolDownToolDuration = 5;
    [SerializeField] private List<ItemTools> listItemTools = new();

    private void OnEnable()
    {
        GameEventManager.Instance.OnGettingTools += GettingTools;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnGettingTools -= GettingTools;
    }

    private void Update()
    {
        bool pressed = Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        if (!pressed) return;

        Vector2 screenPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            UtilityTools tools = hit.collider.GetComponent<UtilityTools>();

            if (tools != null)
            {
                tools.ToolsTaken();
            }
        }
    }

    private void GettingTools(JenisTools tools)
    {
        ItemTools selectedTools = listItemTools.Find(x => x.jenisTools == tools);
        selectedTools.outline.gameObject.SetActive(true);

        if (!selectedTools.onCooldown)
        {
            StartCoroutine(StartCoolDown(tools));
        }
    }

    private void UsingTools(JenisTools tools)
    {
        ItemTools selectedTools = listItemTools.Find(x => x.jenisTools == tools);
        if (!selectedTools.readyToUse)
        {
            CoreManager.Instance.ShowNotif("Tools is still on cooldown!");
            return;
        }

        if (tools == JenisTools.Duit)
        {
            CoreManager.Instance.ShowNotif("Duit Used!", 2);
            GameEventManager.Instance.SpawnCoin();
        }
        else if (tools == JenisTools.Kopi)
        {
            CoreManager.Instance.ShowNotif("Kopi Used!", 2);
        }

        if (!selectedTools.onCooldown)
        {
            StartCoroutine(StartCoolDown(tools));
        }
    }

    public void UsingToolsDuit() => UsingTools(JenisTools.Duit);
    public void UsingToolsKopi() => UsingTools(JenisTools.Kopi);

    private IEnumerator StartCoolDown(JenisTools jenisnya)
    {
        ItemTools tools = listItemTools.Find(x => x.jenisTools == jenisnya);

        tools.readyToUse = false;
        tools.onCooldown = true;
        tools.fillAmount.fillAmount = 1;
        tools.fillAmount.gameObject.SetActive(true);
        tools.outline.effectColor = Color.white;

        float timer = coolDownToolDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float percent = Mathf.Clamp01(timer / coolDownToolDuration);
            tools.fillAmount.fillAmount = percent;
            yield return null;
        }

        tools.fillAmount.fillAmount = 0;
        tools.fillAmount.gameObject.SetActive(false);
        tools.outline.effectColor = Color.green;
        tools.readyToUse = true;
        tools.onCooldown = false;
    }
}