using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public enum ImageType
    {
        Jelly = 1,
        Pudding = 2,
        Star = 3
    }

    public string filePath = "Assets/Resources/testdata.txt";
    public Vector2 startPosition = new Vector2(0, 0);
    public float imageSize = 1f; // �̹��� ũ�� (��������Ʈ�� ���� �����Ͽ� ������ ��)

    private Sprite[] sprites; // ��������Ʈ �迭�� ����

    void Start()
    {
        // �ؽ�Ʈ ������ �о�ɴϴ�.
        TextAsset textAsset = Resources.Load<TextAsset>("testdata");
        if (textAsset != null)
        {
            string fileContent = textAsset.text;
            LoadSprites(); // ��������Ʈ�� �ε�
            DisplayImagesFromText(fileContent);
        }
        else
        {
            Debug.LogError("Failed to load text file.");
        }
    }

    void LoadSprites()
    {
        // ��������Ʈ�� �̸� �ε�
        sprites = new Sprite[]
        {
            Resources.Load<Sprite>("Images/" + ImageType.Jelly.ToString()),   // "Images/Jelly"
            Resources.Load<Sprite>("Images/" + ImageType.Pudding.ToString()), // "Images/Pudding"
            Resources.Load<Sprite>("Images/" + ImageType.Star.ToString())     // "Images/Star"
        };
    }

    void DisplayImagesFromText(string content)
    {
        string[] lines = content.Split('\n');
        Vector2 currentPosition = startPosition;

        foreach (string line in lines)
        {
            string[] indices = line.Split(',');
            float xOffset = 0;

            foreach (string index in indices)
            {
                if (int.TryParse(index.Trim(), out int imageIndex))
                {
                    ImageType imageType = (ImageType)imageIndex;
                    Sprite sprite = GetSpriteFromEnum(imageType);

                    if (sprite != null)
                    {
                        CreateImage(sprite, xOffset, -currentPosition.y, imageIndex); // �ε��� ���� ����
                        xOffset += imageSize; // �̹��� ������ ���� ����
                    }
                    else
                    {
                        Debug.LogError($"Failed to load sprite for enum: {imageType}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid index: {index}");
                }
            }

            currentPosition.y -= imageSize; // �� ������ ���� ����
            currentPosition.x = startPosition.x; // ���ο� ���� ���� ��ġ�� �ʱ�ȭ
        }
    }

    Sprite GetSpriteFromEnum(ImageType imageType)
    {
        int index = (int)imageType - 1; // 0-based index
        if (index >= 0 && index < sprites.Length)
        {
            return sprites[index];
        }
        return null;
    }

    void CreateImage(Sprite sprite, float xOffset, float yOffset, int index)
    {
        GameObject imageObject = new GameObject("Image");
        imageObject.transform.position = new Vector3(xOffset, yOffset, 0); // ��ġ ����

        SpriteRenderer spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // �̹��� ũ�� ����
        imageObject.transform.localScale = new Vector3(imageSize / sprite.bounds.size.x, imageSize / sprite.bounds.size.y, 1);

        // Ŭ�� �ڵ鷯 �߰�
        ImageClickHandler clickHandler = imageObject.AddComponent<ImageClickHandler>();
        clickHandler.SetIndex(index); // �ε��� ���� ����
    }
}
