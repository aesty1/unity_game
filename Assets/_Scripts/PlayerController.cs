using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private Tilemap walkableTilemap;
  [SerializeField] private float moveDuration = 0.3f;

  private bool isMoving;
  private Vector3Int currentCell;

  private void Start()
  {
    currentCell = walkableTilemap.WorldToCell(transform.position);
  }

  [System.Obsolete]
  private void Update()
  {
    if (isMoving) return;

    if (Input.GetMouseButtonDown(0))
    {
      Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int targetCell = walkableTilemap.WorldToCell(worldPos);

      if (IsValidMove(targetCell))
      {
        StartCoroutine(MoveToCell(targetCell));
      }
    }
  }

  private bool IsValidMove(Vector3Int targetCell)
  {
    // Проверка расстояния (только соседние клетки)
    if (Vector3Int.Distance(currentCell, targetCell) > 1.5f)
      return false;

    // Проверка проходимости
    return walkableTilemap.HasTile(targetCell);
  }

  [System.Obsolete]
  private System.Collections.IEnumerator MoveToCell(Vector3Int targetCell)
  {
    isMoving = true;
    Vector3 startPos = transform.position;
    Vector3 endPos = walkableTilemap.GetCellCenterWorld(targetCell);
    float elapsed = 0;

    while (elapsed < moveDuration)
    {
      transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
      elapsed += Time.deltaTime;
      yield return null;
    }

    transform.position = endPos;
    currentCell = targetCell;
    isMoving = false;

    // Тут вызов логики хода врагов/событий
    FindObjectOfType<GameManager>().CompletePlayerTurn();
  }
}