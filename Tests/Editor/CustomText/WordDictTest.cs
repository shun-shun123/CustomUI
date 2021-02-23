using Mummy.CustomUI.Model;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Mummy.CustomUI.Test
{
    /// <summary>
    /// WordDictTest
    /// </summary>
    public class WordDictTest
    {
        /// <summary>
        /// wordDict instance for test
        /// </summary>
        private WordDict _wordDict = ScriptableObject.CreateInstance<WordDict>();

        /// <summary>
        /// word key
        /// </summary>
        private string _key = "WORD_KEY_TEST";

        /// <summary>
        /// word value
        /// </summary>
        private string _value = "WORD_TEST";
        
        /// <summary>
        /// Append new word pair test
        /// </summary>
        [Test]
        public void _01AppendNewWordPairTest()
        {
            var isSuccess = _wordDict.AppendNewWord(_key, _value);
            Assert.IsTrue(isSuccess, "WordDict append failed.");
        }

        /// <summary>
        /// GetWord test
        /// </summary>
        [Test]
        public void _02GetWordPairTest()
        {
            var getWord = _wordDict.GetWord(_key);
            Assert.AreEqual(_value, getWord, $"key: {_key} is saved correctly.");
        }

        /// <summary>
        /// Update word test
        /// </summary>
        [Test]
        public void _03UpdateWordPair()
        {
            var newKey = "WORD_NEW_KEY";
            _wordDict.UpdateWordPair(_key, newKey, _value);
            var getWord = _wordDict.GetWord(_key);
            Assert.IsTrue(string.IsNullOrEmpty(getWord), "Update word pair failed");

            getWord = _wordDict.GetWord(newKey);
            Assert.AreEqual(getWord, _value, "Update word failed");

            _key = newKey;
        }

        /// <summary>
        /// Remove word pair
        /// </summary>
        [Test]
        public void _04RemoveWordPair()
        {
            var isSuccess = _wordDict.RemoveWordPair(_key);
            Assert.IsTrue(isSuccess, "Remove word pair failed");

            var getWord = _wordDict.GetWord(_key);
            Assert.IsTrue(string.IsNullOrEmpty(getWord), "Remove word failed");
        }
    }
}
