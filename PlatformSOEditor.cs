using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace HeroesGames.Arcade.SO.Platform
{
    [CustomEditor(typeof(PlatformSO))]
    public class PlatformSOEditor : Editor
    {
        public int customDistance = 64;
        public PosibleDirectionPlatforms customDirectionPlatforms = PosibleDirectionPlatforms.Right;
        public int index = 0;
        public int space = 81;
        public override void OnInspectorGUI()
        {
            var buttonHeight = 40;
            // var buttonWidth = 83;
            // var fontSize = GUI.skin.button.fontSize += 5;
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            var fontSize = buttonStyle.fontSize += 5;
            buttonStyle.margin.left +=buttonStyle.margin.left+ (int)GUI.skin.button.fixedWidth;
            buttonStyle.fontSize =fontSize;
            GUIStyle normalButtonStyle = new GUIStyle(GUI.skin.button);
            normalButtonStyle.fontSize =fontSize;
            
            base.OnInspectorGUI();
            GUILayout.Space(20f);
            GUILayout.Label("Add Directions", EditorStyles.boldLabel);
            customDistance = EditorGUILayout.IntField("Distance in Pixels", customDistance);
            // space=EditorGUILayout.IntField("Distance middle button", space);

            #region BUTTONS

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(char.ConvertFromUtf32(0x2196),normalButtonStyle ,GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddDiagonalUL(customDistance);
            }

            if (GUILayout.Button(char.ConvertFromUtf32(0x2191),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddUp(customDistance);
            }

            if (GUILayout.Button(char.ConvertFromUtf32(0x2197),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddDiagonalUR(customDistance);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(char.ConvertFromUtf32(0x2190),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddLeft(customDistance);
            }
            GUILayout.Space(space);
            if (GUILayout.Button(char.ConvertFromUtf32(0x2192), buttonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddRight(customDistance);
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(char.ConvertFromUtf32(0x2199),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddDiagonalDL(customDistance);
            }

            if (GUILayout.Button(char.ConvertFromUtf32(0x2193),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddDown(customDistance);
            }

            if (GUILayout.Button(char.ConvertFromUtf32(0x2198),normalButtonStyle, GUILayout.Height(buttonHeight)))
            {
                ((PlatformSO) target).AddDiagonalDR(customDistance);
            }

            GUILayout.EndHorizontal();

            #endregion

            index = EditorGUILayout.IntField("Add / Remove at", index);
            customDirectionPlatforms =
                (PosibleDirectionPlatforms) EditorGUILayout.EnumPopup("Direction to Add",
                    customDirectionPlatforms);
            if(GUILayout.Button($"Add at {index}",GUILayout.Height(buttonHeight)))
                ((PlatformSO) target).AddToList(customDirectionPlatforms,customDistance,index);
            
            if (GUILayout.Button($"Destroy at {index}", GUILayout.Height(buttonHeight)))
                ((PlatformSO) target).RemoveDirection(index);
            
            if (GUILayout.Button("Destroy last added", GUILayout.Height(buttonHeight)))
                ((PlatformSO) target).RemoveDirection();

            if (GUILayout.Button("Reset", GUILayout.Height(buttonHeight)))
                ((PlatformSO) target).ResetDirections();
        }
    }
}