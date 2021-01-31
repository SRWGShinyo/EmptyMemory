using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid grid;

    int[,] grid_matrix = new int[5,5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, 
                                        { 0, 0, 0, 0, 0 } };

    private void Awake()
    {
        if (grid == null)
            grid = this;
        else
            Destroy(grid.gameObject);
    }


    public void SnapPieceToGrid(GridCell cell, PositionPiece piece)
    {

        List<(int, int)> positionsToOccupy = new List<(int, int)>();
        (int, int) positions = (cell.line, cell.column);
        for (int i = 0; i < piece.matrix.Count; i++)
        {
            positions.Item1 += i;
            for (int j = 0; j < piece.matrix[i].column.Count; j++)
            {
                positions.Item2 += j;
                if (positions.Item1 >= 5 || positions.Item2 >= 5 || grid_matrix[positions.Item1, positions.Item2] == 1)
                {
                    Debug.Log(grid_matrix[positions.Item1, positions.Item2]);
                    Debug.Log("Cell is nope " + positions.Item1 + " " + positions.Item2);
                    return;
                }

                Debug.Log(" " + positions + " " + i + " " + j);
                positionsToOccupy.Add((positions.Item1, positions.Item2));
            }


            positions.Item2 = cell.column;
        }

        piece.isSnapped = false;
        piece.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, piece.transform.position.z);
        piece.snappedTo = cell;
        SetPositionsToGrid(positionsToOccupy, 1);
        piece.positions = positionsToOccupy;

        Debug.Log("Piece is snappedTo" + positionsToOccupy[0] + positionsToOccupy[1]);
    }

    public void unSnapToGrid(PositionPiece piece, List<(int, int)> positions)
    {
        piece.snappedTo = null;
        piece.positions = null;
        SetPositionsToGrid(positions, 0);
    }

    public void SetPositionsToGrid(List<(int, int)> positions, int value)
    {
        foreach ((int, int) pos in positions)
            grid_matrix[pos.Item1, pos.Item2] = value;
    }
}
