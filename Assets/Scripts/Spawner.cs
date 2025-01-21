using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public CardWeight[] prefabs;
    public Point[] points;

    public List<Card> Spawn()
    {
        List<Card> cards = new();
        foreach(Point point in points)
        {
            if (point.currentCard != null)
                continue;
            int random = Random.Range(0, prefabs.Length);
            Card card = Instantiate(prefabs[random].card, point.transform.position, Quaternion.identity);
            point.currentCard = card;
            card.currentPoint = point;
            cards.Add(card);
        }
        return cards;
    }
}
[Serializable]
public class CardWeight
{
    public Card card;
    public int weight;
}