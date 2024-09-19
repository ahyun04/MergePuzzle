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
        // ��������Ʈ ��Ʋ�� �ε�
        LoadAtlas();

        // �ؽ�Ʈ ���� �ε� �� �Ľ�
        TextAsset textFile = Resources.Load<TextAsset>("testdata");
        ParseTextFile(textFile.text);

        // �̹��� ���� �� ��ġ
        SpawnImages();
    }

    void LoadAtlas()
    {
        // Resources ������ �ִ� ��������Ʈ ��Ʋ�� �ε�
        spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Item");

        // ��������Ʈ ��Ʋ���� ��� ��������Ʈ�� ��ųʸ��� �߰�
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
        // �̹��� ���� �� ��ġ
        for (int i = 0; i < TestData.GetLength(0); i++)
        {
            for (int j = 0; j < TestData.GetLength(1); j++)
            {
                int index = TestData[i, j] - 1; // �ε��� ���� (1-based to 0-based)
                string spriteName = "Sprite" + (index + 1); // ��������Ʈ �̸� ����

                if (spriteDictionary.TryGetValue(spriteName, out Sprite sprite))
                {
                    GameObject imageObject = Instantiate(imagePrefab, spawnTransform.position, Quaternion.identity);
                    Image image = imageObject.GetComponent<Image>();
                    if (image != null)
                    {
                        image.sprite = sprite;
                    }

                    // �ʿ��� ��� ��ġ ����
                    RectTransform rt = imageObject.GetComponent<RectTransform>();
                    //rt.anchoredPosition = new Vector2(j * 100, -i * 100); // ���� ��ġ ����
                }
            }
        }
    }
}
