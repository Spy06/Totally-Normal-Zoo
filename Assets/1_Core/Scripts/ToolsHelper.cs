using UnityEngine;

public class ToolsHelper : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject coffeePrefab;

    private void OnEnable()
    {
        GameEventManager.Instance.OnSpawnCoin += SpawnCoin;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnSpawnCoin -= SpawnCoin;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCoin();
        }*/
    }

    private void SpawnCoin()
    {
        EnvironmentManager.ActiveEnvironment.npcManager.GetRandomPointOnNavmesh (out Vector3 spawnPos);
        GameObject spawnedCoin = Instantiate(coinPrefab, 
         spawnPos, 
         EnvironmentManager.ActiveEnvironment.npcManager.CurrentAnimal.transform.rotation);
        
        EnvironmentManager.ActiveEnvironment.npcManager.PlaceCoin (spawnedCoin);
    }
}
