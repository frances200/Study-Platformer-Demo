using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldManager : MonoBehaviour
{
    [Header("Red tilemap")]
    public Tilemap redTilemap;
    public TilemapCollider2D redCollider;

    [Header("Blue tilemap")]
    public Tilemap blueTilemap;
    public TilemapCollider2D blueCollider;
    
    [Header("Settings")]
    [Range(0f, 1f)] public float inactiveOpacity = 0.3f;

    private bool isRedActive = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateWorldState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRedActive = !isRedActive;
            UpdateWorldState();
        }
    }

    void UpdateWorldState()
    {
        SetTilemapState(redTilemap, redCollider, isRedActive);
        SetTilemapState(blueTilemap, blueCollider, !isRedActive);
    }

    void SetTilemapState(Tilemap tilemap, TilemapCollider2D tilemapCollider, bool isActive)
    {
        if (isActive)
        {
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f);
            tilemapCollider.enabled = true;
        }
        else
        {
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, inactiveOpacity);
            tilemapCollider.enabled = false;
        }
    }
}
