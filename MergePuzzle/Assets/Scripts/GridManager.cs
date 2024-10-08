using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<ImageClickHandler> imageHandlers = new List<ImageClickHandler>(); // �׸��� �� ��� �̹����� ����

    // �׸��带 �ٽ� �ε��ϴ� �Լ�
    public void ReloadGrid()
    {
        foreach (var imageHandler in imageHandlers)
        {
            // �׸��� �ε����� ���� �̹����� �ٽ� ��ġ
            Vector3 newPosition = GetPositionFromGridIndex(imageHandler.gridIndex);
            imageHandler.transform.position = newPosition;
            imageHandler.originalPosition = newPosition;

        }
    }

    // �׸��� �ε����� ������� ���� ���� ��ǥ�� ��ȯ�ϴ� �Լ�
    Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        float xPos = gridIndex.x;
        float yPos = gridIndex.y;
        return new Vector3(xPos, yPos, 0f);
    }

    // ImageClickHandler�� ����Ʈ�� �߰��ϴ� �Լ�
    public void RegisterImageHandler(ImageClickHandler handler)
    {
        if (!imageHandlers.Contains(handler))
        {
            imageHandlers.Add(handler);
            //Debug.Log($"�̹��� {handler.imageName}�� GridManager�� ��ϵ�.");
        }
    }
}
