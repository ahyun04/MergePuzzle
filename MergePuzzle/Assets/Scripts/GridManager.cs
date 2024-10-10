using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Vector2Int> emptyGridIndexes = new List<Vector2Int>(); // 빈 공간 리스트

    // 빈 공간에 인덱스 추가
    public void AddEmptySpace(Vector2Int index)
    {
        emptyGridIndexes.Add(index);
    }

    // 빈 공간 중 하나를 반환
    public Vector2Int GetEmptySpace()
    {
        if (emptyGridIndexes.Count > 0)
        {
            Vector2Int emptyIndex = emptyGridIndexes[0];
            emptyGridIndexes.RemoveAt(0); // 반환 후 삭제
            return emptyIndex;
        }
        return new Vector2Int(-1, -1); // 만약 빈 공간이 없으면 에러값 반환
    }

    // 이미지가 움직일 때 빈 공간 갱신
    public void UpdateEmptySpace(Vector2Int oldIndex, Vector2Int newIndex)
    {
        AddEmptySpace(oldIndex);
        emptyGridIndexes.Remove(newIndex);
    }
}
