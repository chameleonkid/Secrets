using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SchwerEditor {
    public class AnimationRelinker : EditorWindow {
        private const string _name = "Animation Relinker";

        private static AnimatorController controller;
        private static GameObject originalGameObject;

        [MenuItem("Window/" + _name)]
        public static void ShowWindow() => GetWindow<AnimationRelinker>(_name);

        private void OnGUI() {
            controller = (AnimatorController)EditorGUILayout.ObjectField("Animator Controller", controller, typeof(AnimatorController), false);
            originalGameObject = (GameObject)EditorGUILayout.ObjectField("Original Game Object", originalGameObject, typeof(GameObject), true);

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying || controller == null || originalGameObject == null);
            if (GUILayout.Button("Relink")) {
                Relink();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void Relink() {
            try {
                AssetDatabase.StartAssetEditing();
                foreach (var animation in controller.animationClips) {
                    var path = AssetDatabase.GetAssetPath(animation);
                    var lines = File.ReadAllLines(path);

                    for (int i = 0; i + 1 < lines.Length; i++) {
                        if (lines[i].Contains($"attribute: ")) {
                            var key = "path: ";
                            if (lines[i + 1].TrimStart() == "path: ") {
                                lines[i + 1] += originalGameObject.name;
                            }
                            else if (lines[i + 1].TrimStart().StartsWith(key) && !lines[i + 1].Contains(originalGameObject.name)) {
                                lines[i + 1] = lines[i + 1].Replace(key, $"{key}{originalGameObject.name}/");
                            }
                        }
                    }

                    File.WriteAllLines(path, lines);
                }
            }
            finally {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}
