using Schwer.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SchwerEditor.Secrets {
    public class DeveloperWindow : EditorWindow {
        private const string _name = "Developer Controls";

        [MenuItem("Window/" + _name)]
        public static void ShowWindow() => GetWindow<DeveloperWindow>(_name);

        private void OnGUI() {
            if (!Application.isPlaying) {
                EditorGUILayout.HelpBox("Enter Play Mode to enable developer controls.", MessageType.Info);
            }

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            GUILayout.Label("General");
            ReloadCurrentSceneButton();

            GUILayout.Label("Time Controls");
            FreezeTimeMiddayButton();
            FreezeTimeMidnightButton();
            ResumeTimeButton();

            EditorGUI.EndDisabledGroup();
        }

        private void ReloadCurrentSceneButton() {
            if (GUILayout.Button("Reload Current Scene")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                Log("Reloaded the current scene (and time scale set to 1).");
            }
            EditorGUILayout.HelpBox("Reloading a scene may take some time, depending on scene complexity.", MessageType.Info);
        }

        #region Time Controls
        private void FreezeTimeMiddayButton() {
            if (GUILayout.Button("Freeze Time Midday")) {
                var time = FindObjectOfType<TimeManager>();
                if (time != null) {
                    ReflectionUtility.SetPrivateProperty(time, "normalizedTimeOfDay", 0.5f);
                    ReflectionUtility.SetPrivateField(time, "timeMultiplier", 0);
                    Log("Time set to and frozen at midday.");
                }
                else Log("No active TimeManager found.");
            }
        }

        private void FreezeTimeMidnightButton() {
            if (GUILayout.Button("Freeze Time Midnight")) {
                var time = FindObjectOfType<TimeManager>();
                if (time != null) {
                    ReflectionUtility.SetPrivateProperty(time, "normalizedTimeOfDay", 0);
                    ReflectionUtility.SetPrivateField(time, "timeMultiplier", 0);
                    Log("Time set to and frozen at midnight.");
                }
                else Log("No active TimeManager found.");
            }
        }

        private void ResumeTimeButton() {
            if (GUILayout.Button("Resume Time")) {
                var time = FindObjectOfType<TimeManager>();
                if (time != null) {
                    ReflectionUtility.SetPrivateField(time, "timeMultiplier", 1);
                    Log("Time multiplier set to 1.");
                }
                else Log("No active TimeManager found.");
            }
        }
        #endregion

        private static void Log(object message, Object context = null) => Debug.Log($"{_name}: {message}", context);
        private static void LogWarning(object message, Object context = null) => Debug.LogWarning($"{_name}: {message}", context);
        private static void LogError(object message, Object context = null) => Debug.LogError($"{_name}: {message}", context);
    }
}
