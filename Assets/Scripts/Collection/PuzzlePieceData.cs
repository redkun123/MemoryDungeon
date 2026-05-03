using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Puzzle Piece Data")]
public class PuzzlePieceData : ScriptableObject
{
    [Header("Identity")]
    public string pieceId;

    [Header("Visual")]
    public Sprite sprite;

    [Tooltip("Location on board (UI)")]
    public Vector2 anchoredPosition;

    [Tooltip("Optional scale")]
    public Vector2 scale = Vector2.one;
}
