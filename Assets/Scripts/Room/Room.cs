using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject
{
    [SerializeField] public string roomID;
    [SerializeField] public string roomName;
    [SerializeField] public PuzzlePieceData puzzlePiece;
    public virtual RoomType roomType => RoomType.None;
    public enum RoomType
    {
        None,
        Rest,
        Shop,
        Story,
        Battle,
        Boss
    }
}
