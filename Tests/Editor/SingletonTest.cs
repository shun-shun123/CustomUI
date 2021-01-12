using NUnit.Framework;
using UnityEngine;

namespace Mummy.CustomUI.Test
{
    /// <summary>
    /// SingletonTest
    /// </summary>
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

        /// <summary>
        /// Singleton class for test
        /// </summary>
        public class TestSingleton : SingletonMonoBehaviour<TestSingleton>
        {
            /// <summary>
            /// Count
            /// </summary>
            public int Count { get; private set; }

            /// <summary>
            /// OnInitialize
            /// </summary>
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
