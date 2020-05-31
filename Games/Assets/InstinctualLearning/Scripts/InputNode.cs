using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    public class InputNode : MonoBehaviour
    {
        private TextMeshProUGUI text;
        public bool isBias;
        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetInput(float input)
        {
            if (isBias) text.text = "1.0";
            else text.text = input.ToString("0.00");
        }

        public float GetInput()
        {
            float result;
            if (float.TryParse(text.text, out result)) return result;
            else return 0;
        }
                
    }
}
