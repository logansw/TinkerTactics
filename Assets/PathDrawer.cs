using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private TilePath _tilePathPrefab;
    public static TilePath s_PathStart;

    public List<TilePath> Paths;

    void Start()
    {
        Paths = new List<TilePath>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TilePath path = Instantiate(_tilePathPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            path.transform.position = new Vector3(path.transform.position.x, path.transform.position.y, 0);
            if (Paths.Count == 0)
            {
                s_PathStart = path;
            }
            else
            {
                Paths[^1].NextTilePath = path;
            }
            Paths.Add(path);
        }
    }
}