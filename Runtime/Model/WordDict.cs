using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mummy.CustomUI.Model
{
    /// <summary>
    /// WordDict model
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "CustomUI/WordDict", fileName = WordManager.WordDictDataPath)]
    public class WordDict : ScriptableObject, ISerializationCallbackReceiver
    {
        /// <summary>
        /// WordDictionary
        /// </summary>
        private Dictionary<string, string> _wordDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Readonly wordDict
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<string, string>> WordDictionary => _wordDictionary;
        
        /// <summary>
        /// Keys
        /// </summary>
        [SerializeField]
        private List<string> keys = new List<string>();

        /// <summary>
        /// Words
        /// </summary>
        [SerializeField]
        private List<string> words = new List<string>();
        
        /// <summary>
        /// OnBeforeSerialize
        /// </summary>
        public void OnBeforeSerialize()
        {
            keys.Clear();
            words.Clear();
            foreach (var pair in _wordDictionary)
            {
                keys.Add(pair.Key);
                words.Add(pair.Value);
            }
        }

        /// <summary>
        /// OnAfterDeserialize
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (keys.Count != words.Count)
            {
                Debug.LogError("keys count and words count are not same. deserialization failed.");
                return;
            }

            _wordDictionary.Clear();
            for (var i = 0; i < keys.Count; i++)
            {
                _wordDictionary[keys[i]] = words[i];
            }
        }

        /// <summary>
        /// Append new word pair
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="word">word</param>
        /// <returns>isSuccess</returns>
        public bool AppendNewWord(string key, string word)
        {
            if (_wordDictionary.ContainsKey(key))
            {
                return false;
            }

            _wordDictionary[key] = word;
            return true;
        }

        /// <summary>
        /// Remove word pair
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>isSuccess</returns>
        public bool RemoveWordPair(string key)
        {
            return _wordDictionary.Remove(key);
        }

        /// <summary>
        /// GetWord
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>registered word</returns>
        public string GetWord(string key)
        {
            if (_wordDictionary.ContainsKey(key))
            {
                return _wordDictionary[key];
            }

            Debug.LogWarning($"key: {key} is not found in wordDict.");
            return string.Empty;
        }

        /// <summary>
        /// Update exist word pair
        /// </summary>
        /// <param name="beforeKey">before key</param>
        /// <param name="afterKey">after key</param>
        /// <param name="afterValue">after value</param>
        public void UpdateWordPair(string beforeKey, string afterKey, string afterValue)
        {
            _wordDictionary.Remove(beforeKey);
            _wordDictionary[afterKey] = afterValue;
        }
    }
}