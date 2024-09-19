using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween 네임스페이스 추가

public class ImageClickHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition; // 원래 위치 저장
    private Vector3 position;
    private string imageName;
    private Vector2Int gridIndex; // 그리드 인덱스 (x, y)
    private bool isClicked = false; // 클릭 상태를 추적하는 변수
    private Vector3 offset; // 이미지와 마우스 사이의 오프셋
    private Tween moveTween; // DOTween Tween 변수

    public float minX = 0f; // 그리드 최소 X
    public float maxX = 3f; // 그리드 최대 X
    public float minY = 0f; // 그리드 최소 Y
    public float maxY = 3f; // 그리드 최대 Y
    public float gridSize = 1f; // 그리드 셀 크기
    public float moveDuration = 0.5f; // DOTween을 통한 이동 시간

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position; // 원래 위치 저장
        position = transform.position; // Vector3로 설정
        imageName = spriteRenderer.sprite.name;

        // Collider2D가 없다면 추가
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        // 초기 인덱스를 설정 (위치를 기반으로)
        gridIndex = GetGridIndexFromPosition(transform.position);
    }

    // 인덱스를 설정하는 함수 (x, y 좌표)
    public void SetGridIndex(Vector2Int index)
    {
        gridIndex = index;
    }

    void OnMouseDown()
    {
        isClicked = true;
        // 마우스와 이미지 사이의 오프셋 계산
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 환경에서 Z축은 고정
        offset = transform.position - mousePosition;

        Debug.Log($"눌렀음 : ({gridIndex.x}, {gridIndex.y}), {imageName}");
    }

    void OnMouseDrag()
    {
        if (isClicked)
        {
            // 마우스 위치를 계산하여 이미지의 새로운 위치 설정
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D에서 Z축은 고정

            // DOTween을 사용하여 부드럽게 이동
            if (moveTween != null && moveTween.IsActive())
            {
                moveTween.Kill(); // 기존 Tween 종료
            }

            // 이미지가 부드럽게 마우스 위치를 따라 이동
            moveTween = transform.DOMove(mousePosition + offset, 0.01f).SetEase(Ease.InOutQuad);

            // 그리드 바깥으로 나가지 않도록 제한
            Vector3 currentPosition = transform.position;
            if (currentPosition.x < minX || currentPosition.x > maxX ||
                currentPosition.y < minY || currentPosition.y > maxY)
            {
                // 원래 위치로 되돌리기
                moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
            }
        }
    }

    void OnMouseUp()
    {
        isClicked = false;

        // 그리드 안에 있는지 확인
        if (!IsWithinGrid())
        {
            // 그리드 바깥에 놓으면 원래 자리로 돌아감
            moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
        }
        else
        {
            // 다른 이미지와의 교체 로직 처리
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                ImageClickHandler hitImageHandler = hit.collider.GetComponent<ImageClickHandler>();
                if (hitImageHandler != null)
                {
                    // 다른 이미지와 자리를 교환
                    SwapPosition(hitImageHandler);
                }
                else
                {
                    // 교환이 불가능하면 원래 자리로 복귀
                    moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
                }
            }
            else
            {
                // 아무것도 없으면 원래 자리로 복귀
                moveTween = transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
            }

            // 이동 후 인덱스 업데이트
            gridIndex = GetGridIndexFromPosition(transform.position);
            Debug.Log($"새 위치 : ({gridIndex.x}, {gridIndex.y}), {imageName}");
        }
    }

    // 그리드 안에 있는지 확인
    bool IsWithinGrid()
    {
        Vector3 currentPosition = transform.position;
        return currentPosition.x >= minX && currentPosition.x <= maxX &&
               currentPosition.y >= minY && currentPosition.y <= maxY;
    }

    // 다른 이미지와 자리 교환
    void SwapPosition(ImageClickHandler otherImage)
    {
        Vector3 tempPosition = otherImage.transform.position;
        Vector2Int tempIndex = otherImage.gridIndex;

        otherImage.moveTween = otherImage.transform.DOMove(originalPosition, moveDuration).SetEase(Ease.InOutQuad);
        moveTween = transform.DOMove(tempPosition, moveDuration).SetEase(Ease.InOutQuad);

        // 자리 및 인덱스 업데이트
        originalPosition = tempPosition;
        gridIndex = tempIndex;

        otherImage.originalPosition = tempPosition;
        otherImage.SetGridIndex(tempIndex);
    }

    // 위치를 인덱스로 변환하는 함수
    Vector2Int GetGridIndexFromPosition(Vector3 position)
    {
        int xIndex = Mathf.RoundToInt((position.x - minX) / gridSize);
        int yIndex = Mathf.RoundToInt((position.y - minY) / gridSize);
        return new Vector2Int(xIndex, yIndex);
    }
}
