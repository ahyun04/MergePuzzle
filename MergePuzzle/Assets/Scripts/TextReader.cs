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

    // enum ����
    public enum ItemType
    {
        Jelly,
        Pudding,
        Star
    }

    // ���ڿ� ���� enum ���� �����ϴ� Dictionary
    private Dictionary<int, ItemType> numberToItemMap = new Dictionary<int, ItemType>()
    {
        { 1, ItemType.Jelly },
        { 2, ItemType.Pudding },
        { 3, ItemType.Star }
    };

    void Start()
    {
        // �ؽ�Ʈ ���� �б�
        string fileContent = File.ReadAllText(filePath);

        // �� ���� �и�
        string[] lines = fileContent.Split('\n'); // ���� �и�

        Vector2 currentPosition = startPosition;  // ���� �̹��� ��ġ

        // �� ���� ��ȸ�ϸ鼭 ��ǥ�� �и��ϰ� �̹��� ����
        foreach (string line in lines)
        {
            string[] numbers = line.Split(','); // ��ǥ�� ���ڸ� ������

            foreach (string number in numbers)
            {
                int numberInt = int.Parse(number.Trim());  // ���ڷ� ��ȯ

                // �ش� ���ڿ� �����ϴ� �̹����� Resources���� �ҷ�����
                string spritePath = "Images/" + numberInt;  // �̹��� ��� ����
                Sprite numberSprite = Resources.Load<Sprite>(spritePath);

                if (numberSprite != null)
                {
                    // ���ο� �̹��� ������Ʈ ����
                    GameObject newImageObject = new GameObject("NumberImage");
                    Image imageComponent = newImageObject.AddComponent<Image>();
                    imageComponent.sprite = numberSprite;

                    // �̹����� Canvas�� �߰�
                    newImageObject.transform.SetParent(canvasTransform, false);

                    // �̹����� ��ġ ����
                    RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = currentPosition;
                    rectTransform.sizeDelta = new Vector2(100, 100);  // �̹��� ũ�� ����

                    // Ŭ�� �̺�Ʈ�� ���� ImageClickHandler �߰�
                    ImageClickHandler clickHandler = newImageObject.AddComponent<ImageClickHandler>();
                    clickHandler.Initialize(currentPosition, numberInt, numberToItemMap[numberInt], imageComponent);

                    // ���� �̹����� ���� ��ǥ ���� (���η� ��ġ)
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
    private Vector2 position;
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

    // PointerDown �̺�Ʈ ó�� (�̹��� ����)
    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.enabled = false;  // �̹����� ����
        Debug.Log($"Pressed: ({position.x}, {position.y}), {number}, {itemType}");
    }

    // PointerUp �̺�Ʈ ó�� (�̹��� �ٽ� ���̰�)
    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.enabled = true;  // �̹����� �ٽ� ǥ��
        Debug.Log($"Released: ({position.x}, {position.y}), {number}, {itemType}");
    }
}
