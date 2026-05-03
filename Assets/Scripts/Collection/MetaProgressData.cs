using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MetaProgressData
{
    public List<string> completedRoomsFirstTime = new();
    public List<string> unlockedPieces = new();

    public int maxFloorUnlocked = 1;
    public bool finalBossUnlocked = false;

    private HashSet<string> _completedSet;
    private HashSet<string> _pieceSet;

    public void InitRuntime()
    {
        _completedSet = new HashSet<string>(completedRoomsFirstTime);
        _pieceSet = new HashSet<string>(unlockedPieces);
    }

    public bool IsRoomCompleted(string roomId) => _completedSet.Contains(roomId);
    public bool IsPieceUnlocked(string pieceId) => _pieceSet.Contains(pieceId);

    public bool AddCompletedRoom(string roomId)
    {
        if (_completedSet.Add(roomId))
        {
            completedRoomsFirstTime.Add(roomId);
            return true;
        }
        return false;
    }

    public bool AddPiece(string pieceId)
    {
        if (_pieceSet.Add(pieceId))
        {
            unlockedPieces.Add(pieceId);
            return true;
        }
        return false;
    }
}
