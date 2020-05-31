using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationFunction : MonoBehaviour
{
    public Sprite image;
    public virtual float Activate(float input)
    {
        return input;
    }  
}
