using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Generator Info")]

public class MapGeneratorInfo : ScriptableObject
{
    public int mapSizeX, mapSizeY;
    public string gameType;
}
