using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 position;
    private string imageName;
    private int index;
    private bool isClicked = false; // 클릭 상태를 추적하는 변수

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = transform.position; // Vector3로 설정
        imageName = spriteRenderer.sprite.name;

        // Collider2D가 없다면 추가합니다.
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    void OnMouseDown()
    {
        isClicked = true;
        spriteRenderer.enabled = false; // 이미지 숨기기
        if (isClicked)
        {
            Debug.Log($"({position.x}, {position.y}), {index}, {imageName}");
        }
    }

    void OnMouseUp()
    {
        isClicked = false;
        spriteRenderer.enabled = true; // 이미지 다시 보이게 하기
    }
}
