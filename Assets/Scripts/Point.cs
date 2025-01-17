using System.Linq;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Point[] points;
    public int length;
    public int index;
    public Card currentCard;
    private void Awake()
    {
        points = FindObjectsByType<Point>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public Point GetPointByIndex(int index)
    {
        return points.FirstOrDefault(point => point.index == index);
    }
}
