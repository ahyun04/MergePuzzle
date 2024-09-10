using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Tile selectedTile; // ���õ� Ÿ�� ����
    public int gridWidth = 8; // �׸��� �ʺ�
    public int gridHeight = 8; // �׸��� ����
    public GameObject tilePrefab; // Ÿ�� ������
    public Sprite[] tileSprites; // Ÿ�Ͽ� ����� �̹���
    public GameObject tilesParent; // Tiles �� ������Ʈ�� ������ ����

    public float tileSpacing = 1.1f; // Ÿ�� ���� ����
    public Vector2 gridStartPosition = new Vector2(-4, -4); // �׸��尡 �����ϴ� ���� ��ǥ ��ġ

    private Tile[,] grid; // Ÿ�� �迭

    void Start()
    {
        grid = new Tile[gridWidth, gridHeight];
        InitializeGrid();
    }

    // �׸��带 �ʱ�ȭ�ϰ� Ÿ�� ����
    void InitializeGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                CreateTile(x, y);
            }
        }
    }

    // Ÿ���� �����ϰ� �׸��忡 ��ġ
    void CreateTile(int x, int y)
    {
        // Ÿ���� �׸��� ���� ��ġ�������� ������ �ΰ� ����
        Vector3 tilePosition = new Vector3(gridStartPosition.x + x * tileSpacing, gridStartPosition.y + y * tileSpacing, 0);

        // Ÿ���� tilesParent�� �ڽ����� ����
        GameObject tileObj = Instantiate(tilePrefab, tilePosition, Quaternion.identity, tilesParent.transform);

        Tile tile = tileObj.GetComponent<Tile>();
        tile.SetPosition(x, y);

        int randomType = Random.Range(0, tileSprites.Length); // ������ Ÿ�� ����
        tile.SetTileType(randomType, tileSprites[randomType]);

        grid[x, y] = tile; // �׸��忡 Ÿ�� ����
    }

    void Update()
    {
        // ���콺 Ŭ������ Ÿ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();

                if (selectedTile == null)
                {
                    // Ÿ���� ���õ��� ���� ���¶�� ����
                    selectedTile = clickedTile;
                }
                else
                {
                    // �̹� Ÿ���� ���õ� ���¶�� ��ȯ �õ�
                    SwapTiles(selectedTile, clickedTile);
                    selectedTile = null; // ���� ����
                }
            }
        }
    }

    // Ÿ�� ��ȯ
    void SwapTiles(Tile tileA, Tile tileB)
    {
        // ��ġ ��ȯ
        int tempX = tileA.x;
        int tempY = tileA.y;

        tileA.SetPosition(tileB.x, tileB.y);
        tileB.SetPosition(tempX, tempY);

        grid[tileA.x, tileA.y] = tileA;
        grid[tileB.x, tileB.y] = tileB;

        // �� Ÿ���� ������ ������ ���� �� ����
        if (tileA.tileType == tileB.tileType)
        {
            MergeTiles(tileA, tileB);
        }
    }

    // �� Ÿ���� �����ϰ� �����ϴ� �Լ�
    void MergeTiles(Tile tileA, Tile tileB)
    {
        // Ÿ���� ��Ȱ��ȭ�Ͽ� ������� ��
        Destroy(tileA.gameObject);
        Destroy(tileB.gameObject);

        // �׸��忡�� Ÿ�� ����
        grid[tileA.x, tileA.y] = null;
        grid[tileB.x, tileB.y] = null;

        // �߰� ����: ���ο� Ÿ�� ���� �Ǵ� �� ���� ä��� ������ ���� �߰� ����
    }

    // Ÿ���� �Ʒ��� ����߸��� ����
    void DropTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 1; y < gridHeight; y++) // �Ʒ����� ���� üũ
            {
                if (grid[x, y] == null) // �� ���� �߰�
                {
                    for (int aboveY = y + 1; aboveY < gridHeight; aboveY++)
                    {
                        if (grid[x, aboveY] != null) // ���ʿ� �ִ� Ÿ���� �Ʒ��� ����
                        {
                            Tile aboveTile = grid[x, aboveY];
                            grid[x, y] = aboveTile;
                            grid[x, aboveY] = null;
                            aboveTile.SetPosition(x, y); // Ÿ���� �Ʒ��� �����鼭 �ִϸ��̼� ����
                            break;
                        }
                    }
                }
            }
        }

        // �� ������ ä�������� ���ο� Ÿ���� ����
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == null) // �� ������ ���ο� Ÿ�� ����
                {
                    CreateTile(x, y);
                }
            }
        }
    }
}


