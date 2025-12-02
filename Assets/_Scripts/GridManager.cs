using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
  public Tilemap tilemap;
  public GameObject hexTileWhitePrefab;
  public GameObject hexTileRedPrefab;
  public int mapWidth = 10;
  public int mapHeight = 10;
  [Range(0, 1)]
  public float redTileProbability = 0.3f; // ����������� ��������� �������� �����.

  public List<Sprite> npcSprites = new List<Sprite>();
  public List<Sprite> itemSprites = new List<Sprite>();
  [Range(0, 1)]
  public float itemNPCRandomSpawnPossibility = 0.1f;

  public Vector3 spriteScale = new Vector3(16f, 16f, 16f); // ��������� ���������� ��� ��������

  void Start()
  {
    GenerateLevel();
  }

  void GenerateLevel()
  {
    // 1. ������� ����� �� ������.
    GenerateTiles();

    // 2. ��������� NPC � ��������.
    PopulateLevel();
  }

  void GenerateTiles()
  {
    for (int x = -mapWidth / 2; x < mapWidth / 2; x++)
    {
      for (int y = -mapHeight / 2; y < mapHeight / 2; y++)
      {
        // Convert to world position
        Vector3 worldPosition = tilemap.CellToWorld(new Vector3Int(x, y, 0));
        worldPosition.x += tilemap.cellSize.x / 2;
        worldPosition.y += tilemap.cellSize.y / 2;

        // Randomly choose between white and red tile
        GameObject tilePrefab = Random.value < redTileProbability ? hexTileRedPrefab : hexTileWhitePrefab;

        GameObject tile = Instantiate(tilePrefab, worldPosition, Quaternion.identity);
        tile.transform.SetParent(this.transform); // ���������� ��������.

        // Set the name for easier identification
        tile.name = $"Tile_{x}_{y}";
      }
    }
  }

  void PopulateLevel()
  {
    // Find all white tiles in the level
    GameObject[] tiles = GameObject.FindGameObjectsWithTag("HexTileWhite");

    foreach (GameObject tile in tiles)
    {
      if (Random.value < itemNPCRandomSpawnPossibility)
      {
        // Randomly choose between item and npc
        if (Random.value < 0.5f)
        {
          // Spawn item
          if (itemSprites.Count > 0)
          {
            int randomIndex = Random.Range(0, itemSprites.Count);
            SpawnItem(tile.transform.position, itemSprites[randomIndex]);
          }
          else
          {
            Debug.LogWarning("No item sprites assigned.");
          }
        }
        else
        {
          // Spawn NPC
          if (npcSprites.Count > 0)
          {
            int randomIndex = Random.Range(0, npcSprites.Count);
            SpawnNPC(tile.transform.position, npcSprites[randomIndex]);
          }
          else
          {
            Debug.LogWarning("No NPC sprites assigned.");
          }
        }
      }
    }
  }

  void SpawnNPC(Vector3 position, Sprite sprite)
  {
    GameObject npc = new GameObject("NPC");
    npc.transform.position = position;
    SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
    sr.sprite = sprite;
    npc.transform.SetParent(this.transform);
    npc.transform.localScale = spriteScale; // ������������� �������
  }

  void SpawnItem(Vector3 position, Sprite sprite)
  {
    GameObject item = new GameObject("Item");
    item.transform.position = position;
    SpriteRenderer sr = item.AddComponent<SpriteRenderer>();
    sr.sprite = sprite;
    item.transform.SetParent(this.transform);
    item.transform.localScale = spriteScale; // ������������� �������
  }
}