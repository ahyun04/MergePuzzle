using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<ImageClickHandler> imageHandlers = new List<ImageClickHandler>(); // 그리드 내 모든 이미지를 관리

    // 그리드를 다시 로드하는 함수
    public void ReloadGrid()
    {
        foreach (var imageHandler in imageHandlers)
        {
            // 그리드 인덱스에 맞춰 이미지를 다시 배치
            Vector3 newPosition = GetPositionFromGridIndex(imageHandler.gridIndex);
            imageHandler.transform.position = newPosition;
            imageHandler.originalPosition = newPosition;

        }
    }

    // 그리드 인덱스를 기반으로 실제 월드 좌표를 반환하는 함수
    Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        float xPos = gridIndex.x;
        float yPos = gridIndex.y;
        return new Vector3(xPos, yPos, 0f);
    }

    // ImageClickHandler를 리스트에 추가하는 함수
    public void RegisterImageHandler(ImageClickHandler handler)
    {
        if (!imageHandlers.Contains(handler))
        {
            imageHandlers.Add(handler);
            //Debug.Log($"이미지 {handler.imageName}가 GridManager에 등록됨.");
        }
    }
}
