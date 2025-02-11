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
            Card card = Instantiate(GetRandomCard(), point.transform.position, Quaternion.identity);
            point.currentCard = card;
            card.currentPoint = point;
            cards.Add(card);
        }
        return cards;
    }
    private Card GetRandomCard()
    {
        int totalWeight = 0;
        foreach (CardWeight weight in prefabs)
        {
            totalWeight += weight.weight;
        }
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (CardWeight weight in prefabs)
        {
            currentWeight += weight.weight;
            if (currentWeight >= randomWeight)
            {
                return weight.card;
            }
        }
        return null;
    }
}
[Serializable]
public class CardWeight
{
    public Card card;
    public int weight;
}