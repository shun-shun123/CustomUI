using UnityEngine;

namespace Mummy.CustomUI
{
    /// <summary>
    /// SingletonMonoBehaviour
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// SingletonInstance
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Initialized flag
        /// </summary>
        private bool _hasInitialized;

        /// <summary>
        /// public SingletonInstance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        if (Application.isPlaying)
                        {
                            Debug.LogError($"The GameObject attached {typeof(T)} is not found.");
                        }
                        else
                        {
                            Debug.LogWarning($"The GameObject attached {typeof(T)} is not found.");
                        }
                    }
                }

                var singleton = _instance as SingletonMonoBehaviour<T>;
                if (singleton != null && singleton._hasInitialized == false)
                {
                    singleton.OnInitialize();
                    singleton._hasInitialized = true;
                }
                
                return _instance;
            }
        }

        /// <summary>
        /// Awake
        /// </summary>
        protected virtual void Awake()
        {
            CheckInstance();
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Called when initialize
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Check exist instance
        /// </summary>
        private void CheckInstance()
        {
            if (this != _instance)
            {
                Destroy(this);
                Debug.LogError($"{typeof(T)} is already attached with {Instance.gameObject.name}, so destroy component attached with {name}.");
                return;
            }
        }
    }
}