using UnityEditor;
using UnityEngine;

namespace SchwerEditor.ItemSystem {
    [CustomEditor(typeof(Inventory))]
    public class InventoryDemo : Editor {
        private static Item item;
        private static int amount = 1;

        public override void OnInspectorGUI() {
            var inventory = (Inventory)target;
            DrawDemoControls(inventory);

            GUILayout.Space(5);
            base.OnInspectorGUI();
        }

        private void DrawDemoControls(Inventory inventory) {
            EditorGUILayout.LabelField("Demo Controls", EditorStyles.boldLabel);

            if (GUILayout.Button("Clear Inventory")) {
                inventory.items.Clear();
                Debug.Log("Cleared '" + inventory.name + "'.");
            }

            DrawItemControls(inventory);
        }

        private void DrawItemControls(Inventory inventory) {
            EditorGUILayout.BeginVertical("box");

            item = (Item)EditorGUILayout.ObjectField("Item", item, typeof(Item), false);
            amount = EditorGUILayout.IntField("Amount", amount);

            var invName = "'" + inventory.name + "'";
            var itemName = "'" + ((item != null) ? item.name : "(Item)") + "'";

            EditorGUI.BeginDisabledGroup(item == null);
            if (GUILayout.Button("Set " + itemName + " to " + amount + "x")) {
                inventory.items[item] = amount;
                Debug.Log("Set " + invName + " " + itemName + " to " + amount + "x.");
            }
            if (GUILayout.Button("Add " + amount + "x " + itemName)) {
                inventory.items[item] += amount;
                Debug.Log("Added " + amount + "x " + itemName + " to " + invName + ".");
            }
            if (GUILayout.Button("Subtract " + amount + "x " + itemName)) {
                inventory.items[item] -= amount;
                Debug.Log("Removed " + amount + "x " + itemName + " from " + invName + ".");
            }
            if (GUILayout.Button("Remove all of " + itemName)) {
                if (inventory.items.Remove(item)) {
                    Debug.Log("Removed all of " + itemName + " from " + invName + ".");
                }
                else {
                    Debug.Log(invName + " does not have any of " + itemName + " to remove.");
                }
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();
        }
    }   
}
