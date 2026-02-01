using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameStarted() => OnGameStarted?.Invoke();
    public event Action OnGameStarted;

    public void SelectedKandang(JenisHewan jenisHewan) => OnSelectedKandang?.Invoke(jenisHewan);
    public event Action<JenisHewan> OnSelectedKandang;

    public void BackToPos() => OnBackToPos?.Invoke();
    public event Action OnBackToPos;

    public void GettingTools(JenisTools jenisTools) => OnGettingTools?.Invoke(jenisTools);
    public event Action<JenisTools> OnGettingTools;

    public void SpawnCoin() => OnSpawnCoin?.Invoke();
    public event Action OnSpawnCoin;

    public void FinishChangingScene() => OnFinishChangingScene?.Invoke();
    public event Action OnFinishChangingScene;
}