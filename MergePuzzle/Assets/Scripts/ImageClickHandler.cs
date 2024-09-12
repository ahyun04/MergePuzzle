using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 position;
    private string imageName;
    private int index;
    private bool isClicked = false; // Ŭ�� ���¸� �����ϴ� ����

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = transform.position; // Vector3�� ����
        imageName = spriteRenderer.sprite.name;

        // Collider2D�� ���ٸ� �߰��մϴ�.
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
        spriteRenderer.enabled = false; // �̹��� �����
        if (isClicked)
        {
            Debug.Log($"({position.x}, {position.y}), {index}, {imageName}");
        }
    }

    void OnMouseUp()
    {
        isClicked = false;
        spriteRenderer.enabled = true; // �̹��� �ٽ� ���̰� �ϱ�
    }
}
