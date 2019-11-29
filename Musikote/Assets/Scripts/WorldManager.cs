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
        foreach (Tile tile in tilesInMap)
            tile.SetupTile();
    }

    private void Start()
    {
        RecalculateWorldTiles();
    }
}
