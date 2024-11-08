using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Point[] points;
    private void Start()
    {
        points = GetComponentsInChildren<Point>();
    }
    public void CreateWeapon(Card weapon)
    {
        foreach (Point point in points)
        {
            if (!point.currentCard)
            {
                point.currentCard = Instantiate(weapon, point.transform.position, Quaternion.identity);
            }
        }
    }
}