using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.DeepLearningGames
{
    public class HalfStepActivationFunction : ActivationFunction
    {
        public override float Activate(float input)
        {
            if (input >= .5f) return 1.0f;
            else return 0.0f;
        }
    }
}
