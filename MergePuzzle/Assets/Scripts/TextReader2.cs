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
    public float imageSize = 1f; // 이미지 크기 (스프라이트의 월드 스케일에 영향을 줌)

    private Sprite[] sprites; // 스프라이트 배열을 정의

    void Start()
    {
        // 텍스트 파일을 읽어옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("testdata");
        if (textAsset != null)
        {
            string fileContent = textAsset.text;
            LoadSprites(); // 스프라이트를 로드
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
                        CreateImage(sprite, xOffset, -currentPosition.y, imageIndex); // 인덱스 정보 전달
                        xOffset += imageSize; // 이미지 사이의 간격 조정
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

            currentPosition.y -= imageSize; // 줄 사이의 간격 조정
            currentPosition.x = startPosition.x; // 새로운 줄의 시작 위치로 초기화
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
        imageObject.transform.position = new Vector3(xOffset, yOffset, 0); // 위치 설정

        SpriteRenderer spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // 이미지 크기 조정
        imageObject.transform.localScale = new Vector3(imageSize / sprite.bounds.size.x, imageSize / sprite.bounds.size.y, 1);

        // 클릭 핸들러 추가
        ImageClickHandler clickHandler = imageObject.AddComponent<ImageClickHandler>();
        clickHandler.SetIndex(index); // 인덱스 정보 설정
    }
}
