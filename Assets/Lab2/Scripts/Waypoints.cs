using UnityEngine;

[System.Serializable]
public class Waypoint
{
    [SerializeField] public Vector3 pos = new(0, 0, 0);

    public void SetPos(Vector3 newPos)
    {
        pos = newPos;
    }

    public Vector3 GetPos()
    {
        return pos;
    }
}