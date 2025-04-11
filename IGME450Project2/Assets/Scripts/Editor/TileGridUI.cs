using UnityEngine;
using UnityEditor;
using CodiceApp;

[CustomEditor(typeof(AttackPatterns))]

public class TileGridUI : Editor
{
    private float tileSize = 20f; // Size of each button


    public override void OnInspectorGUI()
    {
        AttackPatterns patterns = (AttackPatterns)target;

        // Ensure grid size is always valid
        patterns.rows = Mathf.Max(1, EditorGUILayout.IntField("Rows", patterns.rows));
        patterns.cols = Mathf.Max(1, EditorGUILayout.IntField("Cols", patterns.cols));

        patterns.DifficultyRating = (Difficulty)EditorGUILayout.EnumPopup("Tile Type", patterns.DifficultyRating);

        patterns.connectedPatterns = (AttackPatterns)EditorGUILayout.ObjectField("Tile Type", patterns.connectedPatterns, typeof(AttackPatterns), false);

        if (GUILayout.Button("Do Something"))
        {
            Debug.Log("Button in Inspector clicked!");
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tile Grid", EditorStyles.boldLabel);

        // Make sure the grid is properly initialized before drawing
        if (patterns.tileGrid == null || patterns.tileGrid.Count != patterns.rows)
        {
            patterns.OnValidate();
        }

        // Draw the grid
        for (int i = 0; i < patterns.tileGrid.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < patterns.tileGrid[i].row.Count; j++)
            {
                Tile tile = patterns.tileGrid[i].row[j]; //  Now correctly referencing a `Tile` object

                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fixedWidth = tileSize,
                    fixedHeight = tileSize,
                    margin = new RectOffset(1, 1, 1, 1)
                };

                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = tile.isDangerous ? Color.red : Color.gray;

                if (GUILayout.Button("", buttonStyle))
                {
                    tile.isDangerous = !tile.isDangerous; //  Toggle the `isActive` property of the `Tile`
                }

                GUI.backgroundColor = originalColor; // Reset button color
            }
            EditorGUILayout.EndHorizontal();
        }

        // Ensure changes are saved
        if (GUI.changed)
        {
            EditorUtility.SetDirty(patterns);
        }
    }
}
