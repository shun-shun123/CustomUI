using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomText Editor
    /// </summary>
    [CustomEditor(typeof(CustomText))]
    public class CustomTextEditor : Editor
    {
        [MenuItem("GameObject/CustomUI/Custom Text", false, 10)]
        public static void CreateCustomText()
        {
            var selectObject = Selection.activeGameObject;
            if (selectObject == null)
            {
                var canvas = FindObjectOfType<Canvas>();
                if (canvas == null)
                {
                    canvas = CreateNewCanvas();
                }

                var eventSystem = FindObjectOfType<EventSystem>();
                if (eventSystem == null)
                {
                    eventSystem = CreateNewEventSystem();
                }

                CreateNewCustomText(canvas.transform);
                return;
            }

            CreateNewCustomText(selectObject.transform);
        }

        /// <summary>
        /// Canvasを新規作成
        /// </summary>
        /// <returns>Canvas</returns>
        private static Canvas CreateNewCanvas()
        {
            var canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(CanvasRenderer));
            // Init canvas
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 0;

            // Init canvasScaler 
            var canvasScaler = canvasObject.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;

            // Init GraphicRaycaster
            var graphicRaycaster = canvasObject.GetComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;
            graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
            
            return canvas;
        }

        /// <summary>
        /// Create new EventSystem
        /// </summary>
        /// <returns>EventSystem component</returns>
        private static EventSystem CreateNewEventSystem()
        {
            var eventSystemObject = new GameObject("EventSystem", typeof(EventSystem));
            var eventSystem = eventSystemObject.GetComponent<EventSystem>();
            return eventSystem;
        }

        /// <summary>
        /// Create new CustomText
        /// </summary>
        /// <param name="parent">Parent Transform</param>
        /// <returns>CustomText</returns>
        private static CustomText CreateNewCustomText(Transform parent)
        {
            var customTextObject = new GameObject("Custom Text", typeof(CustomText));
            var customText = customTextObject.GetComponent<CustomText>();
            
            // SetParent
            customTextObject.transform.SetParent(parent);
            
            // Init Transform
            customTextObject.transform.localScale = Vector3.one;
            customTextObject.transform.localRotation = Quaternion.identity;

            return customText;
        }
    }
}