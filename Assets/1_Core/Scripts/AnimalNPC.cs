using UnityEngine;
using UnityEngine.AI;


public class AnimalNPC : MonoBehaviour
{
    [Header ("Movement Settings")]    
    NavMeshAgent nma;
    [SerializeField] float speed;
    [SerializeField] bool canSit = false;

    [Header ("Sprite")]
    [SerializeField] SpriteRenderer[] spriteRenderer;
    [SerializeField] Sprite normalSprite, impostorSprite, sitSprite;

    private bool isSitting = false;
    float timer = 0;
    Vector3 direction;
    
    [HideInInspector]public NPCManager npcManager;

    void Start()
    {
        nma = GetComponent<NavMeshAgent> ();
        nma.speed = speed;

        timer = Random.Range (3f, 7f);
    } 

    void GetRandomPointOnNavmesh(out Vector3 result)
    {
        Vector3 randomPoint = transform.position + new Vector3 (Random.Range (-3f, 3f), -5f, Random.Range (-3f, 3f));
        NavMeshHit hit;
        NavMesh.SamplePosition (randomPoint, out hit, 3f, NavMesh.AllAreas);
        result = hit.position;
    }

    void Update()
    {
        if (speed == 0)return;
        if (!isSitting){
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetRandomPointOnNavmesh (out Vector3 randomPos);
                nma.SetDestination (randomPos);
                timer = Random.Range (3f, 7f);
            }
        }
    }

    public void SitOnCoin(GameObject coin)
    {
        if (!canSit) return;
        isSitting = true;
        nma.Warp (coin.transform.position);
        foreach (SpriteRenderer sr in spriteRenderer) sr.sprite = sitSprite;
    }

    public void AssignImpostorRole()
    {
        gameObject.tag = Selector.instance.impostorAnimalTag;
    }

    public void SetImpostorAppearance()
    {
        foreach (SpriteRenderer sr in spriteRenderer) sr.sprite = impostorSprite;
    }

    public void SetNormalAppearance()
    {
        foreach (SpriteRenderer sr in spriteRenderer) sr.sprite = normalSprite;
    }

    public void Interact()
    {
        isSitting = false;
        foreach (SpriteRenderer sr in spriteRenderer) sr.sprite = normalSprite;
    }
}
