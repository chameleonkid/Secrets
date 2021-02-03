using System.Collections.Generic;
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

        var skinTexDB = AssetsUtility.FindFirstAsset<SkinTexturesDatabase>("");
        ReflectionUtility.SetPrivateField(sop, "_skinTexturesDatabase", skinTexDB);

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

        for (int i = 0; i < bools.Length; i++) {
            var path = AssetDatabase.GetAssetPath(bools[i]);
            if (path.Contains("Chest")) {
                chests.Add(bools[i]);
            }
            else if (path.Contains("Door")) {
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
        }

        ReflectionUtility.SetPrivateField(sop, "_chests", chests.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_doors", doors.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_bosses", bosses.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_healthCrystals", healthCrystals.ToArray());
        ReflectionUtility.SetPrivateField(sop, "_manaCrystals", manaCrystals.ToArray());

        var inventories = AssetsUtility.FindAllAssets<Inventory>();
        var vendorInventories = new List<Inventory>();
        for (int i = 0; i < inventories.Length; i++) {
            var path = AssetDatabase.GetAssetPath(inventories[i]);
            if (path.Contains("Vendor") && !path.Contains("Default")) {         // <------ Why is it still resetting?
                vendorInventories.Add(inventories[i]);
            }
        }
        ReflectionUtility.SetPrivateField(sop, "_vendorInventories", vendorInventories.ToArray());

        Debug.Log("SOP: Refreshed references.");
    }
}
