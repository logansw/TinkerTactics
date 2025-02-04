using UnityEngine;
using UnityEditor;

public class SpritePostprocessor : AssetPostprocessor
{
    // Desired default Pixels Per Unit
    private const float DefaultPixelsPerUnit = 128f;

    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;

        // Check if the texture is set to Sprite
        if (importer.textureType == TextureImporterType.Sprite)
        {
            // Set the default Pixels Per Unit
            importer.spritePixelsPerUnit = DefaultPixelsPerUnit;

            // Optional: Adjust other import settings if needed
            // importer.mipmapEnabled = false;
            // importer.filterMode = FilterMode.Point;
            // importer.wrapMode = TextureWrapMode.Clamp;
        }
    }

    // Optional: Provide a menu item to reset all sprites' PPU to the default
    [MenuItem("Tools/Reset All Sprites PPU")]
    static void ResetAllSpritesPPU()
    {
        string[] spritePaths = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });
        foreach (string guid in spritePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer != null && importer.textureType == TextureImporterType.Sprite)
            {
                importer.spritePixelsPerUnit = DefaultPixelsPerUnit;
                importer.SaveAndReimport();
            }
        }

        Debug.Log("All sprite Pixels Per Unit values have been reset to " + DefaultPixelsPerUnit);
    }
}
