using UnityEngine;

public class snowGenSceneTransfer : MonoBehaviour
{
    public static snowGenSceneTransfer SnowGenSceneTransfer;

    void Awake()
    {
        if (SnowGenSceneTransfer == null)
        {
            // if instance is null, store a reference to this instance
            SnowGenSceneTransfer = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it as we already have one
            Destroy(gameObject);
            return;
        }
    }
}
