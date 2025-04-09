using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackPatterns", menuName = "Scriptable Objects/AttackPatterns")]

public class AttackPatterns : ScriptableObject 
{
    

    public Difficulty DifficultyRating;

    public int rows = 9;
    public int cols = 9;

    [SerializeField] public List<TileRow> tileGrid = new List<TileRow>(); //  List of `TileRow`, NOT `List<List<bool>>`

    [SerializeField] public AttackPatterns connectedPatterns;

    public void OnValidate()
    {
        // Ensure the list has the correct number of rows
        while (tileGrid.Count < rows)
            tileGrid.Add(new TileRow());

        while (tileGrid.Count > rows)
            tileGrid.RemoveAt(tileGrid.Count - 1);

        // Ensure each row has the correct number of columns
        foreach (var row in tileGrid)
        {
            while (row.row.Count < cols)
                row.row.Add(new Tile()); //  Add `Tile`, NOT `bool`

            while (row.row.Count > cols)
                row.row.RemoveAt(row.row.Count - 1);
        }
    }
}

[System.Serializable]
public class Tile
{
    public bool isDangerous; // Boolean value inside the Tile object
}

[System.Serializable]
public class TileRow
{
    public List<Tile> row = new List<Tile>(); // List of Tiles, NOT booleans
}

