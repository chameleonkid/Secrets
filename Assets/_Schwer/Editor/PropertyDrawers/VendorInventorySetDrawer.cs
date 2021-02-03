using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScriptableObjectPersistence.VendorInventorySet))]
public class VendorInventorySetDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var halfWidth = position.width / 2;
        var regRect = new Rect(position.x, position.y, halfWidth, EditorGUIUtility.singleLineHeight);
        var iniRect = new Rect(position.x + halfWidth, position.y, halfWidth, EditorGUIUtility.singleLineHeight);
        
        EditorGUI.PropertyField(regRect, property.FindPropertyRelative("regular"), GUIContent.none);
        EditorGUI.PropertyField(iniRect, property.FindPropertyRelative("initial"), GUIContent.none);
    }
}
