/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class TextReader : MonoBehaviour
{
    public string filePath = "Assets/Resources/testdata.txt";
    public Vector2 startPosition = new Vector2(0, 0);
    public Vector2 ImageInterval = new Vector2(200, 200);
    public float Interval = 200f;
    public Transform canvasTransform;

    // enum
    public enum ItemType
    {
        Jelly,
        Pudding,
        Star
    }

    private Dictionary<int, ItemType> numberToItemMap = new Dictionary<int, ItemType>()
    {
        { 1, ItemType.Jelly },
        { 2, ItemType.Pudding },
        { 3, ItemType.Star }
    };

    // 이미지 위치를 저장하는 리스트
    public static List<ImageClickHandler> imageHandlers = new List<ImageClickHandler>();

    void Start()
    {
        string fileContent = File.ReadAllText(filePath);
        string[] lines = fileContent.Split('\n'); // 줄을 분리

        Vector2 currentPosition = startPosition;  // 현재 이미지 위치

        foreach (string line in lines)
        {
            string[] numbers = line.Split(','); // 쉼표로 숫자를 나누기

            foreach (string number in numbers)
            {
                int numberInt = int.Parse(number.Trim());  // 숫자로 변환

                string spritePath = "Images/" + numberInt;  // 이미지 경로 추정
                Sprite numberSprite = Resources.Load<Sprite>(spritePath);

                if (numberSprite != null)
                {
                    GameObject newImageObject = new GameObject("NumberImage");
                    Image imageComponent = newImageObject.AddComponent<Image>();
                    imageComponent.sprite = numberSprite;

                    newImageObject.transform.SetParent(canvasTransform, false);

                    // 이미지의 위치 설정
                    RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = currentPosition;
                    rectTransform.sizeDelta = new Vector2(200, 200);  // 이미지 크기 설정

                    ImageClickHandler clickHandler = newImageObject.AddComponent<ImageClickHandler>();
                    clickHandler.Initialize(currentPosition, numberInt, numberToItemMap[numberInt], imageComponent);

                    // 핸들러를 리스트에 추가하여 나중에 접근 가능
                    imageHandlers.Add(clickHandler);

                    currentPosition.x += ImageInterval.x;
                }
                else
                {
                    Debug.LogError("이미지를 불러올 수 없습니다: " + spritePath);
                }
            }

            // 줄이 끝나면 다음 줄에 이미지를 배치 (세로로 간격 추가)
            currentPosition.x = startPosition.x;  // X 위치를 초기화 (다음 줄로 넘어가기)
            currentPosition.y -= Interval;  // Y 위치를 갱신 (아래로 이동)
        }
    }
}


public class ImageClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 position; // 이미지의 위치
    private int number;
    private TextReader.ItemType itemType;
    private Image imageComponent;

    // 초기화 메서드
    public void Initialize(Vector2 pos, int num, TextReader.ItemType type, Image img)
    {
        position = pos;
        number = num;
        itemType = type;
        imageComponent = img;
    }

    // 좌표를 간단한 정수형으로 변환하는 함수
    private string GetFormattedPosition(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);  // X 좌표를 반올림하여 정수로 변환
        int y = Mathf.RoundToInt(pos.y);  // Y 좌표를 반올림하여 정수로 변환
        return $"({x}, {y})";
    }

    // 클릭된 위치에서 가장 가까운 이미지의 핸들러를 찾는 함수
    private ImageClickHandler GetClosestImage(Vector2 clickPosition)
    {
        ImageClickHandler closestHandler = null;
        float closestDistance = float.MaxValue;

        // 모든 이미지 핸들러를 순회하며 가장 가까운 이미지를 찾음
        foreach (var handler in TextReader.imageHandlers)
        {
            float distance = Vector2.Distance(clickPosition, handler.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHandler = handler;
            }
        }

        return closestHandler;
    }

    // PointerDown 이벤트 처리 (이미지 숨김)
    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.enabled = false;  // 이미지를 숨김

        // 눌렀을 때 이미지의 정확한 위치 출력
        Debug.Log($"눌렀음: {GetFormattedPosition(position)}, {number}, {itemType}");
    }

    // PointerUp 이벤트 처리 (뗀 위치의 가장 가까운 이미지 정보 출력)
    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.enabled = true;  // 이미지를 다시 표시

        // 마우스 클릭 좌표를 캔버스의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageComponent.canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        // 가장 가까운 이미지 핸들러 찾기
        ImageClickHandler closestHandler = GetClosestImage(localPoint);

        if (closestHandler != null)
        {
            // 뗀 위치에 있는 이미지의 위치와 정보 출력
            Debug.Log($"뗐음: {GetFormattedPosition(closestHandler.position)}, {closestHandler.number}, {closestHandler.itemType}");
        }
    }
}

*/