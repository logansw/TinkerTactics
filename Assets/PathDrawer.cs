using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private TilePath _tilePathPrefab;
    public static TilePath s_PathStart;
    public static bool Drawn;
    private Vector2[] _positions = {    new Vector2(-11.48f, 7.39f),
                                        new Vector2(-10.77f, 6.94f), 
                                        new Vector2(-9.79f, 6.35f),
                                        new Vector2(-8.30f, 5.61f), 
                                        new Vector2(-7.06f, 5.68f), 
                                        new Vector2(-5.30f, 6.78f), 
                                        new Vector2(-3.61f, 7.99f), 
                                        new Vector2(-0.64f, 8.54f), 
                                        new Vector2(0.71f, 7.89f), 
                                        new Vector2(0.380444139f,6.49133348f),
                                        new Vector2(-0.784667015f,5.30244446f),
                                        new Vector2(-1.92600083f,4.42266655f),
                                        new Vector2(-3.87577772f,2.71066642f),
                                        new Vector2(-4.70800066f,0.784666479f),
                                        new Vector2(-5.20733356f,-1.33155537f),
                                        new Vector2(-4.73177814f,-2.80577779f),
                                        new Vector2(-2.71066642f,-4.01844454f),
                                        new Vector2(-0.166444644f,-4.25622225f),
                                        new Vector2(1.09377754f,-3.73311138f),
                                        new Vector2(2.44911218f,-2.54422212f),
                                        new Vector2(2.94844604f,-0.927333534f),
                                        new Vector2(2.52044439f,0.451778233f),
                                        new Vector2(1.56933403f,0.808445096f),
                                        new Vector2(0.618221045f,0.61822235f),
                                        new Vector2(-0.0475561954f,-0.023777842f),
                                        };

    public List<TilePath> Paths;

    void Start()
    {
        Paths = new List<TilePath>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Drawn)
        {
            foreach (Vector2 position in _positions)
            {
                TilePath path = Instantiate(_tilePathPrefab, position, Quaternion.identity);
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
            Drawn = true;
        }
    }
}