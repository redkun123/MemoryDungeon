using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Database")]
public class CardDB : ScriptableObject
{
    [Header("List card")]

    [SerializeField] public List<Card> cardDatabase;
}
