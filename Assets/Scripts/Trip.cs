using System.Collections.Generic;
using UnityEngine;

public class Trip : MonoBehaviour
{
    [SerializeField] private List<GameObject> points;
    
    public GameObject GetNewPoint(int currentIndex)
    {
        var index = currentIndex + 1;
        if (index > points.Count-1)
            index = 0;
        return points[index];
    }
    public GameObject GetStartPoint()
    {
        return points[0];
    }

    public int Count()
    {
        return points.Count;
    }
}