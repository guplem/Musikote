using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    private List<Tile> tilesInMap;

    private void Awake()
    {
        Instance = this;
        tilesInMap = new List<Tile>();
    }

    public void RegisterTile(Tile tile)
    {
        tilesInMap.Add(tile);
    }

    public void RecalculateWorldTiles()
    {
        Debug.Log("Recalculating tiles");
        foreach (Tile tile in tilesInMap)
            tile.SetupTile();
    }
    
    public void DeactivateAvaliabilityInAllTiles()
    {
        foreach (Tile tile in tilesInMap)
            tile.isPlayerCurrentlyNextToTile = false;
    }

    private void Start()
    {
        RecalculateWorldTiles();
    }
}
