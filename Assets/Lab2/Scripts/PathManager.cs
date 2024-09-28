using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector] 
    [SerializeField] public List<Waypoint> Path;

    public GameObject prefab;
    int currentPointIndex = 0;
    public List<GameObject> prefabPoints;


    public Waypoint GetNextTarget()
    {
        int nextPointIndex = (currentPointIndex + 1) % (Path.Count);
        currentPointIndex = nextPointIndex;
        return Path[nextPointIndex];
    }
    public List<Waypoint> GetPath()
    {
        if (Path == null)
            Path = new List<Waypoint>();
        return Path;
    }

    public void CreatAddPoints()
    {
        Waypoint go = new Waypoint();
        Path.Add(go);
    }

    void Start()
    {
        prefabPoints = new List<GameObject>();
        foreach (Waypoint p in Path)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = p.pos;
            prefabPoints.Add(go);
        }
    }

    void Update()
    {
        for (int i = 0; i < Path.Count; i++)
        {
            Waypoint p = Path[i];
            GameObject g = prefabPoints[i];
            g.transform.position = p.pos;
        }
    }
}
