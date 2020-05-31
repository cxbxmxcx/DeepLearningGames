using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.DeepLearningGames
{
    public class WeightNode : MonoBehaviour
    {
        public InputNode input;
        public ParameterNode parameter;

        // Start is called before the first frame update
        void Awake()
        {
            input = GetComponentInChildren<InputNode>();
            parameter = GetComponentInChildren<ParameterNode>();
        }

        public void SetWeights(float[] possibleWeights)
        {            
            parameter.SetWeights(possibleWeights);
            input.SetInput(0f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
