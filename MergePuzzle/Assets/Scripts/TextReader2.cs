using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader2 : MonoBehaviour
{
    public enum ImageType
    {
        Jelly = 1,
        Pudding = 2,
        Star = 3
    }

    public string filePath = "Assets/Resources/testdata.txt";
    public Vector2 startPosition = new Vector2(0, 0);
    public float imageSize = 1f; 

    private Sprite[] sprites; 

    void Start()
    {
        // 텍스트 파일
        TextAsset textAsset = Resources.Load<TextAsset>("testdata");
        if (textAsset != null)
        {
            string fileContent = textAsset.text;
            LoadSprites(); 
            DisplayImagesFromText(fileContent);
        }
        else
        {
            Debug.LogError("Failed to load text file.");
        }
    }

    void LoadSprites()
    {
        // 스프라이트를 미리 로드
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
                        CreateImage(sprite, xOffset, -currentPosition.y, imageIndex); 
                        xOffset += imageSize;
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

            currentPosition.y -= imageSize; 
            currentPosition.x = startPosition.x; 
        }
    }

    Sprite GetSpriteFromEnum(ImageType imageType)
    {
        int index = (int)imageType - 1; 
        if (index >= 0 && index < sprites.Length)
        {
            return sprites[index];
        }
        return null;
    }

    void CreateImage(Sprite sprite, float xOffset, float yOffset, int index)
    {
        GameObject imageObject = new GameObject("Item");
        imageObject.transform.position = new Vector3(xOffset, yOffset, 0);

        SpriteRenderer spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        imageObject.transform.localScale = new Vector3(imageSize / sprite.bounds.size.x, imageSize / sprite.bounds.size.y, 1);

        ImageClickHandler clickHandler = imageObject.AddComponent<ImageClickHandler>();
        //clickHandler.SetIndex(index);
    }
}
