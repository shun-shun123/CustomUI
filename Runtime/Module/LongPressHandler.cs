using System;
using UnityEngine;

namespace Mummy.CustomUI.Module
{
    [Serializable]
    public class LongPressHandler
    {
        /// <summary>
        /// LongPress time threshold
        /// </summary>
        [SerializeField, Range(0.1f, 10.0f)]
        private float longPressThreshold;

        /// <summary>
        /// whether raise longPressEvent once or many
        /// </summary>
        [SerializeField]
        private bool isRepeatLongPressEvent;

        /// <summary>
        /// LongPress delegate
        /// </summary>
        public delegate void OnLongPressed();

        /// <summary>
        /// LongPressEventHandler
        /// </summary>
        public OnLongPressed OnLongPressedHandler;

        /// <summary>
        /// LongPressTimer to count down longPressTime
        /// </summary>
        private float _longPressTimer;

        /// <summary>
        /// whether raise longPressEvent once or not
        /// </summary>
        private bool _hasRaisedLongPressEvent;

        /// <summary>
        /// Initialize parameters
        /// </summary>
        public void Initialize()
        {
            _longPressTimer = 0f;
            _hasRaisedLongPressEvent = false;
        }

        /// <summary>
        /// CountDown longPressTimer and execute longPressHandler if needed
        /// </summary>
        public void CountDownLongPress()
        {
            _longPressTimer += Time.unscaledDeltaTime;
            if (longPressThreshold <= _longPressTimer)
            {
                if (_hasRaisedLongPressEvent && !isRepeatLongPressEvent)
                {
                    return;
                }
                OnLongPressedHandler?.Invoke();
                _hasRaisedLongPressEvent = true;
                _longPressTimer = 0f;
            }
        }
    }
}