using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public static Selector instance;
    void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(gameObject);
    }
    public string realAnimalTag, impostorAnimalTag;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.TryGetComponent(out AnimalNPC animal))
                {
                    animal.Interact();
                }
            }
        }
    }
}