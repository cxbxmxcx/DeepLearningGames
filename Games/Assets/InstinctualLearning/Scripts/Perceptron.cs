using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace IL.DeepLearningGames
{
    public class Perceptron : MonoBehaviour
    {
        public bool needsUpdate;
        public float[] possibleWeights;
        public WeightNode[] nodes;
        public InputItem[] inputs;
        private RectTransform canvas;
        private DenseLayer layer;
        public SummationNode sumNode;
        public ActivationNode actNode;
        public OutputNode outNode;

        public LineRenderer connector;
        private InputItem currentInput;
        private LineRenderer c;
        private float midY;
        private LineRenderer[] inputConnectors;

        // Start is called before the first frame update
        void Awake()
        {
            nodes = GetComponentsInChildren<WeightNode>();            
            canvas = GetComponent<RectTransform>();
            layer = GetComponentInParent<DenseLayer>();
        }

        private void Start()
        {
            foreach(var node in nodes)
            {
                node.SetWeights(possibleWeights);                
            }
            StartCoroutine(ConnectNodes());
            if(inputs.Length > 0)
            {
                foreach(var input in inputs)
                {
                    input.AddPerceptron(this);
                }
                SetInput(inputs[0]);

                inputConnectors = new LineRenderer[inputs.Length];                
            }
        }

        private IEnumerator ConnectNodes()
        {
            yield return new WaitForEndOfFrame();

            var cam = Camera.main;            
            
            var st = sumNode.GetComponent<RectTransform>();
            var cst = ScreenUtils.GetWorldCorners(st);
            foreach (var node in nodes)
            {
                c = Instantiate(connector, node.transform);
                c.positionCount = 2;                
                c.SetPosition(0,cst[1]);
                
                var pt = node.parameter.GetComponent<RectTransform>();
                var cpt = ScreenUtils.GetWorldCorners(pt);
                midY = (cpt[2].y + cpt[3].y) / 2;
                c.SetPosition(1, new Vector3(cpt[2].x, midY, cpt[2].z));               
            }

            c = Instantiate(connector, sumNode.transform);
            c.positionCount = 2;
            midY = (cst[2].y + cst[3].y) / 2;
            c.SetPosition(0, new Vector3(cst[2].x, midY, cst[2].z));

            var at = actNode.GetComponent<RectTransform>();
            var ast = ScreenUtils.GetWorldCorners(at);
            midY = (ast[2].y + ast[3].y) / 2;
            c.SetPosition(1, new Vector3(ast[1].x, midY, ast[1].z));

            c = Instantiate(connector, actNode.transform);
            c.positionCount = 2;
            c.SetPosition(0, new Vector3(ast[2].x, midY, ast[2].z));

            var ot = outNode.GetComponent<RectTransform>();
            var ost = ScreenUtils.GetWorldCorners(ot);
            midY = (ost[2].y + ost[3].y) / 2;
            c.SetPosition(1, new Vector3(ost[1].x, midY, ost[1].z));

            UpdatePerceptron();
        }

        public void UpdatePerceptron()
        {
            needsUpdate = true;
        }

        public void SetInput(InputItem item)
        {
            if (item.inputs.Length != nodes.Length - 1 || 
                inputConnectors == null || 
                inputConnectors.Length<1) return; //always have one bias node

            foreach(var ir in inputConnectors)
            {
                if (ir != null) Destroy(ir);
            }

            var input = 0;
            currentInput = item;
            foreach(var node in nodes)
            {
                if (node.input.isBias) continue;
                
                node.input.SetInput(item.inputs[input]);

                if (input < inputConnectors.Length)
                {
                    c = Instantiate(connector, node.transform);

                    inputConnectors[input] = c;
                    var rn = node.GetComponent<RectTransform>();
                    var crn = ScreenUtils.GetWorldCorners(rn);

                    c.positionCount = 2;
                    midY = (crn[2].y + crn[3].y) / 2;
                    c.SetPosition(0, new Vector3(crn[1].x, midY, crn[1].z));

                    var itemNode = item.nodes[input];
                    var ics = ScreenUtils.GetWorldCorners(itemNode);
                    midY = (ics[2].y + ics[3].y) / 2;
                    c.SetPosition(1, new Vector3(ics[2].x, midY, ics[2].z));

                    input++;
                }
            }           

            UpdatePerceptron();
        }

        // Update is called once per frame
        void Update()
        {
            if (needsUpdate && currentInput != null)
            {
                needsUpdate = false;
                var sum = 0f;
                foreach(var node in nodes)
                {
                    sum += node.input.GetInput() * node.parameter.GetParameter();
                }
                sumNode.SetSum(sum);
                var act = actNode.Activate(sum);
                var loss = Mathf.Abs(currentInput.label - act);
                outNode.SetOutput(act,currentInput.label, loss);

                if(layer != null)
                {
                    layer.UpdateLayer();
                }
            }
        }
    }
}
