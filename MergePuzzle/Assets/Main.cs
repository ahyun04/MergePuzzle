using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform spawnTransform;

    private SpriteAtlas spriteAtlas;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    private int[,] TestData;

    void Start()
    {
        // 스프라이트 아틀라스 로드
        LoadAtlas();

        // 텍스트 파일 로드 및 파싱
        TextAsset textFile = Resources.Load<TextAsset>("testdata");
        ParseTextFile(textFile.text);

        // 이미지 생성 및 배치
        SpawnImages();
    }

    void LoadAtlas()
    {
        // Resources 폴더에 있는 스프라이트 아틀라스 로드
        spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Item");

        // 스프라이트 아틀라스의 모든 스프라이트를 딕셔너리에 추가
        Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(sprites);

        foreach (Sprite sprite in sprites)
        {
            spriteDictionary[sprite.name] = sprite;
        }
    }

    void ParseTextFile(string fileContent)
    {
        string[] lines = fileContent.Split('\n');
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length;

        TestData = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] numbers = lines[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                TestData[i, j] = int.Parse(numbers[j]);
            }
        }
    }

    void SpawnImages()
    {
        // 이미지 생성 및 배치
        for (int i = 0; i < TestData.GetLength(0); i++)
        {
            for (int j = 0; j < TestData.GetLength(1); j++)
            {
                int index = TestData[i, j] - 1; // 인덱스 조정 (1-based to 0-based)
                string spriteName = "Sprite" + (index + 1); // 스프라이트 이름 조정

                if (spriteDictionary.TryGetValue(spriteName, out Sprite sprite))
                {
                    GameObject imageObject = Instantiate(imagePrefab, spawnTransform.position, Quaternion.identity);
                    Image image = imageObject.GetComponent<Image>();
                    if (image != null)
                    {
                        image.sprite = sprite;
                    }

                    // 필요한 경우 위치 조정
                    RectTransform rt = imageObject.GetComponent<RectTransform>();
                    //rt.anchoredPosition = new Vector2(j * 100, -i * 100); // 예시 위치 조정
                }
            }
        }
    }
}
