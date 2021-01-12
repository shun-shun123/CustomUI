using NUnit.Framework;
using UnityEngine;

namespace Mummy.CustomUI.Test
{
    public class SingletonTest
    {
        /// <summary>
        /// Initialize Singleton test
        /// </summary>
        [Test]
        public void _01SingletonInitializeTest()
        {
            var _ = new GameObject("TestSingleton", typeof(TestSingleton));
            Assert.IsTrue(TestSingleton.Instance.Count == 100, "TestSingleton.Instance.Count == 100 failed");
        }

        public class TestSingleton : SingletonMonoBehaviour<TestSingleton>
        {
            public int Count { get; private set; }
            
            protected override void OnInitialize()
            {
                for (var i = 0; i < 100; i++)
                {
                    // Do something
                    Count++;
                }
                Debug.Log($"Finished Initialize: {Count}");
            }
        }
    }
}
