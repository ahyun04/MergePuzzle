using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y; // 타일의 그리드 위치
    public int tileType; // 타일의 종류
    public SpriteRenderer spriteRenderer; // 타일의 이미지를 관리

    private Vector3 targetPosition; // 타일이 이동할 목표 위치
    private float dropSpeed = 5.0f; // 타일이 떨어지는 속도

    void Update()
    {
        // 타일이 목표 위치로 이동하도록 함
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition; // 최종 위치로 맞춤
        }
    }

    // 타일의 위치를 설정 (애니메이션 포함)
    public void SetPosition(int newX, int newY)
    {
        x = newX;
        y = newY;

        // 타일이 위에서 떨어지는 효과
        transform.position = new Vector3(x, y, 0); // 처음 생성될 때 위쪽에 위치
        targetPosition = new Vector3(x, y, 0);
    }

    // 타일의 종류와 이미지 변경
    public void SetTileType(int type, Sprite sprite)
    {
        tileType = type;
        spriteRenderer.sprite = sprite;
    }

}

