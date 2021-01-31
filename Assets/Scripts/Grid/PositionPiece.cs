using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPiece : MonoBehaviour
{
    [System.Serializable]
    public class VectorStored
    {
        public List<int> column;
    }

    public List<SpriteRenderer> graphics;
    public List<VectorStored> matrix = new List<VectorStored>();
    public List<(int, int)> positions = new List<(int, int)>();

    public GridCell snappedTo;
    public bool isSnapped;

    bool selected;

    // Update is called once per frame
    void Update()
    {
        if (isSnapped)
        {
            Vector2 mousePOsition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePOsition.x, mousePOsition.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0) && selected)
        {
            if (!snappedTo)
                isSnapped = true;
            else
            {
                Debug.Log("Snap1");
                isSnapped = true;
                Grid.grid.unSnapToGrid(this, positions);
            }
        }

        else if (Input.GetMouseButtonDown(1) && isSnapped)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Cell"));
            if (hit.collider != null && hit.transform.GetComponent<GridCell>())
            {
                Debug.Log("Snap2");
                Grid.grid.SnapPieceToGrid(hit.transform.GetComponent<GridCell>(), this);
            }

            else
            {
                isSnapped = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        selected = true;
        foreach (SpriteRenderer s in graphics)
            s.color = Color.blue;
    }

    private void OnMouseExit()
    {
        selected = false;
        foreach (SpriteRenderer s in graphics)
            s.color = Color.red;
    }
}
