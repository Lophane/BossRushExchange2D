using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        // Check if we have an existing BackgroundMusic GameObject in the scene,
        // we only want one instance of it to exist.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // This makes the object not be destroyed automatically when loading a new scene.
        DontDestroyOnLoad(this.gameObject);
    }
}
