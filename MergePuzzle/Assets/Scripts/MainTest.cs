using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MainTest : MonoBehaviour
{
    bool isDown;
    Block selectedBlock;

    public Block block00;
    public Block block01;
    public Block block10;
    public Block block11;

    Block[,] blocks = new Block[2, 2];

    private void Start()
    {
        blocks[0, 0] = block00;
        blocks[0, 1] = block01;
        blocks[1, 0] = block10;
        blocks[1, 1] = block11;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 10);

            var hit = Physics2D.Raycast(ray.origin, ray.direction, 1000);

            if (hit.collider != null)
            {
                selectedBlock = hit.collider.GetComponent<Block>();

                if (selectedBlock != null)
                {
                    // 월드 포지션 추가
                    Debug.Log($"{selectedBlock.id}, ({selectedBlock.row},{selectedBlock.col}) ====> World Pos: {selectedBlock.transform.position}");

                    isDown = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDown = false;

            // 마우스 위치를 월드 좌표로 변환
            var tpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tpos.z = 0;

            // 마우스가 위치한 지점의 모든 블록을 찾기 위해 RaycastAll 사용
            RaycastHit2D[] hits = Physics2D.RaycastAll(tpos, Vector2.zero);

            if (hits.Length > 0)
            {
                // 충돌한 블록 중 가장 아래에 있는 블록 찾기
                Block bottomBlock = null;

                foreach (var hit in hits)
                {
                    Block hitBlock = hit.collider.GetComponent<Block>();
                    if (bottomBlock == null || hitBlock.transform.position.y < bottomBlock.transform.position.y)
                    {
                        bottomBlock = hitBlock; // 가장 아래에 있는 블록 선택
                    }
                }

                if (bottomBlock != null && selectedBlock != null)
                {
                    // 블록 위치 교체
                    Vector3 selectedBlockPos = selectedBlock.transform.position;
                    selectedBlock.transform.position = bottomBlock.transform.position;
                    //bottomBlock.transform.position = selectedBlockPos;

                    // 블록 스프라이트 교체 (이미지 교환)
                    Sprite selectedSprite = selectedBlock.GetComponent<SpriteRenderer>().sprite;
                    Sprite bottomSprite = bottomBlock.GetComponent<SpriteRenderer>().sprite;

                    selectedBlock.GetComponent<SpriteRenderer>().sprite = bottomSprite;
                    bottomBlock.GetComponent<SpriteRenderer>().sprite = selectedSprite;

                    // 새 위치에 따라 row와 col 값 갱신
                    UpdateBlockPosition(selectedBlock);
                    UpdateBlockPosition(bottomBlock);

                    // blocks 배열에서 위치도 갱신
                    blocks[selectedBlock.row, selectedBlock.col] = selectedBlock;
                    blocks[bottomBlock.row, bottomBlock.col] = bottomBlock;

                    // 선택한 블록과 아래에 있던 블록의 새 위치 출력
                    Debug.Log($"{selectedBlock.id}, ({selectedBlock.row}, {selectedBlock.col}), World Pos: {selectedBlock.transform.position}");

                    this.PrintBlocks();
                }
            }
        }



        // 클릭 후 드래그 시 블록 따라 이동
        if (isDown)
        {
            var tpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tpos.z = 0;
            this.selectedBlock.transform.position = tpos;
        }
    }

    // 월드 좌표를 바탕으로 row, col 업데이트
    private void UpdateBlockPosition(Block block)
    {
        Vector3 pos = block.transform.position;
        block.row = Mathf.Max(0, Mathf.FloorToInt(pos.y) - 1);  // y축에 맞춰 row 계산
        block.col = Mathf.Max(0, Mathf.FloorToInt(pos.x));      // x축에 맞춰 col 계산
    }

    private void PrintBlocks()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                if (blocks[i, j] != null)
                {
                    sb.Append($"{blocks[i, j].id} ({blocks[i, j].row},{blocks[i, j].col}), World Pos: {blocks[i, j].transform.position}");
                    sb.Append(",     ");
                }
                else
                {
                    sb.Append($"null (empty)");
                    sb.Append(",     ");
                }
            }
            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }
}
