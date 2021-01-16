using UnityEditor;
using UnityEngine;

namespace SchwerEditor {
    public static class AssetsUtility {
        /// <summary>
        /// Wrapper for `AssetDatabase.SaveAssets`, `AssetDatabase.Refresh`, and `EditorUtility.FocusProjectWindow`.
        /// </summary>
        public static void SaveRefreshAndFocus() {
            //! Need clarification on what each line does.
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }
        
        /// <summary>
        /// Returns all assets of a specified type from the Assets folder.
        /// </summary>
        public static T[] GetAllInstances<T>() where T : Object {
            // From: https://answers.unity.com/questions/1425758/how-can-i-find-all-instances-of-a-scriptable-objec.html
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            T[] instances = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return instances;
        }
    }
}
