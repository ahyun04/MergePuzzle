using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject imagePrefab; // Prefab for displaying images
    public Transform gridParent;   // Parent object for the grid (could be an empty GameObject in the scene)
    public Vector2 startPosition;  // The starting position of the grid in world space
    public float cellSize = 1f;    // Size of each cell in the grid
    public float spacing = 0.1f;   // Spacing between grid cells

    private Sprite[] atlasSprites; // Loaded atlas sprites
    private int[,] TestData;       // TestData parsed from the text file

    void Start()
    {
        // Load all sprites from the atlas
        atlasSprites = Resources.LoadAll<Sprite>("Item");

        // Load and parse the text file
        TextAsset textFile = Resources.Load<TextAsset>("testdata");
        ParseTextFile(textFile.text);

        // Instantiate prefabs according to TestData
        DisplayGrid();
    }

    // Parse the text file into a 2D array
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

    // Instantiate the image prefabs in a grid pattern
    void DisplayGrid()
    {
        for (int row = 0; row < TestData.GetLength(0); row++)
        {
            for (int col = 0; col < TestData.GetLength(1); col++)
            {
                int spriteIndex = TestData[row, col] - 1; // Match 1 to index 0, 2 to 1, etc.

                if (spriteIndex >= 0 && spriteIndex < atlasSprites.Length)
                {
                    // Instantiate a new image prefab
                    GameObject newImage = Instantiate(imagePrefab, gridParent);

                    // Set the sprite to match the number from TestData
                    Sprite matchingSprite = atlasSprites[spriteIndex];
                    newImage.GetComponent<SpriteRenderer>().sprite = matchingSprite;

                    // Position the image based on row and column
                    Vector3 position = new Vector3(
                        startPosition.x + col * (cellSize + spacing),
                        startPosition.y - row * (cellSize + spacing),
                        0
                    );
                    newImage.transform.position = position;
                }
            }
        }
    }
}
