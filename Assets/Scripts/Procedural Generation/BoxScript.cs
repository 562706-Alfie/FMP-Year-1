using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BoxScript : MonoBehaviour
{
    public Transform playerCamera;
    public Transform tile;
    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector3.Distance(playerCamera.position, tile.transform.position);
        if ( distance <= 100f)
        {
            //Destroy(game);
        }
    }
   
}
