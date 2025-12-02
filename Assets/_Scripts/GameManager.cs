using UnityEngine;

public class GameManager : MonoBehaviour
{
  public void CompletePlayerTurn()
  {
    // ������ ����� ���� ������:
    // 1. ��������� ������
    // 2. �������� �������
    // 3. ���������� ����������
    Debug.Log("Player turn completed!");

    // ������ ������ ���� ������:
    // foreach (var enemy in FindObjectsOfType<Enemy>())
    // {
    //     enemy.ExecuteTurn();
    // }
  }
}