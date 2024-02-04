using UnityEngine;
using UnityEditor;
using System.IO;

public class WeaponDataEditor : EditorWindow
{
    [MenuItem("Tools/Update WeaponData Hitboxes")]
    public static void UpdateWeaponDataHitboxes()
    {
        // Find all WeaponData assets in the project
        string[] guids = AssetDatabase.FindAssets("t:WeaponData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponData weaponData = AssetDatabase.LoadAssetAtPath<WeaponData>(path);

            if (weaponData != null)
            {
                // Modify hitboxOffset based on enemyHitboxOffset
                weaponData.hitboxOffset.y = weaponData.enemyHitboxOffset.x; 
                weaponData.hitboxOffset.x = weaponData.enemyHitboxOffset.y;

                weaponData.hitboxSize.y = weaponData.enemyHitboxSize.x;
                weaponData.hitboxSize.x = weaponData.enemyHitboxSize.y;

                // Save changes to the asset
                EditorUtility.SetDirty(weaponData);
                AssetDatabase.SaveAssets();
            }
        }

        Debug.Log("WeaponData hitboxes updated.");
    }
}
