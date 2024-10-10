using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class ImageClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Vector3 originalPosition; // ���� ��ġ ����
    private Vector3 position;
    public string imageName;
    public Vector2Int gridIndex; // �׸��� �ε��� (x, y)
    private bool isClicked = false; // Ŭ�� ���¸� �����ϴ� ����
    private Vector3 offset; // �̹����� ���콺 ������ ������
    private Tween moveTween; // DOTween Tween ����

    public float minX = 0f; // �׸��� �ּ� X
    public float maxX = 3f; // �׸��� �ִ� X
    public float minY = 0f; // �׸��� �ּ� Y
    public float maxY = 3f; // �׸��� �ִ� Y
    public float gridSize = 1f; // �׸��� �� ũ��
    public float moveDuration = 0.5f; // DOTween�� ���� �̵� �ð�

    private GridManager gridManager; // �׸��带 �����ϴ� �Ŵ��� ����

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position; // ���� ��ġ ����
        position = transform.position; // Vector3�� ����
        imageName = spriteRenderer.sprite.name;

        // Collider2D�� ���ٸ� �߰�
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        // Rigidbody2D�� ���ٸ� �߰��ϰ� �Ӽ� ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Rigidbody2D�� �Ӽ� ����
        rb.gravityScale = 0; // �߷� ������ 0���� ����
        rb.isKinematic = true; // isKinematic�� true�� ����

        // �ʱ� �ε����� ���� (��ġ�� �������)
        gridIndex = GetGridIndexFromPosition(transform.position);

        // GridManager�� ã��
        gridManager = FindObjectOfType<GridManager>();

        // GridManager�� �ڽ��� ���
        // gridManager.RegisterImageHandler(this);
    }


    // �ε����� �����ϴ� �Լ� (x, y ��ǥ)
    public void SetGridIndex(Vector2Int index)
    {
        gridIndex = index;
    }

    void OnMouseDown()
    {
        isClicked = true;
        // ���콺�� �̹��� ������ ������ ���
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D ȯ�濡�� Z���� ����
        offset = transform.position - mousePosition;

        Debug.Log($"������ : ({gridIndex.x}, {gridIndex.y}), {imageName}");
    }

    void OnMouseDrag()
    {
        if (isClicked)
        {
            // ���콺 ��ġ�� ����Ͽ� �̹����� ���ο� ��ġ ����
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D���� Z���� ����

            // DOTween�� ����Ͽ� �ε巴�� �̵�
            if (moveTween != null && moveTween.IsActive())
            {
                moveTween.Kill(); // ���� Tween ����
            }

            // �̹����� �ε巴�� ���콺 ��ġ�� ���� �̵�
            moveTween = transform.DOMove(mousePosition + offset, 0.01f).SetEase(Ease.InOutQuad);

            // �׸��� �ٱ����� ������ �ʵ��� ����
            Vector3 currentPosition = transform.position;
            if (currentPosition.x < minX || currentPosition.x > maxX ||
                currentPosition.y < minY || currentPosition.y > maxY)
            {
                // ���� ��ġ�� �ǵ�����
                moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
            }
        }
    }

    void OnMouseUp()
    {
        isClicked = false;

        // �׸��� �ȿ� �ִ��� Ȯ��
        if (!IsWithinGrid())
        {
            // �׸��� �ٱ��� ������ ���� �ڸ��� ���ư�
            moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
        }
        else
        {
            // �ٸ� �̹������� ��ü ���� ó��
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                ImageClickHandler hitImageHandler = hit.collider.GetComponent<ImageClickHandler>();
                if (hitImageHandler != null)
                {
                    // �ٸ� �̹����� �ڸ��� ��ȯ
                    //Debug.Log($"��ħ : ({hitImageHandler.gridIndex.x}, {hitImageHandler.gridIndex.y}), {hitImageHandler.imageName}");
                    SwapPosition(hitImageHandler);
                    Debug.Log($"���� : ({gridIndex.x}, {gridIndex.y}), {imageName}");
                }
                else
                {
                    // ��ȯ�� �Ұ����ϸ� ���� �ڸ��� ����
                    moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
                    Debug.Log($"���� : ({gridIndex.x}, {gridIndex.y}), {imageName} (��ȯ �Ұ�)");
                }
            }
            else
            {
                // �ƹ��͵� ������ ���� �ڸ��� ����
                moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
                Debug.Log($"���� : ({gridIndex.x}, {gridIndex.y}), {imageName} (�ƹ��͵� ����)");
            }

            // �̵� �� �ε��� ������Ʈ
            gridIndex = GetGridIndexFromPosition(transform.position);
            Debug.Log($"�̵��� : ({gridIndex.x}, {gridIndex.y}), {imageName}");

            // �׸��� ������Ʈ
            //gridManager.ReloadGrid(); // �׸��� �Ŵ����� ReloadGrid() ȣ��
        }
    }

    // �׸��� �ȿ� �ִ��� Ȯ��
    bool IsWithinGrid()
    {
        Vector3 currentPosition = transform.position;
        return currentPosition.x >= minX && currentPosition.x <= maxX &&
               currentPosition.y >= minY && currentPosition.y <= maxY;
    }

    // �ٸ� �̹����� �ڸ� ��ȯ (DOTween�� �̿��� ��ȯ �ִϸ��̼�)
    void SwapPosition(ImageClickHandler otherImage)
    {
        // 1. ���� �������� originalPosition�� gridIndex �ӽ� ����
        Vector3 tempOriginalPosition = originalPosition;
        Vector2Int tempGridIndex = gridIndex;

        // 2. DOTween�� ����� �ε巴�� ��ġ ��ȯ
        otherImage.moveTween = otherImage.transform.DOMove(tempOriginalPosition, moveDuration).SetEase(Ease.InOutQuad);
        moveTween = transform.DOMove(otherImage.originalPosition, moveDuration).SetEase(Ease.InOutQuad);

        // 3. �׸��� �Ŵ������� �� ���� ó��
        gridManager.UpdateEmptySpace(gridIndex, otherImage.gridIndex);

        // 4. �ε����� ��ġ ����
        gridIndex = otherImage.gridIndex; // ���� ������ �ε����� �ٸ� ������ �ε�����
        otherImage.gridIndex = tempGridIndex; // �ٸ� ������ �ε����� ���� �������� �ε�����

        // 5. originalPosition ����: ��ȯ �� ��ġ�� ����
        originalPosition = otherImage.originalPosition; // ���� ������ ��ġ ����
        otherImage.originalPosition = tempOriginalPosition; // �ٸ� ������ ��ġ ����

        // �α� ���
        Debug.Log($"��ȯ��: {imageName}�� ({gridIndex.x}, {gridIndex.y})�� �̵���, {otherImage.imageName}�� ({otherImage.gridIndex.x}, {otherImage.gridIndex.y})�� �̵���");
    }



    // ��ġ�� �ε����� ��ȯ�ϴ� �Լ�
    Vector2Int GetGridIndexFromPosition(Vector3 position)
    {
        int xIndex = Mathf.RoundToInt((position.x - minX) / gridSize);
        int yIndex = Mathf.RoundToInt((position.y - minY) / gridSize);
        return new Vector2Int(xIndex, yIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ImageClickHandler otherImage = collision.gameObject.GetComponent<ImageClickHandler>();
        if (otherImage != null)
        {
            // �ٸ� �̹����� �ڸ� ��ȯ
            SwapPosition(otherImage);
            Debug.Log($"�浹�Ͽ� ��ȯ�� : {imageName} ({gridIndex.x}, {gridIndex.y}) <-> {otherImage.imageName} ({otherImage.gridIndex.x}, {otherImage.gridIndex.y})");
        }
    }

}
