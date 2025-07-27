using UnityEngine;

public class VolumeManager : MonoBehaviour
{

    private static VolumeManager instance;



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

		public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
