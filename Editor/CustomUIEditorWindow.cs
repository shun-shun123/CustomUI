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
        /// <summary>
        /// CustomUIEditorWindow
        /// </summary>
        private static CustomUIEditorWindow _window;

        /// <summary>
        /// WordDict
        /// </summary>
        private WordDict _wordDict;

        /// <summary>
        /// newKey
        /// </summary>
        private string _newKey;

        /// <summary>
        /// newWord
        /// </summary>
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

        /// <summary>
        /// OnGUI
        /// </summary>
        private void OnGUI()
        {
            DrawCustomTextSettings();
        }

        /// <summary>
        /// DrawCustomTextSettings
        /// </summary>
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

        /// <summary>
        /// ReadWordDictData from objectField
        /// </summary>
        private void ReadWordDictData()
        {
            _wordDict = EditorGUILayout.ObjectField(_wordDict, typeof(WordDict), false) as WordDict;
        }

        /// <summary>
        /// Draw saved wordDict data
        /// </summary>
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
                        modifiedWordPairs.Add(new ModifiedWordPair(pair.Key, key, value));
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

        /// <summary>
        /// Draw new word pair
        /// </summary>
        private void DrawNewWordPair()
        {
            EditorGUILayout.BeginHorizontal();
            {
                _newKey = GUILayout.TextField(_newKey);
                _newWord = EditorGUILayout.TextField(_newWord);
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// save new word pair data
        /// </summary>
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
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="beforeKey">beforeKey</param>
            /// <param name="afterKey">afterKey</param>
            /// <param name="afterValue">afterValue</param>
            public ModifiedWordPair(string beforeKey, string afterKey, string afterValue)
            {
                BeforeKey = beforeKey;
                AfterKey = afterKey;
                AfterValue = afterValue;
            }
            
            /// <summary>
            /// BeforeKey
            /// </summary>
            public string BeforeKey;
            
            /// <summary>
            /// AfterKey
            /// </summary>
            public string AfterKey;
            
            /// <summary>
            /// AfterValue
            /// </summary>
            public string AfterValue;
        }
    }
}