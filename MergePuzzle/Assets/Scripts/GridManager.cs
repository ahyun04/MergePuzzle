using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Vector2Int> emptyGridIndexes = new List<Vector2Int>(); // �� ���� ����Ʈ

    // �� ������ �ε��� �߰�
    public void AddEmptySpace(Vector2Int index)
    {
        emptyGridIndexes.Add(index);
    }

    // �� ���� �� �ϳ��� ��ȯ
    public Vector2Int GetEmptySpace()
    {
        if (emptyGridIndexes.Count > 0)
        {
            Vector2Int emptyIndex = emptyGridIndexes[0];
            emptyGridIndexes.RemoveAt(0); // ��ȯ �� ����
            return emptyIndex;
        }
        return new Vector2Int(-1, -1); // ���� �� ������ ������ ������ ��ȯ
    }

    // �̹����� ������ �� �� ���� ����
    public void UpdateEmptySpace(Vector2Int oldIndex, Vector2Int newIndex)
    {
        AddEmptySpace(oldIndex);
        emptyGridIndexes.Remove(newIndex);
    }
}
