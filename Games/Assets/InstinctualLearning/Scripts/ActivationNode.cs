using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IL.DeepLearningGames
{
    public class ActivationNode : MonoBehaviour
    {        
        public ActivationFunction[] activationFunctions;
        public Button button;
        public Perceptron perceptron;
        private Image image;
        private TextMeshProUGUI text;
        private int functionIndex;
        public int FunctionIndex
        {
            get
            {
                return functionIndex;
            }
            set
            {
                functionIndex = value >= activationFunctions.Length ? 0 : value;
            }
        }

        public ActivationFunction ActivationFunction
        {
            get
            {
                return activationFunctions[FunctionIndex];
            }
        }

        private void Awake()
        {
            image = GetComponent<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            button = GetComponent<Button>();
            perceptron = GetComponentInParent<Perceptron>();
            FunctionIndex = -1;
        }

        private void Start()
        {
            NextFunction();
            if(button != null)
            {
                button.onClick.AddListener(() => NextFunction());
            }
        }

        public void NextFunction()
        {
            if (activationFunctions.Length > 0)
            {
                FunctionIndex++;
                SetImage();
                perceptron.UpdatePerceptron();
            }
        }

        private void SetImage()
        {
            image.sprite = activationFunctions[FunctionIndex].image;
        }

        public float Activate(float input)
        {
            var function = activationFunctions[FunctionIndex];
            return function.Activate(input);
        }
    }
}
