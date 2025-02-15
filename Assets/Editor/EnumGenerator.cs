using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using KayosStudios.TBD.SpawnSystem;

public class EnumGenerator : Editor
{
    private const string directoryPath = "Assets/PROJECT/Scripts/SpawnSystem";
    private const string enumFilePath = "Assets/PROJECT/Scripts/SpawnSystem/SpawnableType.cs"; // Path for the generated file

    [MenuItem("Tools/Generate SpawnableType Enum")]
    public static void GenerateEnum()
    {
        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log("Created missing directory: " + directoryPath);
        }

        SpawnManager spawnManager = FindFirstObjectByType<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager not found in the scene! Please add one before generating the enum.");
            return;
        }

        List<GameObject> spawnablePrefabs = spawnManager.GetSpawnablePrefabs();
        if (spawnablePrefabs == null || spawnablePrefabs.Count == 0)
        {
            Debug.LogWarning("No spawnable prefabs found in SpawnManager!");
            return;
        }

        List<string> enumEntries = new List<string> { "None" }; // Default entry

        foreach (GameObject prefab in spawnablePrefabs)
        {
            if (prefab != null)
            {
                string enumName = prefab.name.Replace(" ", "").Replace("-", "").Replace(".", ""); // Ensure valid enum name
                if (!enumEntries.Contains(enumName))
                {
                    enumEntries.Add(enumName);
                }
            }
        }

        // Generate the enum file content with [System.Serializable]
        string enumContent =
$@"// This file is auto-generated. Do not edit manually.
using System;

[Serializable]
public enum SpawnableType
{{";
        foreach (string entry in enumEntries)
        {
            enumContent += $"\n\t{entry},";
        }
        enumContent += "\n}";

        // Write to file
        File.WriteAllText(enumFilePath, enumContent);
        AssetDatabase.Refresh();

        Debug.Log("SpawnableType enum successfully generated at: " + enumFilePath);
    }
}
