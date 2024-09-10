using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y; // Ÿ���� �׸��� ��ġ
    public int tileType; // Ÿ���� ����
    public SpriteRenderer spriteRenderer; // Ÿ���� �̹����� ����

    private Vector3 targetPosition; // Ÿ���� �̵��� ��ǥ ��ġ
    private float dropSpeed = 5.0f; // Ÿ���� �������� �ӵ�

    void Update()
    {
        // Ÿ���� ��ǥ ��ġ�� �̵��ϵ��� ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition; // ���� ��ġ�� ����
        }
    }

    // Ÿ���� ��ġ�� ���� (�ִϸ��̼� ����)
    public void SetPosition(int newX, int newY)
    {
        x = newX;
        y = newY;

        // Ÿ���� ������ �������� ȿ��
        transform.position = new Vector3(x, y, 0); // ó�� ������ �� ���ʿ� ��ġ
        targetPosition = new Vector3(x, y, 0);
    }

    // Ÿ���� ������ �̹��� ����
    public void SetTileType(int type, Sprite sprite)
    {
        tileType = type;
        spriteRenderer.sprite = sprite;
    }

}

