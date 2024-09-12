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

    // �̹��� ��ġ�� �����ϴ� ����Ʈ
    public static List<ImageClickHandler> imageHandlers = new List<ImageClickHandler>();

    void Start()
    {
        string fileContent = File.ReadAllText(filePath);
        string[] lines = fileContent.Split('\n'); // ���� �и�

        Vector2 currentPosition = startPosition;  // ���� �̹��� ��ġ

        foreach (string line in lines)
        {
            string[] numbers = line.Split(','); // ��ǥ�� ���ڸ� ������

            foreach (string number in numbers)
            {
                int numberInt = int.Parse(number.Trim());  // ���ڷ� ��ȯ

                string spritePath = "Images/" + numberInt;  // �̹��� ��� ����
                Sprite numberSprite = Resources.Load<Sprite>(spritePath);

                if (numberSprite != null)
                {
                    GameObject newImageObject = new GameObject("NumberImage");
                    Image imageComponent = newImageObject.AddComponent<Image>();
                    imageComponent.sprite = numberSprite;

                    newImageObject.transform.SetParent(canvasTransform, false);

                    // �̹����� ��ġ ����
                    RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = currentPosition;
                    rectTransform.sizeDelta = new Vector2(200, 200);  // �̹��� ũ�� ����

                    ImageClickHandler clickHandler = newImageObject.AddComponent<ImageClickHandler>();
                    clickHandler.Initialize(currentPosition, numberInt, numberToItemMap[numberInt], imageComponent);

                    // �ڵ鷯�� ����Ʈ�� �߰��Ͽ� ���߿� ���� ����
                    imageHandlers.Add(clickHandler);

                    currentPosition.x += ImageInterval.x;
                }
                else
                {
                    Debug.LogError("�̹����� �ҷ��� �� �����ϴ�: " + spritePath);
                }
            }

            // ���� ������ ���� �ٿ� �̹����� ��ġ (���η� ���� �߰�)
            currentPosition.x = startPosition.x;  // X ��ġ�� �ʱ�ȭ (���� �ٷ� �Ѿ��)
            currentPosition.y -= Interval;  // Y ��ġ�� ���� (�Ʒ��� �̵�)
        }
    }
}


public class ImageClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 position; // �̹����� ��ġ
    private int number;
    private TextReader.ItemType itemType;
    private Image imageComponent;

    // �ʱ�ȭ �޼���
    public void Initialize(Vector2 pos, int num, TextReader.ItemType type, Image img)
    {
        position = pos;
        number = num;
        itemType = type;
        imageComponent = img;
    }

    // ��ǥ�� ������ ���������� ��ȯ�ϴ� �Լ�
    private string GetFormattedPosition(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);  // X ��ǥ�� �ݿø��Ͽ� ������ ��ȯ
        int y = Mathf.RoundToInt(pos.y);  // Y ��ǥ�� �ݿø��Ͽ� ������ ��ȯ
        return $"({x}, {y})";
    }

    // Ŭ���� ��ġ���� ���� ����� �̹����� �ڵ鷯�� ã�� �Լ�
    private ImageClickHandler GetClosestImage(Vector2 clickPosition)
    {
        ImageClickHandler closestHandler = null;
        float closestDistance = float.MaxValue;

        // ��� �̹��� �ڵ鷯�� ��ȸ�ϸ� ���� ����� �̹����� ã��
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

    // PointerDown �̺�Ʈ ó�� (�̹��� ����)
    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.enabled = false;  // �̹����� ����

        // ������ �� �̹����� ��Ȯ�� ��ġ ���
        Debug.Log($"������: {GetFormattedPosition(position)}, {number}, {itemType}");
    }

    // PointerUp �̺�Ʈ ó�� (�� ��ġ�� ���� ����� �̹��� ���� ���)
    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.enabled = true;  // �̹����� �ٽ� ǥ��

        // ���콺 Ŭ�� ��ǥ�� ĵ������ ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageComponent.canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        // ���� ����� �̹��� �ڵ鷯 ã��
        ImageClickHandler closestHandler = GetClosestImage(localPoint);

        if (closestHandler != null)
        {
            // �� ��ġ�� �ִ� �̹����� ��ġ�� ���� ���
            Debug.Log($"����: {GetFormattedPosition(closestHandler.position)}, {closestHandler.number}, {closestHandler.itemType}");
        }
    }
}

*/