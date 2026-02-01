using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public static AnimalNPC ActualImpostor;
    private static List<AnimalNPC> allSpawnedNPCs = new List<AnimalNPC>();
    private static int readyManagers = 0;
    private const int totalManagers = 5;

    public AnimalNPC CurrentAnimal;
    [SerializeField] AnimalNPC animalPrefab;

    [Space ()]
    private GameObject placedCoin;

    [SerializeField] int durationUntilPeekaboo = 10, durationUntilHideMoney = 5;

    [Space ()]
    public float delay;
    
    void Start()
    {
        if (readyManagers == 0) ActualImpostor = null;
        
        SpawnAnimal();
    }

    public void GetRandomPointOnNavmesh(out Vector3 result, float range = 1f)
    {
        Vector3 randomPoint = transform.position + new Vector3 (Random.Range (-3f * range, 3f * range), 0f, Random.Range (-3f * range, 3f * range));
        NavMeshHit hit;
        NavMesh.SamplePosition (randomPoint, out hit, 3f, NavMesh.AllAreas);
        result = hit.position;
    }

    private void SpawnAnimal()
    {
        Vector3 spawnPos;
        if (animalPrefab.name != "Kuda Nil"){
            GetRandomPointOnNavmesh (out spawnPos);
            
        }
        else
            spawnPos = transform.position;
        AnimalNPC animal = Instantiate(animalPrefab, spawnPos, Quaternion.Euler (animalPrefab.transform.eulerAngles));
        animal.npcManager = this;
        animal.transform.parent = transform;
        //animal.transform.localPosition = spawnPos;
        allSpawnedNPCs.Add(animal);
        readyManagers++;

        CurrentAnimal = animal;

        if (readyManagers == totalManagers)
        {
            AssignImpostor();
        }

    }

    public void OnSelected ()
    {
        if (TimerManager.gameTick >= durationUntilHideMoney){
            if (placedCoin != null && placedCoin.activeSelf)
            {
                if (CurrentAnimal == ActualImpostor)
                {
                    if (coinDestroyable)
                        placedCoin.SetActive (false);
                } else {
                    if (CurrentAnimal.transform.name.Contains("Panda")){
                        if (Random.value > 0.5f){
                            GetRandomPointOnNavmesh (out Vector3 newPos);
                            placedCoin.transform.position = newPos;
                        } else
                        {
                            CurrentAnimal.SitOnCoin (placedCoin);
                        }
                    } else
                    {
                        GetRandomPointOnNavmesh (out Vector3 newPos);
                        placedCoin.transform.position = newPos;
                    }
                }
            }
        }

        if (TimerManager.gameTick >= durationUntilPeekaboo){
            if (CurrentAnimal == ActualImpostor)
            {
                StartCoroutine (Peekaboo ());
            }
        }
    }

    IEnumerator WaitTillCoinIsDestroyable()
    {
        yield return new WaitForSeconds (15f);
        coinDestroyable = true;
    }
    private bool coinDestroyable = false;

    public void PlaceCoin (GameObject coin)
    {
        if (placedCoin != null)
        {
            Destroy (placedCoin);
        }
        placedCoin = coin;

        if (CurrentAnimal == ActualImpostor)
        {
            StopCoroutine (WaitTillCoinIsDestroyable ());
            coinDestroyable = false;
            StartCoroutine (WaitTillCoinIsDestroyable ());
        }
    }

    IEnumerator Peekaboo()
    {
        if (CurrentAnimal == null) yield break;
        CurrentAnimal.SetImpostorAppearance ();
        yield return new WaitForSeconds (delay);
        CurrentAnimal.SetNormalAppearance ();
    }

    private void AssignImpostor()
    {
        if (allSpawnedNPCs.Count == 0) return;

        int randomIndex = Random.Range(0, allSpawnedNPCs.Count);
        AnimalNPC chosenNPC = allSpawnedNPCs[randomIndex];

        chosenNPC.AssignImpostorRole();

        ActualImpostor = chosenNPC;

        allSpawnedNPCs.Clear();
        readyManagers = 0;
    }
}