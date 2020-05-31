using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.DeepLearningGames
{
    public class DenseLayer : MonoBehaviour
    {
        public bool needsUpdate;
        public Perceptron[] perceptrons;
        public OutputNode[] outputs;
        public DenseLayer nextLayer;
        //output of layer is input to next
        private InputItem output;
        private LineRenderer c;
        private float midY;

        private void Awake()
        {
            perceptrons = GetComponentsInChildren<Perceptron>();
            output = GetComponent<InputItem>();
            outputs = GetComponentsInChildren<OutputNode>();
        }

        private void Start()
        {
            StartCoroutine(ConnectNodes());            
        }

        private IEnumerator ConnectNodes()
        {
            yield return new WaitForEndOfFrame();

            if (output != null && nextLayer != null)
            {
                output.inputs = new float[perceptrons.Length];
                output.nodes = new RectTransform[perceptrons.Length];
                for (int i = 0; i < perceptrons.Length; i++)
                {
                    output.nodes[i] = perceptrons[i].outNode.GetComponent<RectTransform>();
                }
                //perceptrons in layer are just outputs
                for (int i = 0; i < perceptrons.Length; i++)
                {
                    var n = perceptrons[i];
                    n.outNode.IsLayer = true;

                    var rt = n.outNode.GetComponent<RectTransform>();
                    var crn = ScreenUtils.GetWorldCorners(rt);

                    foreach (var p in nextLayer.perceptrons)
                    {
                        c = Instantiate(n.connector, n.outNode.transform);
                        c.positionCount = 2;
                        midY = (crn[2].y + crn[3].y) / 2;
                        c.SetPosition(0, new Vector3(crn[2].x, midY, crn[2].z));

                        var t = p.nodes[i + 1].GetComponent<RectTransform>();
                        var trn = ScreenUtils.GetWorldCorners(t);
                        midY = (trn[2].y + trn[3].y) / 2;
                        c.SetPosition(1, new Vector3(trn[1].x, midY, trn[1].z));
                    }
                }
            }
        }
    
        private void Update()
        {
            if (needsUpdate && 
                output != null &&
                output.inputs != null && 
                output.inputs.Length >0)
            {                
                needsUpdate = false;
                for(int i=0;i<perceptrons.Length;i++)
                {                    
                    output.inputs[i] = perceptrons[i].outNode.output;
                    if (nextLayer.perceptrons.Length > 0)
                    {
                        for (int j = 0; j < nextLayer.perceptrons.Length; j++)
                        {
                            nextLayer.perceptrons[j].SetInput(output);
                        }
                    }
                    else
                    {
                        nextLayer.outputs[i].SetOutput(output.inputs[i], 0, 0);
                    }
                }
            }
        }

        public void UpdateLayer()
        {
            needsUpdate = true;
        }
    }
}
