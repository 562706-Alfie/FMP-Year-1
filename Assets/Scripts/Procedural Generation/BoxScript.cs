using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BoxScript : MonoBehaviour
{
    Collider2D cloneCollider;
    public Vector3 oldcloneSize;
    public Vector3 newcloneSize;

    public Vector3 oldTileWidth;
    public Vector3 newTileWidth;
    public bool widthCheckComplete = false;

    public TileInstantiate TI;

    void Start()
    {
        cloneCollider = GetComponent<Collider2D>();
    }

    void Update()
    {

    }
   
}
