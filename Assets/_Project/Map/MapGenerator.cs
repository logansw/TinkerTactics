using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MapGenerator : MonoBehaviour
{
    private MapNode _head;
    [SerializeField] MapLocation _mapLocationPrefab;

    void Start()
    {
        GenerateNodes(3);
        DistributeMinimums();
        AssignRemaining();
        VerifyMap();
        InstantiateNodes();
        LinkMapLocations();
        _head.MapLocation.SetActive(true);
    }

    // One round is defined as a sequence of encounters terminated with a mandatory battle. Starts with a truncated round to start.
    public MapNode GenerateNodes(int rounds)
    {
        _head = new MapNode(SceneType.Battle);
        MapNode current = _head;
        for (int i = 0; i < rounds; i++)
        {
            MapNode a = current.AddNew();
            MapNode b = current.AddNew();
            MapNode c = a.AddNew();
            MapNode d = b.AddNew();
            MapNode requiredBattle = new MapNode(SceneType.Battle);
            c.Next.Add(requiredBattle);
            d.Next.Add(requiredBattle);
            current = requiredBattle;
        }
        return _head;
    }

    // Set minimum numbers of each location type
    public void DistributeMinimums()
    {
        
    }

    // Fill in remaining undefined location types
    public void AssignRemaining()
    {
        Queue<MapNode> queue = new Queue<MapNode>();
        queue.Enqueue(_head);
        while (queue.Count > 0)
        {
            MapNode current = queue.Dequeue();
            foreach (MapNode node in current.Next)
            {
                queue.Enqueue(node);
            }
            if (current.SceneType == SceneType.Base)
            {
                int random = Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        current.SceneType = SceneType.Battle;
                        break;
                    case 1:
                        current.SceneType = SceneType.TinkerShop;
                        break;
                    case 2:
                        current.SceneType = SceneType.TowerShop;
                        break;
                }
            }
        }
    }

    // Remove any unwanted patterns
    public void VerifyMap()
    {

    }

    // Instantiate MapLocation objects
    public void InstantiateNodes()
    {
        Queue<MapNode> queue = new Queue<MapNode>();
        HashSet<MapNode> seen = new HashSet<MapNode>();
        queue.Enqueue(_head);
        float x = -500;
        while (queue.Count > 0)
        {
            int nodesInLayer = queue.Count;
            x += 100;

            for (int i = 0; i < nodesInLayer; i++)
            {
                MapNode current = queue.Dequeue();

                float y = ((nodesInLayer - 1) * 100f / 2) - (100f * i);
                current.MapLocation = Instantiate(_mapLocationPrefab, transform);
                current.MapLocation.transform.localPosition = new Vector2(x, y);
                current.MapLocation.Initialize(current.SceneType);
                
                foreach (MapNode node in current.Next)
                {
                    if (seen.Contains(node))
                    {
                        continue;
                    }
                    queue.Enqueue(node);
                    seen.Add(node);
                }
            }
        }
    }

    public void LinkMapLocations()
    {
        Queue<MapNode> queue = new Queue<MapNode>();
        queue.Enqueue(_head);
        while (queue.Count > 0)
        {
            MapNode current = queue.Dequeue();

            MapLocation mapLocation = current.MapLocation;

            foreach (MapNode node in current.Next)
            {
                queue.Enqueue(node);
                Debug.Log(mapLocation == null); 
                Debug.Log(mapLocation.NextLocations == null);
                Debug.Log(current == null);
                mapLocation.NextLocations.Add(node.MapLocation);
            }
        }
    }
}

public class MapNode
{
    public SceneType SceneType;
    public List<MapNode> Next;
    public MapLocation MapLocation;

    public MapNode()
    {
        SceneType = SceneType.Base;
        Next = new List<MapNode>();
    }

    public MapNode(SceneType sceneType)
    {
        SceneType = SceneType.Base;
        SceneType = sceneType;
        Next = new List<MapNode>();
    }
    
    public MapNode AddNew()
    {
        MapNode newNode = new MapNode();
        Next.Add(newNode);
        return newNode;
    }
}