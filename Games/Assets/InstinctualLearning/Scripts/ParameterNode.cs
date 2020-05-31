using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    public class ParameterNode : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Perceptron perceptron;
        private int weightIndex;
        public int WeightIndex
        {
            get
            {
                return weightIndex;
            }
            set
            {
                weightIndex = value >= possibleWeights.Length ? 0 : value;                
            }
        }
        private float[] possibleWeights;

        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            perceptron = GetComponentInParent<Perceptron>();
        }

        public void NextWeight()
        {
            if (possibleWeights.Length > 0)
            {
                WeightIndex++;
                SetText();
            }
        }

        private void SetText()
        {            
            text.text = possibleWeights[WeightIndex].ToString("0.00");
            perceptron.UpdatePerceptron();
        }
         
        public void SetWeights(float[] possibleWeights)
        {
            this.possibleWeights = possibleWeights;
            if(possibleWeights.Length > 0)
            {
                WeightIndex = 0;
                SetText();                
            }
        }

        public float GetParameter()
        {
            float result;
            if (float.TryParse(text.text, out result)) return result;
            else return 0;
        }
    }
}
