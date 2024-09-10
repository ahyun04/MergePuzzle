using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Tile selectedTile; // 선택된 타일 저장
    public int gridWidth = 8; // 그리드 너비
    public int gridHeight = 8; // 그리드 높이
    public GameObject tilePrefab; // 타일 프리팹
    public Sprite[] tileSprites; // 타일에 사용할 이미지
    public GameObject tilesParent; // Tiles 빈 오브젝트를 참조할 변수

    public float tileSpacing = 1.1f; // 타일 간의 간격
    public Vector2 gridStartPosition = new Vector2(-4, -4); // 그리드가 시작하는 월드 좌표 위치

    private Tile[,] grid; // 타일 배열

    void Start()
    {
        grid = new Tile[gridWidth, gridHeight];
        InitializeGrid();
    }

    // 그리드를 초기화하고 타일 생성
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

    // 타일을 생성하고 그리드에 배치
    void CreateTile(int x, int y)
    {
        // 타일을 그리드 시작 위치에서부터 간격을 두고 생성
        Vector3 tilePosition = new Vector3(gridStartPosition.x + x * tileSpacing, gridStartPosition.y + y * tileSpacing, 0);

        // 타일을 tilesParent의 자식으로 생성
        GameObject tileObj = Instantiate(tilePrefab, tilePosition, Quaternion.identity, tilesParent.transform);

        Tile tile = tileObj.GetComponent<Tile>();
        tile.SetPosition(x, y);

        int randomType = Random.Range(0, tileSprites.Length); // 랜덤한 타일 종류
        tile.SetTileType(randomType, tileSprites[randomType]);

        grid[x, y] = tile; // 그리드에 타일 저장
    }

    void Update()
    {
        // 마우스 클릭으로 타일 선택
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();

                if (selectedTile == null)
                {
                    // 타일이 선택되지 않은 상태라면 선택
                    selectedTile = clickedTile;
                }
                else
                {
                    // 이미 타일이 선택된 상태라면 교환 시도
                    SwapTiles(selectedTile, clickedTile);
                    selectedTile = null; // 선택 해제
                }
            }
        }
    }

    // 타일 교환
    void SwapTiles(Tile tileA, Tile tileB)
    {
        // 위치 교환
        int tempX = tileA.x;
        int tempY = tileA.y;

        tileA.SetPosition(tileB.x, tileB.y);
        tileB.SetPosition(tempX, tempY);

        grid[tileA.x, tileA.y] = tileA;
        grid[tileB.x, tileB.y] = tileB;

        // 두 타일의 종류가 같으면 머지 및 삭제
        if (tileA.tileType == tileB.tileType)
        {
            MergeTiles(tileA, tileB);
        }
    }

    // 두 타일을 머지하고 삭제하는 함수
    void MergeTiles(Tile tileA, Tile tileB)
    {
        // 타일을 비활성화하여 사라지게 함
        Destroy(tileA.gameObject);
        Destroy(tileB.gameObject);

        // 그리드에서 타일 제거
        grid[tileA.x, tileA.y] = null;
        grid[tileB.x, tileB.y] = null;

        // 추가 로직: 새로운 타일 생성 또는 빈 공간 채우기 로직을 여기 추가 가능
    }

    // 타일을 아래로 떨어뜨리는 로직
    void DropTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 1; y < gridHeight; y++) // 아래에서 위로 체크
            {
                if (grid[x, y] == null) // 빈 공간 발견
                {
                    for (int aboveY = y + 1; aboveY < gridHeight; aboveY++)
                    {
                        if (grid[x, aboveY] != null) // 위쪽에 있는 타일을 아래로 내림
                        {
                            Tile aboveTile = grid[x, aboveY];
                            grid[x, y] = aboveTile;
                            grid[x, aboveY] = null;
                            aboveTile.SetPosition(x, y); // 타일을 아래로 내리면서 애니메이션 포함
                            break;
                        }
                    }
                }
            }
        }

        // 빈 공간이 채워졌으니 새로운 타일을 생성
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == null) // 빈 공간에 새로운 타일 생성
                {
                    CreateTile(x, y);
                }
            }
        }
    }
}


