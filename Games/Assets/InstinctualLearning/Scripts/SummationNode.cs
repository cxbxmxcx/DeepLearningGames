using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    public class SummationNode : MonoBehaviour
    {
        public Image node;
        private TextMeshProUGUI text;

        // Start is called before the first frame update
        void Awake()
        {
            node = GetComponentInChildren<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            SetSum(0);
        }

        public void SetSum(float sum)
        {
            text.text = sum.ToString("0.00");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
