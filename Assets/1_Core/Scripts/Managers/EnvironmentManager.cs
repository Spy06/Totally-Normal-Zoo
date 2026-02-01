using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Environment[] environments;
    public static Environment ActiveEnvironment;

    void Start()
    {
        SetEnvironment(0);
    } 

    public void SetEnvironment(int index)
    {
        ActiveEnvironment = environments[index];

        ActiveEnvironment.npcManager.OnSelected ();
    }
}
