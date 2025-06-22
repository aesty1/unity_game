using UnityEngine;

public class GameManager : MonoBehaviour
{
  public void CompletePlayerTurn()
  {
    // Логика после хода игрока:
    // 1. Активация врагов
    // 2. Проверка событий
    // 3. Обновление интерфейса
    Debug.Log("Player turn completed!");

    // Пример вызова хода врагов:
    // foreach (var enemy in FindObjectsOfType<Enemy>())
    // {
    //     enemy.ExecuteTurn();
    // }
  }
}