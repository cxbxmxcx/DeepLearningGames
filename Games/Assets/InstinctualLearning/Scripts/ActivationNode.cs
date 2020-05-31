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
        }

        private void Start()
        {
            NextFunction();
        }

        public void NextFunction()
        {
            if (activationFunctions.Length > 0)
            {
                FunctionIndex++;
                SetImage();
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
