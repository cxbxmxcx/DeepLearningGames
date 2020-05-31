using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearActivationFunction : ActivationFunction
{
    public override float Activate(float input)
    {
        return input;
    }
}
