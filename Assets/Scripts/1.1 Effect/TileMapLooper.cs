using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapLooper : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform tilemapA;
    [SerializeField] private Transform tilemapB;
    [SerializeField] private Transform swapPoint1;
    [SerializeField] private Transform swapPoint2;

    private float tilemapWidth;
    private bool useSwapPoint1 = true;

    void Start()
    {
        // Tính chiều rộng dựa trên bounds tilemap A
        Tilemap tm = tilemapA.GetComponent<Tilemap>();
        if (tm != null)
        {
            BoundsInt bounds = tm.cellBounds;
            float cellSize = tm.layoutGrid.cellSize.x;
            tilemapWidth = bounds.size.x * cellSize;
            Debug.Log("Tilemap width = " + tilemapWidth);
        }
    }

    void Update()
    {
        if (cam == null) return;

        if (useSwapPoint1 && cam.position.x >= swapPoint2.position.x)
        {
            // dịch A ra trước B
            tilemapA.position = new Vector3(
                tilemapB.position.x + tilemapWidth,
                tilemapA.position.y,
                tilemapA.position.z
            );
            useSwapPoint1 = false; // lần sau check swapPoint1
        }
        else if (!useSwapPoint1 && cam.position.x >= swapPoint1.position.x)
        {
            // dịch B ra trước A
            tilemapB.position = new Vector3(
                tilemapA.position.x + tilemapWidth,
                tilemapB.position.y,
                tilemapB.position.z
            );
            useSwapPoint1 = true; // lần sau check swapPoint2
        }
    }
}
