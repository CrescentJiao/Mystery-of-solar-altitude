
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SpringFramework.UI
{

    /// <summary>
    /// 函数图基础 XY轴 刻度等
    /// </summary>
    [Serializable]
    public class FunctionSetX
    {
        public static float x;
        //public static float x2;
        public static VertexHelper vh2;
        public static FunctionalGraphBase GraphBase = new FunctionalGraphBase();
        public static List<FunctionFormula> Formulas;
        public static RectTransform _myRect;
    }
    public class FunctionalGraphBase
    {
        /// <summary>
        /// 是否显示刻度
        /// </summary>
        public bool ShowScale = false;

        /// <summary>
        /// XY轴刻度
        /// </summary>
        [Range(20f, 100f)] public float ScaleValue = 50f;
        /// <summary>
        /// 刻度的长度
        /// </summary>
        [Range(2, 10)] public float ScaleLenght = 5.0f;
        /// <summary>
        /// XY轴宽度
        /// </summary>
        [Range(2f, 20f)] public float XYAxisWidth = 2.0f;
        /// <summary>
        /// XY轴颜色
        /// </summary>

    }

    /// <summary>
    /// 函数公式
    /// </summary>
    [Serializable]
    public class FunctionFormula
    {
        /// <summary>
        /// 函数表达式
        /// </summary>
        public Func<float, float> Formula;
        /// <summary>
        /// 函数图对应线条颜色
        /// </summary>
        public Color FormulaColor;
        public float FormulaWidth;

        public FunctionFormula() { }
        public FunctionFormula(Func<float, float> formula, Color formulaColor, float width)
        {
            Formula = formula;
            FormulaColor = formulaColor;
            FormulaWidth = width;
        }

        public Vector2 GetResult(float xValue, float scaleValue)
        {
            return new Vector2(xValue, Formula(xValue / scaleValue) * scaleValue);
        }
    }

    public class FunctionalGraph : MaskableGraphic
    {
        public FunctionalGraphBase GraphBase = new FunctionalGraphBase();
        public List<FunctionFormula> Formulas;
        private RectTransform _myRect;
        private Vector2 _xPoint;
        private Vector2 _yPoint;


        /// <summary>
        /// 初始化函数信息
        /// </summary>
        public void Init()
        {
            _myRect = this.rectTransform;
            Formulas = new List<FunctionFormula>
            {
              // new FunctionFormula(Mathf.Sin, Color.red, 2.0f),
              // new FunctionFormula(Mathf.Cos, Color.green, 2.0f),
               // new FunctionFormula(Mathf.Log10,Color.yellow,2.0f)
             new FunctionFormula(xValue=>-(xValue-3/2)*(xValue-3/2)+3/2,Color.green,2.0f)
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
            FunctionSetX.vh2 = vh;


            #region 函数图的绘制
            FunctionSetX._myRect = _myRect;
            FunctionSetX.GraphBase = GraphBase;
            FunctionSetX.Formulas = Formulas;
            drawline();

            #endregion
        }

        public static void drawline()
        {
            //FunctionSetX._myRect;
            //FunctionSetX.GraphBase = new FunctionalGraphBase();
            //FunctionSetX.vh2.Clear();//坐标轴

            Debug.Log("this is in drawline of x!" + FunctionSetX.Formulas.ToString());
            float unitPixel = 100 / FunctionSetX.GraphBase.ScaleValue;
            foreach (var functionFormula in FunctionSetX.Formulas)
            {
                //FunctionSetX.x
                Vector2 startPos = functionFormula.GetResult(-17 ,FunctionSetX.GraphBase.ScaleValue);
                for (float x = -FunctionSetX._myRect.sizeDelta.x /6.0f +0; x < FunctionSetX.x -50f; x += unitPixel)
                {
                    Vector2 endPos = functionFormula.GetResult(x, FunctionSetX.GraphBase.ScaleValue);
                    FunctionSetX.vh2.AddUIVertexQuad(FunctionalGraph.GetQuad(startPos, endPos, functionFormula.FormulaColor, functionFormula.FormulaWidth));
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
