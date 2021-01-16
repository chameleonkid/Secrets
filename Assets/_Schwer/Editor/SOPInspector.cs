﻿using System.Collections.Generic;
using Schwer.ItemSystem;
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

        var itemDB = FindFirstAsset<ItemDatabase>("");
        SetPrivateField(sop, "_itemDatabase", itemDB);

        var name = FindFirstAsset<StringValue>("Save");
        SetPrivateField(sop, "_saveName", name);

        var pos = FindFirstAsset<VectorValue>("Player");
        SetPrivateField(sop, "_playerPosition", pos);

        var health = FindFirstAsset<ConstrainedFloat>("Health");
        SetPrivateField(sop, "_health", health);

        var mana = FindFirstAsset<ConstrainedFloat>("Mana");
        SetPrivateField(sop, "_mana", mana);

        var lumen = FindFirstAsset<ConstrainedFloat>("Lumen");
        SetPrivateField(sop, "_lumen", lumen);

        var xp = FindFirstAsset<XPSystem>("Player");
        SetPrivateField(sop, "_xpSystem", xp);

        var time = FindFirstAsset<FloatValue>("TimeOfDay");
        SetPrivateField(sop, "_timeOfDay", time);

        var appearance = FindFirstAsset<CharacterAppearance>("Player");
        SetPrivateField(sop, "_characterAppearance", appearance);

        var inv = FindFirstAsset<Inventory>("Player");
        SetPrivateField(sop, "_playerInventory", inv);

        var bools = AssetsUtility.GetAllInstances<BoolValue>();
        var strings = AssetsUtility.GetAllInstances<StringValue>();
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

        SetPrivateField(sop, "_chests", chests.ToArray());
        SetPrivateField(sop, "_doors", doors.ToArray());
        SetPrivateField(sop, "_bosses", bosses.ToArray());
        SetPrivateField(sop, "_healthCrystals", healthCrystals.ToArray());
        SetPrivateField(sop, "_manaCrystals", manaCrystals.ToArray());

        var inventories = AssetsUtility.GetAllInstances<Inventory>();
        var vendorInventories = new List<Inventory>();
        for (int i = 0; i < inventories.Length; i++) {
            var path = AssetDatabase.GetAssetPath(inventories[i]);
            if (path.Contains("Vendor")) {
                vendorInventories.Add(inventories[i]);
            }
        }
        SetPrivateField(sop, "_vendorInventories", vendorInventories.ToArray());

        Debug.Log("SOP: Refreshed references.");
    }

    // Reference: https://stackoverflow.com/questions/12993962/set-value-of-private-field
    private static void SetPrivateField(object instance, string fieldName, object value) {
        var prop = instance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        prop.SetValue(instance, value);
    }

    private static T FindFirstAsset<T>(string filter) where T : Object {
        if (!filter.Contains("t:")) {
            filter = $"{filter} t:{typeof(T)}";
        }

        var guids = AssetDatabase.FindAssets(filter);
        if (guids.Length <= 0) return default(T);

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
    }
}
