using UnityEngine;

public class VolumeManager : MonoBehaviour
{

    private static VolumeManager instance;

    public float volumeMulti = 1f;

    public static VolumeManager Instance()
    {
        return instance;
    }
    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if(instance == null) instance = this;
    }
}
