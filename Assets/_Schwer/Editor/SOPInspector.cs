using System.Collections.Generic;
using System.Linq;
using Schwer.ItemSystem;
using Schwer.Reflection;
using SchwerEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableObjectPersistence))]
public class SOPInspector : Editor {
    public override void OnInspectorGUI() {
        var sop = (ScriptableObjectPersistence)target;
        if (GUILayout.Button("Refresh References")) {
            RefreshReferences(sop);
        }
        GUILayout.Space(5);
        base.OnInspectorGUI();
    }

    private static void RefreshReferences(ScriptableObjectPersistence sop) {
        Undo.RecordObject(sop, "Refresh Scriptable Object References");

        var itemDB = AssetsUtility.FindFirstAsset<ItemDatabase>("");
        ReflectionUtility.SetPrivateField(sop, "_itemDatabase", itemDB);

        var skinTexDBs = AssetsUtility.FindAllAssets<SkinTexturesDatabase>();
        ReflectionUtility.SetPrivateField(sop, "_skinTexturesDatabases", skinTexDBs);

        var name = AssetsUtility.FindFirstAsset<StringValue>("Save");
        ReflectionUtility.SetPrivateField(sop, "_saveName", name);

        var pos = AssetsUtility.FindFirstAsset<VectorValue>("Player");
        ReflectionUtility.SetPrivateField(sop, "_playerPosition", pos);

        var health = AssetsUtility.FindFirstAsset<ConstrainedFloat>("Health");
        ReflectionUtility.SetPrivateField(sop, "_health", health);

        var mana = AssetsUtility.FindFirstAsset<ConstrainedFloat>("Mana");
        ReflectionUtility.SetPrivateField(sop, "_mana", mana);

        var lumen = AssetsUtility.FindFirstAsset<ConstrainedFloat>("Lumen");
        ReflectionUtility.SetPrivateField(sop, "_lumen", lumen);

        var xp = AssetsUtility.FindFirstAsset<XPSystem>("Player");
        ReflectionUtility.SetPrivateField(sop, "_xpSystem", xp);

        var time = AssetsUtility.FindFirstAsset<FloatValue>("TimeOfDay");
        ReflectionUtility.SetPrivateField(sop, "_timeOfDay", time);

        var appearance = AssetsUtility.FindFirstAsset<CharacterAppearance>("Player");
        ReflectionUtility.SetPrivateField(sop, "_characterAppearance", appearance);

        var inv = AssetsUtility.FindFirstAsset<Inventory>("Player");
        ReflectionUtility.SetPrivateField(sop, "_playerInventory", inv);

        var bools = AssetsUtility.FindAllAssets<BoolValue>();
        var strings = AssetsUtility.FindAllAssets<StringValue>();
        var chests = new List<BoolValue>();
        var doors = new List<BoolValue>();
        var bosses = new List<BoolValue>();
        var healthCrystals = new List<BoolValue>();
        var manaCrystals = new List<BoolValue>();
        var cutscenes = new List<BoolValue>();

        for (int i = 0; i < bools.Length; i++) {
            var path = AssetDatabase.GetAssetPath(bools[i]);
            if (path.Contains("Chest")) {
                chests.Add(bools[i]);
            }
            else if (path.Contains("Doors")) {
                doors.Add(bools[i]);
            }
            else if (path.Contains("Bosses"))
            {
                bosses.Add(bools[i]);
            }
            else if (path.Contains("HealthCrystals"))
            {
                healthCrystals.Add(bools[i]);
            }
            else if (path.Contains("ManaCrystals"))
            {
                manaCrystals.Add(bools[i]);
            }
            else if (path.Contains("Cutscenes"))
            {
                cutscenes.Add(bools[i]);
            }
        }

        ReflectionUtility.SetPrivateField(sop, "_chests", chests.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_doors", doors.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_bosses", bosses.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_healthCrystals", healthCrystals.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_manaCrystals", manaCrystals.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_cutscenes", cutscenes.ToArray());

        var inventories = AssetsUtility.FindAllAssets<Inventory>();
        var vendorInventories = new List<ScriptableObjectPersistence.VendorInventorySet>();
        for (int i = 0; i < inventories.Length; i++) {
            var path = AssetDatabase.GetAssetPath(inventories[i]);
            if (path.Contains("Vendor") && !path.Contains("Default")) {
                var search = path.Split('/').Last();        // Remove anything before the last `/` in the path (i.e. folder names).
                search = search.Split('.').First();         // Remove file extension.
                search = search.Replace("Vendor", "");
                search = search.Replace("Inventory", "");
                var initial = AssetsUtility.FindFirstAsset<Inventory>(search);

                // Only accept inventories with "Default" in the path.
                if (initial != null && !AssetDatabase.GetAssetPath(initial).Contains("Default")) {
                    initial = null;                    
                }

                vendorInventories.Add(new ScriptableObjectPersistence.VendorInventorySet(inventories[i], initial));
            }
        }
        ReflectionUtility.SetPrivateField(sop, "_vendorInventories", vendorInventories.ToArray());

        PrefabUtility.RecordPrefabInstancePropertyModifications(sop);
        
        Debug.Log("SOP: Refreshed references.");
    }
}
