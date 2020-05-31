using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace IL.DeepLearningGames
{
    [RequireComponent(typeof(UILineRenderer))]
    public class WeightConnector : MonoBehaviour
    {
        private UILineRenderer line;
        private RectTransform rectTransform;

        // Start is called before the first frame update
        void Awake()
        {
            line = GetComponent<UILineRenderer>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            SetLineWidth();
        }

        private void SetLineWidth()
        {
            var width = rectTransform.rect.width;
            line.Points = new Vector2[] { new Vector2(0, 0), new Vector2(width, 0) };
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
