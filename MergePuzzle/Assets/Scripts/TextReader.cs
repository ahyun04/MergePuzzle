using System.Collections;
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

    // enum 정의
    public enum ItemType
    {
        Jelly,
        Pudding,
        Star
    }

    // 숫자에 따라 enum 값을 매핑하는 Dictionary
    private Dictionary<int, ItemType> numberToItemMap = new Dictionary<int, ItemType>()
    {
        { 1, ItemType.Jelly },
        { 2, ItemType.Pudding },
        { 3, ItemType.Star }
    };

    void Start()
    {
        // 텍스트 파일 읽기
        string fileContent = File.ReadAllText(filePath);

        // 각 줄을 분리
        string[] lines = fileContent.Split('\n'); // 줄을 분리

        Vector2 currentPosition = startPosition;  // 현재 이미지 위치

        // 각 줄을 순회하면서 쉼표로 분리하고 이미지 생성
        foreach (string line in lines)
        {
            string[] numbers = line.Split(','); // 쉼표로 숫자를 나누기

            foreach (string number in numbers)
            {
                int numberInt = int.Parse(number.Trim());  // 숫자로 변환

                // 해당 숫자에 대응하는 이미지를 Resources에서 불러오기
                string spritePath = "Images/" + numberInt;  // 이미지 경로 추정
                Sprite numberSprite = Resources.Load<Sprite>(spritePath);

                if (numberSprite != null)
                {
                    // 새로운 이미지 오브젝트 생성
                    GameObject newImageObject = new GameObject("NumberImage");
                    Image imageComponent = newImageObject.AddComponent<Image>();
                    imageComponent.sprite = numberSprite;

                    // 이미지를 Canvas에 추가
                    newImageObject.transform.SetParent(canvasTransform, false);

                    // 이미지의 위치 설정
                    RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = currentPosition;
                    rectTransform.sizeDelta = new Vector2(100, 100);  // 이미지 크기 설정

                    // 클릭 이벤트를 위한 ImageClickHandler 추가
                    ImageClickHandler clickHandler = newImageObject.AddComponent<ImageClickHandler>();
                    clickHandler.Initialize(currentPosition, numberInt, numberToItemMap[numberInt], imageComponent);

                    // 다음 이미지를 위한 좌표 갱신 (가로로 배치)
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
    private Vector2 position;
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

    // PointerDown 이벤트 처리 (이미지 숨김)
    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.enabled = false;  // 이미지를 숨김
        Debug.Log($"Pressed: ({position.x}, {position.y}), {number}, {itemType}");
    }

    // PointerUp 이벤트 처리 (이미지 다시 보이게)
    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.enabled = true;  // 이미지를 다시 표시
        Debug.Log($"Released: ({position.x}, {position.y}), {number}, {itemType}");
    }
}
