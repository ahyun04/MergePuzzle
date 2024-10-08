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
                    // ���� ������ �߰�
                    Debug.Log($"{selectedBlock.id}, ({selectedBlock.row},{selectedBlock.col}) ====> World Pos: {selectedBlock.transform.position}");

                    isDown = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDown = false;

            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            var tpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tpos.z = 0;

            // ���콺�� ��ġ�� ������ ��� ����� ã�� ���� RaycastAll ���
            RaycastHit2D[] hits = Physics2D.RaycastAll(tpos, Vector2.zero);

            if (hits.Length > 0)
            {
                // �浹�� ��� �� ���� �Ʒ��� �ִ� ��� ã��
                Block bottomBlock = null;

                foreach (var hit in hits)
                {
                    Block hitBlock = hit.collider.GetComponent<Block>();
                    if (bottomBlock == null || hitBlock.transform.position.y < bottomBlock.transform.position.y)
                    {
                        bottomBlock = hitBlock; // ���� �Ʒ��� �ִ� ��� ����
                    }
                }

                if (bottomBlock != null && selectedBlock != null)
                {
                    // ��� ��ġ ��ü
                    Vector3 selectedBlockPos = selectedBlock.transform.position;
                    selectedBlock.transform.position = bottomBlock.transform.position;
                    //bottomBlock.transform.position = selectedBlockPos;

                    // ��� ��������Ʈ ��ü (�̹��� ��ȯ)
                    Sprite selectedSprite = selectedBlock.GetComponent<SpriteRenderer>().sprite;
                    Sprite bottomSprite = bottomBlock.GetComponent<SpriteRenderer>().sprite;

                    selectedBlock.GetComponent<SpriteRenderer>().sprite = bottomSprite;
                    bottomBlock.GetComponent<SpriteRenderer>().sprite = selectedSprite;

                    // �� ��ġ�� ���� row�� col �� ����
                    UpdateBlockPosition(selectedBlock);
                    UpdateBlockPosition(bottomBlock);

                    // blocks �迭���� ��ġ�� ����
                    blocks[selectedBlock.row, selectedBlock.col] = selectedBlock;
                    blocks[bottomBlock.row, bottomBlock.col] = bottomBlock;

                    // ������ ��ϰ� �Ʒ��� �ִ� ����� �� ��ġ ���
                    Debug.Log($"{selectedBlock.id}, ({selectedBlock.row}, {selectedBlock.col}), World Pos: {selectedBlock.transform.position}");

                    this.PrintBlocks();
                }
            }
        }



        // Ŭ�� �� �巡�� �� ��� ���� �̵�
        if (isDown)
        {
            var tpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tpos.z = 0;
            this.selectedBlock.transform.position = tpos;
        }
    }

    // ���� ��ǥ�� �������� row, col ������Ʈ
    private void UpdateBlockPosition(Block block)
    {
        Vector3 pos = block.transform.position;
        block.row = Mathf.Max(0, Mathf.FloorToInt(pos.y) - 1);  // y�࿡ ���� row ���
        block.col = Mathf.Max(0, Mathf.FloorToInt(pos.x));      // x�࿡ ���� col ���
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
