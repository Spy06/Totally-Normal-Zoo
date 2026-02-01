using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnToolsDuitManager : MonoBehaviour
{
    [SerializeField] private float spawnDuitInterval = 30;
    [SerializeField] private Transform listRandomDuitPos;
    
    private List<GameObject> listDuit = new();
    private GameObject spawnedDuit;
    private bool duitDiambil;

    private void OnEnable()
    {
        GameEventManager.Instance.OnGettingTools += CheckIfDuitTaken;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnGettingTools -= CheckIfDuitTaken;
    }

    private void Awake()
    {
        foreach(Transform t in listRandomDuitPos)
        {
            listDuit.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        StartCoroutine(nameof(SpawnDuitUntilTaken));
    }

    private void CheckIfDuitTaken(JenisTools jenisnya)
    {
        if (jenisnya == JenisTools.Duit) duitDiambil = true;
    }

    private IEnumerator SpawnDuitUntilTaken()
    {
        while (!duitDiambil)
        {
            yield return null;

            float timer = 0;
            while (timer < spawnDuitInterval)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            if (!duitDiambil) SpawnDuit();
        }
    }

    private void SpawnDuit()
    {
        if (spawnedDuit != null) spawnedDuit.SetActive(false);

        GameObject selectedDuit = listDuit[Random.Range(0, listDuit.Count)];
        spawnedDuit = selectedDuit;
        spawnedDuit.SetActive(true);
    }
}