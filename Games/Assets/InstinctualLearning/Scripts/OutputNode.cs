using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IL.DeepLearningGames
{
    public class OutputNode : MonoBehaviour
    {
        public TextMeshProUGUI outputText;
        public TextMeshProUGUI expectedText;
        public TextMeshProUGUI lossText;        

        public void SetOutput(float output, float expected, float loss)
        {
            outputText.text = "Output\n" + output.ToString("0.00");
            expectedText.text = "Expected\n" + expected.ToString("0.00");
            lossText.text = "Loss\n" + loss.ToString("0.00");
        }

       
    }
}
