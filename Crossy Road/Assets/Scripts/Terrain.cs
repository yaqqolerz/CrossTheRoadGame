using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    protected int horizontalSize;

    public virtual void Generate(int size)
    {
        horizontalSize = size;
        if (size == 0)
        {
            return;
        }
        if ((float) size % 2 == 0)
        {
            size -= 1;
        }
        int movinglimit = Mathf.FloorToInt((float) size / 2); 
        for(int i = -movinglimit; i <= movinglimit; i++)
        {
            SpawnTile(i);
        }
        for (int x = 1; x < 6; x++)
        {
            var leftBoundaryTile = SpawnTile(-movinglimit - x);
            var rightBoundaryTile = SpawnTile(movinglimit + x);
            DarkenObject(leftBoundaryTile);
            DarkenObject(rightBoundaryTile);
        }
        
    }

    private GameObject SpawnTile(int xPos)
    {
        var go = Instantiate(tilePrefab, transform);
        go.transform.localPosition = new Vector3(xPos, 0, 0);
        return go;
    }

    private void DarkenObject(GameObject go)
    {
        var renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        foreach(var rend in renderers)
        {
            rend.material.color *= Color.grey; 
        }

    }
}
