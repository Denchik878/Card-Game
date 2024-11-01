using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Card[] prefabs;
    public Point[] points;

    public List<Card> Spawn()
    {
        List<Card> cards = new();
        foreach(Point point in points)
        {
            if (point.currentCard != null)
                continue;
            int random = Random.Range(0, prefabs.Length);
            Card card = Instantiate(prefabs[random], point.transform.position, Quaternion.identity);
            point.currentCard = card;
            cards.Add(card);
        }
        return cards;
    }
}
