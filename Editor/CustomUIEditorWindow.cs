using System.Collections.Generic;
using System.IO;
using Mummy.CustomUI.Model;
using UnityEditor;
using UnityEngine;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomUI setting window
    /// </summary>
    public class CustomUIEditorWindow : EditorWindow
    {
        private static CustomUIEditorWindow _window;

        private WordDict _wordDict;

        private string _newKey;

        private string _newWord;
        
        /// <summary>
        /// Open customUi settings window
        /// </summary>
        [MenuItem("CustomUI/Open settings window")]
        public static void Init()
        {
            _window = GetWindow<CustomUIEditorWindow>("CustomUI Settings");
            _window.Show();
        }

        private void OnGUI()
        {
            DrawCustomTextSettings();
        }

        private void DrawCustomTextSettings()
        {
            EditorGUILayout.BeginVertical();
            {
                ReadWordDictData();
                if (_wordDict == null)
                {
                    return;
                }
                DrawSavedWordDictData();
                DrawNewWordPair();
                SaveNewWordPair();
            }
            EditorGUILayout.EndVertical();
        }

        private void ReadWordDictData()
        {
            _wordDict = EditorGUILayout.ObjectField(_wordDict, typeof(WordDict), false) as WordDict;
        }

        private void DrawSavedWordDictData()
        {
            List<string> removeKeys = new List<string>();
            List<ModifiedWordPair> modifiedWordPairs = new List<ModifiedWordPair>();
            
            foreach (var pair in _wordDict.WordDictionary)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var key = GUILayout.TextField(pair.Key);
                    var value = GUILayout.TextField(pair.Value);

                    // modified word pair data
                    if (key != pair.Key || value != pair.Value)
                    {
                        modifiedWordPairs.Add(new ModifiedWordPair(pair.Key, pair.Value, key, value));
                    }
                    
                    // pressed delete button
                    if (GUILayout.Button("X"))
                    {
                        removeKeys.Add(pair.Key);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            // update removed word pair
            foreach (var removeKey in removeKeys)
            {
                _wordDict.RemoveWordPair(removeKey);
            }
            
            // update modified word pair
            foreach (var mod in modifiedWordPairs)
            {
                _wordDict.UpdateWordPair(mod.BeforeKey, mod.AfterKey, mod.AfterValue);
            }
        }

        private void DrawNewWordPair()
        {
            EditorGUILayout.BeginHorizontal();
            {
                _newKey = GUILayout.TextField(_newKey);
                _newWord = EditorGUILayout.TextField(_newWord);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SaveNewWordPair()
        {
            if (GUILayout.Button("Append new pair"))
            {
                if (string.IsNullOrEmpty(_newKey) || string.IsNullOrEmpty(_newWord))
                {
                    Debug.LogError("Empty key or word cannot append to wordDict.");
                    return;
                }

                if (_wordDict.AppendNewWord(_newKey, _newWord))
                {
                    _newKey = string.Empty;
                    _newWord = string.Empty;
                }

            }
        }

        /// <summary>
        /// WordDict modified info
        /// </summary>
        private class ModifiedWordPair
        {
            public ModifiedWordPair(string beforeKey, string beforeValue, string afterKey, string afterValue)
            {
                BeforeKey = beforeKey;
                BeforeValue = beforeValue;
                AfterKey = afterKey;
                AfterValue = afterValue;
            }
            public string BeforeKey;
            public string BeforeValue;
            public string AfterKey;
            public string AfterValue;
        }
    }
}