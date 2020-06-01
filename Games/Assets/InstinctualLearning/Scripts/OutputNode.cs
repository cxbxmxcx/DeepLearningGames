using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    public class OutputNode : MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI outputText;
        public TextMeshProUGUI expectedText;
        public TextMeshProUGUI lossText;
        [SerializeField]
        private bool isLayer = false;
        public float output;
        public float expected;
        public float loss;

        public bool IsLayer
        {
            get
            {
                return isLayer;
            }
            set
            {
                isLayer = value;
                var rt = GetComponent<RectTransform>();
                if (isLayer)
                {
                    rt.sizeDelta = new Vector2(80, 80);
                    expectedText.gameObject.SetActive(false);
                    lossText.gameObject.SetActive(false);
                }
                else
                {
                    rt.sizeDelta = new Vector2(80, 120);
                    expectedText.gameObject.SetActive(true);
                    lossText.gameObject.SetActive(true);
                }
            }
        }

        public void SetOutput(float output, float expected, float loss)
        {
            this.output = output;
            this.expected = expected;
            this.loss = loss;

            if (image == null)
            {
                outputText.text = "Output\n" + output.ToString("0.00");
                expectedText.text = "Expected\n" + expected.ToString("0.00");
                lossText.text = "Loss\n" + loss.ToString("0.00");
            }
            else
            {
                image.color = new Color(output, output, output);
                outputText.text = "Output\n" + output.ToString("0.00");
                outputText.color = new Color(1.0f - output, 1.0f - output, 1.0f - output);
            }
        }

       
    }
}
