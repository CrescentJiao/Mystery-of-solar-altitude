
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SpringFramework2.UI
{

    /// <summary>
    /// 函数图基础 XY轴 刻度等
    /// </summary>
    [Serializable]
    public class FunctionSetX2
    {
        // public static float x;
        public static float x2;
        public static VertexHelper vh22;
        public static FunctionalGraphBase2 GraphBase2 = new FunctionalGraphBase2();
        public static List<FunctionFormula2> Formulas2;
        public static RectTransform _myRect2;
    }
    public class FunctionalGraphBase2
    {
        /// <summary>
        /// 是否显示刻度
        /// </summary>
        public bool ShowScale2 = false;

        /// <summary>
        /// XY轴刻度
        /// </summary>
        [Range(20f, 100f)] public float ScaleValue2 = 50f;
        /// <summary>
        /// 刻度的长度
        /// </summary>
        [Range(2, 10)] public float ScaleLenght2 = 5.0f;
        /// <summary>
        /// XY轴宽度
        /// </summary>
        [Range(2f, 20f)] public float XYAxisWidth2 = 2.0f;
        /// <summary>
        /// XY轴颜色
        /// </summary>

    }

    /// <summary>
    /// 函数公式
    /// </summary>
    [Serializable]
    public class FunctionFormula2
    {
        /// <summary>
        /// 函数表达式
        /// </summary>
        public Func<float, float> Formula2;
        /// <summary>
        /// 函数图对应线条颜色
        /// </summary>
        public Color FormulaColor2;
        public float FormulaWidth2;

        public FunctionFormula2() { }
        public FunctionFormula2(Func<float, float> formula, Color formulaColor, float width)
        {
            Formula2 = formula;
            FormulaColor2 = formulaColor;
            FormulaWidth2 = width;
        }

        public Vector2 GetResult(float xValue, float scaleValue)
        {
            return new Vector2(xValue, Formula2(xValue / scaleValue) * scaleValue);
        }
    }


    public class FunctionalGraph2 : MaskableGraphic
    {
        public FunctionalGraphBase2 GraphBase2 = new FunctionalGraphBase2();
        public List<FunctionFormula2> Formulas2;
        private RectTransform _myRect2;
        private Vector2 _xPoint;
        private Vector2 _yPoint;


        /// <summary>
        /// 初始化函数信息
        /// </summary>
        public void Init()
        {
            _myRect2 = this.rectTransform;
            Formulas2 = new List<FunctionFormula2>
            {
             //  new FunctionFormula(Mathf.Sin, Color.red, 2.0f),
              //new FunctionFormula2(Mathf.Cos, Color.green, 2.0f),
               // new FunctionFormula(Mathf.Log10,Color.yellow,2.0f)
               new FunctionFormula2(xValue=>-(xValue-2)*(xValue-2)+2,Color.red,2.0f)
            };
        }

        /// <summary>
        /// 重写这个类以绘制UI
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Init();
            vh.Clear();
            FunctionSetX2.vh22 = vh;


            #region 函数图的绘制
            FunctionSetX2._myRect2 = _myRect2;
            FunctionSetX2.GraphBase2 = GraphBase2;
            FunctionSetX2.Formulas2 = Formulas2;
            drawline();

            #endregion
        }

        public static void drawline()
        {
            //FunctionSetX._myRect;
            //FunctionSetX.GraphBase = new FunctionalGraphBase();
            //FunctionSetX.vh2.Clear();//坐标轴

            Debug.Log("this is in drawline of x!" + FunctionSetX2.Formulas2.ToString());
            float unitPixel = 100 / FunctionSetX2.GraphBase2.ScaleValue2;
            foreach (var functionFormula in FunctionSetX2.Formulas2)
            {
                //FunctionSetX.x
                Vector2 startPos = functionFormula.GetResult(26, FunctionSetX2.GraphBase2.ScaleValue2);
                for (float x = -FunctionSetX2._myRect2.sizeDelta.x / 100.0f + 26; x < FunctionSetX2.x2 + 10 / 2.0f; x += unitPixel)
                {
                    Vector2 endPos = functionFormula.GetResult(x, FunctionSetX2.GraphBase2.ScaleValue2);
                    FunctionSetX2.vh22.AddUIVertexQuad(FunctionalGraph2.GetQuad(startPos, endPos, functionFormula.FormulaColor2, functionFormula.FormulaWidth2));
                    startPos = endPos;
                }
            }
            //Debug.Log("this is in drawline2!");
        }

        //通过两个端点绘制矩形
        public static UIVertex[] GetQuad(Vector2 startPos, Vector2 endPos, Color color0, float lineWidth = 2.0f)
        {
            float dis = Vector2.Distance(startPos, endPos);
            float y = lineWidth * 0.5f * (endPos.x - startPos.x) / dis;
            float x = lineWidth * 0.5f * (endPos.y - startPos.y) / dis;
            if (y <= 0) y = -y;
            else x = -x;
            UIVertex[] vertex = new UIVertex[4];
            vertex[0].position = new Vector3(startPos.x + x, startPos.y + y);
            vertex[1].position = new Vector3(endPos.x + x, endPos.y + y);
            vertex[2].position = new Vector3(endPos.x - x, endPos.y - y);
            vertex[3].position = new Vector3(startPos.x - x, startPos.y - y);
            for (int i = 0; i < vertex.Length; i++) vertex[i].color = color0;
            return vertex;
        }

        //通过四个顶点绘制矩形
        private UIVertex[] GetQuad(Vector2 first, Vector2 second, Vector2 third, Vector2 four, Color color0)
        {
            UIVertex[] vertexs = new UIVertex[4];
            vertexs[0] = GetUIVertex(first, color0);
            vertexs[1] = GetUIVertex(second, color0);
            vertexs[2] = GetUIVertex(third, color0);
            vertexs[3] = GetUIVertex(four, color0);
            return vertexs;
        }

        //构造UIVertex
        private UIVertex GetUIVertex(Vector2 point, Color color0)
        {
            UIVertex vertex = new UIVertex
            {
                position = point,
                color = color0,
                uv0 = new Vector2(0, 0)
            };
            return vertex;
        }


        //本地坐标转化屏幕坐标绘制GUI文字
        private Vector2 local2Screen(Vector2 parentPos, Vector2 localPosition)
        {
            Vector2 pos = localPosition + parentPos;
            float xValue, yValue = 0;
            if (pos.x > 0)
                xValue = pos.x + Screen.width / 2.0f;
            else
                xValue = Screen.width / 2.0f - Mathf.Abs(pos.x);
            if (pos.y > 0)
                yValue = Screen.height / 2.0f - pos.y;
            else
                yValue = Screen.height / 2.0f + Mathf.Abs(pos.y);
            return new Vector2(xValue, yValue);
        }

        //递归计算位置
        private Vector2 getScreenPosition(Transform trans, ref Vector3 result)
        {
            if (null != trans.parent && null != trans.parent.parent)
            {
                result += trans.parent.localPosition;
                getScreenPosition(trans.parent, ref result);
            }
            if (null != trans.parent && null == trans.parent.parent)
                return result;
            return result;
        }
    }
}
