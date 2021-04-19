using Schwer.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SchwerEditor.Secrets {
    public class DeveloperWindow : EditorWindow {
        private const string _name = "Developer Controls";

        private static void Log(object message, Object context = null) => Debug.Log($"{_name}: {message}", context);
        private static void LogWarning(object message, Object context = null) => Debug.LogWarning($"{_name}: {message}", context);
        private static void LogError(object message, Object context = null) => Debug.LogError($"{_name}: {message}", context);

        [MenuItem("Window/" + _name)]
        public static void ShowWindow() => GetWindow<DeveloperWindow>(_name);

        private void OnGUI() {
            if (!Application.isPlaying) {
                EditorGUILayout.HelpBox("Enter Play Mode to enable developer controls.", MessageType.Info);
            }

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            DrawGeneralControls();
            DrawTimeControls();
            DrawPlayerControls();

            EditorGUI.EndDisabledGroup();
        }

        #region General Controls
        private void DrawGeneralControls() {
            GUILayout.Label("General");
            ReloadCurrentSceneButton();
        }

        private void ReloadCurrentSceneButton() {
            if (GUILayout.Button("Reload Current Scene")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                Log("Reloaded the current scene (and time scale set to 1).");
            }
            EditorGUILayout.HelpBox("Reloading a scene may take some time, depending on scene complexity.", MessageType.Info);
        }
        #endregion

        #region Time Controls
        private void DrawTimeControls() {
            GUILayout.Label("Time Controls");
            FreezeTimeMiddayButton();
            FreezeTimeMidnightButton();
            ResumeTimeButton();
        }

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

        #region Player Controls
        private void DrawPlayerControls() {
            GUILayout.Label("Player Controls");
            SetHealthToMax();
            SetManaToMax();
            SetLumenToMax();
        }

        private void SetHealthToMax() {
            if (GUILayout.Button("Set Health To Max")) {
                var player = FindObjectOfType<PlayerMovement>();
                if (player != null) {
                    player.healthMeter.current = player.healthMeter.max;
                    Log($"Player health set to max ({player.healthMeter.max}).");
                }
                else Log("No active Player found.");
            }
        }

        private void SetManaToMax() {
            if (GUILayout.Button("Set Mana To Max")) {
                var player = FindObjectOfType<PlayerMovement>();
                if (player != null) {
                    player.mana.current = player.mana.max;
                    Log($"Player mana set to max ({player.mana.max}).");
                }
                else Log("No active Player found.");
            }
        }

        private void SetLumenToMax() {
            if (GUILayout.Button("Set Lumen To Max")) {
                var player = FindObjectOfType<PlayerMovement>();
                if (player != null) {
                    player.lumen.current = player.lumen.max;
                    Log($"Player lumen set to max ({player.lumen.max}).");
                }
                else Log("No active Player found.");
            }
        }
        #endregion
    }
}
