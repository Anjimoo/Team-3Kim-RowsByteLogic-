using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public TileTypes[] tileTypes;

    public int[,] tiles;

    public MapGeneratorInfo mapGeneratorInfo;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new int[mapGeneratorInfo.mapSizeX, mapGeneratorInfo.mapSizeY];
        System.Random rn = new System.Random();
        if (mapGeneratorInfo.gameType.CompareTo("novice") == 0)
        {
            for (int x = 0; x < mapGeneratorInfo.mapSizeX; x++)
            {
                for (int y = 0; y < mapGeneratorInfo.mapSizeY; y++)
                {
                    tiles[x, y] = 0; //Sets all tiles' materials to default material.
                }
            }
        }
        else if(mapGeneratorInfo.gameType.CompareTo("easy") == 0)
        {
            for (int x = 0; x < mapGeneratorInfo.mapSizeX; x++)
            {
                for (int y = 0; y < mapGeneratorInfo.mapSizeY; y++)
                {
                    tiles[x, y] = rn.Next(2,4); //randomize a material for each tile, according to TileTypes definition, e.g. 2=DryGroundPrefab, 1=LavaPrefab..
                }
            }

        }
        else if(mapGeneratorInfo.gameType.CompareTo("hard") == 0)
        {
            //generates the same materials as Easy mode for the whole map
            for (int x = 0; x < mapGeneratorInfo.mapSizeX; x++)
            {
                for (int y = 0; y < mapGeneratorInfo.mapSizeY; y++)
                {
                    tiles[x, y] = rn.Next(2, 4); //randomize a material for each tile, according to TileTypes definition, e.g. 2=DryGroundPrefab, 1=LavaPrefab..
                }
            }

            //the second loop generates the unwalkable after the units spawn points, to prevent them from spawning on unwalkable tiles.
            for (int x = 4; x < (mapGeneratorInfo.mapSizeX)-4; x++)
            {
                for (int y = 1; y < (mapGeneratorInfo.mapSizeY-1); y++)
                {
                    tiles[x, y] = rn.Next(1, 4); //randomize a material for each tile, according to TileTypes definition, e.g. 2=DryGroundPrefab, 1=LavaPrefab..
                }
            }
        }
        GenerateMapVisuals();
    }
    
    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapGeneratorInfo.mapSizeX; x++)
        {
            for (int y = 0; y < mapGeneratorInfo.mapSizeY; y++)
            {
                TileTypes tt = tileTypes[tiles[x, y]];
                Instantiate(tt.tilePreFab, new Vector3(x, 0, y), new Quaternion(0,0,180,0));

            }
        }
        //UnitsHandler uh = new UnitsHandler();
        //uh.GenerateUnits(mapSizeX, mapSizeY);
    }
}