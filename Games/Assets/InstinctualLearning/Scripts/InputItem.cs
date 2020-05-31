using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    [RequireComponent(typeof(Button))]
    public class InputItem : MonoBehaviour
    {
        public float[] inputs;
        public float label;
        private Button button;
        private List<Perceptron> perceptrons;

        // Start is called before the first frame update
        private void Awake()
        {
            button = GetComponent<Button>();
            perceptrons = new List<Perceptron>();
        }
        void Start()
        {
            button.onClick.AddListener(() => UpdatePerceptron());
        }

        private void UpdatePerceptron()
        {
            foreach (var perceptron in perceptrons)
            {
                perceptron.SetInput(this);
            }
        }

        public void AddPerceptron(Perceptron perceptron)
        {
            perceptrons.Add(perceptron);
        }
    }
}
