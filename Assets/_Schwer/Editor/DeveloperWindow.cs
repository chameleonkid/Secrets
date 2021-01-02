using UnityEditor;
using UnityEngine;

namespace SchwerEditor.Secrets
{
    public class DeveloperWindow : EditorWindow {
        private const string _name = "Developer Controls";

        [MenuItem("Window/" + _name)]
        public static void ShowWindow() => GetWindow<DeveloperWindow>(_name);

        private void OnGUI() {
            if (!Application.isPlaying) {
                EditorGUILayout.HelpBox("Enter Play Mode to enable developer controls.", MessageType.Info);
            }

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            FreezeTimeMiddayButton();

            EditorGUI.EndDisabledGroup();
        }

        private void FreezeTimeMiddayButton() {
            if (GUILayout.Button("Freeze Time Midday")) {
                var time = FindObjectOfType<TimeManager>();
                if (time != null) {
                    SetPrivateField(time, "normalizedTimeOfDay", 0.5f);
                    SetPrivateField(time, "timeMultiplier", 0);
                    Log("Time set to and frozen at midday.");
                }
                else {
                    LogWarning("Could not find an active TimeManager");
                }
            }
        }

        private static void Log(object message, Object context = null) => Debug.Log($"{_name}: {message}", context);
        private static void LogWarning(object message, Object context = null) => Debug.LogWarning($"{_name}: {message}", context);
        private static void LogError(object message, Object context = null) => Debug.LogError($"{_name}: {message}", context);

        // Reference: https://stackoverflow.com/questions/12993962/set-value-of-private-field
        private static void SetPrivateField(object instance, string fieldName, object value) {
            var prop = instance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(instance, value);
        }
    }
}
