
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

    public class FunctionalGraphBase1
    {
        /// <summary>
        /// 是否显示刻度
        /// </summary>
        public bool ShowScale = false;
        /// <summary>
        /// 是否显示XY轴单位
        /// </summary>
        public bool ShowXYAxisUnit = true;
        /// <summary>
        /// X轴单位
        /// </summary>
        public string XAxisUnit = "XUnit";
        /// <summary>
        /// Y轴单位
        /// </summary>
        public string YAxisUnit = "YUnit";
        /// <summary>
        /// 单位字体大小
        /// </summary>
       
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color FontColor = Color.black;
        /// <summary>
        /// XY轴刻度
        /// </summary>
        [Range(20f, 100f)] public float ScaleValue = 50f;
        /// <summary>
        /// 刻度的长度
        /// </summary>
        [Range(2, 10)] public float ScaleLenght = 6.0f;
        /// <summary>
        /// XY轴宽度
        /// </summary>
        [Range(2f, 20f)] public float XYAxisWidth = 2.0f;
        /// <summary>
        /// XY轴颜色
        /// </summary>
        public Color XYAxisColor = Color.gray;


    }




    public class Functionalzhou : MaskableGraphic
    {
        public FunctionalGraphBase1 GraphBase = new FunctionalGraphBase1();
        public List<FunctionFormula> Formulas;
        private RectTransform _myRect;
       // private Vector2 _xPoint;
       // private Vector2 _yPoint;

        private void OnGUI()
        {
          
        }

        /// <summary>
        /// 初始化函数信息
        /// </summary>
        public void Init()
        {
            _myRect = this.rectTransform;
            Formulas = new List<FunctionFormula>
            {
                new FunctionFormula(Mathf.Sin, Color.red, 2.0f),
                new FunctionFormula(Mathf.Cos, Color.green, 2.0f),
                new FunctionFormula(Mathf.Log10,Color.yellow,2.0f)
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
            #region 基础框架的绘制

            //绘制X轴
            float lenght = _myRect.sizeDelta.x;
            Vector2 leftPoint = new Vector2(0, 0);
            Vector2 rightPoint = new Vector2(lenght / 0.7f, 0);
            vh.AddUIVertexQuad(GetQuad(leftPoint, rightPoint, GraphBase.XYAxisColor, GraphBase.XYAxisWidth));
            // 绘制X轴的箭头
            float arrowUnit = GraphBase.XYAxisWidth * 3;
            Vector2 firstPointX = rightPoint + new Vector2(0, arrowUnit);
            Vector2 secondPointX = rightPoint;
            Vector2 thirdPointX = rightPoint + new Vector2(0, -arrowUnit);
            Vector2 fourPointX = rightPoint + new Vector2(Mathf.Sqrt(3) * arrowUnit, 0);
            vh.AddUIVertexQuad(GetQuad(firstPointX, secondPointX, thirdPointX, fourPointX, GraphBase.XYAxisColor));
            //绘制Y轴
            float height = _myRect.sizeDelta.y;
            Vector2 downPoint = new Vector2(0, 0);
            Vector2 upPoint = new Vector2(0, height / 1.0f);
            vh.AddUIVertexQuad(GetQuad(downPoint, upPoint, GraphBase.XYAxisColor, GraphBase.XYAxisWidth));
            // 绘制Y轴的箭头
            Vector2 firstPointY = upPoint + new Vector2(arrowUnit, 0);
            Vector2 secondPointY = upPoint;
            Vector2 thirdPointY = upPoint + new Vector2(-arrowUnit, 0);
            Vector2 fourPointY = upPoint + new Vector2(0, Mathf.Sqrt(3) * arrowUnit);
            vh.AddUIVertexQuad(GetQuad(firstPointY, secondPointY, thirdPointY, fourPointY, GraphBase.XYAxisColor));

           /* if (GraphBase.ShowXYAxisUnit)
            {
                _xPoint = rightPoint;
                _yPoint = upPoint;
            }*/

            #region 刻度的绘制

            if (GraphBase.ShowScale)
            {
                // X 轴的正方向
                for (int i = 1; i * GraphBase.ScaleValue < _myRect.sizeDelta.x / 2.0f; i++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(GraphBase.ScaleValue * i, 0);
                    Vector2 secongPoint = firstPoint + new Vector2(0, GraphBase.ScaleLenght);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, GraphBase.XYAxisColor));
                }
                // X 轴的负方向
                for (int i = 1; i * -GraphBase.ScaleValue > -_myRect.sizeDelta.x / 2.0f; i++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(-GraphBase.ScaleValue * i, 0);
                    Vector2 secongPoint = firstPoint + new Vector2(0, GraphBase.ScaleLenght);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, GraphBase.XYAxisColor));
                }
                // Y 轴正方向
                for (int y = 1; y * GraphBase.ScaleValue < _myRect.sizeDelta.y / 2.0f; y++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(0, y * GraphBase.ScaleValue);
                    Vector2 secongPoint = firstPoint + new Vector2(GraphBase.ScaleLenght, 0);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, GraphBase.XYAxisColor));
                }
                // Y 轴负方向
                for (int y = 1; y * -GraphBase.ScaleValue > -_myRect.sizeDelta.y / 2.0f; y++)
                {
                    Vector2 firstPoint = Vector2.zero + new Vector2(0, y * -GraphBase.ScaleValue);
                    Vector2 secongPoint = firstPoint + new Vector2(GraphBase.ScaleLenght, 0);
                    vh.AddUIVertexQuad(GetQuad(firstPoint, secongPoint, GraphBase.XYAxisColor));
                }
            }

            #endregion

            #endregion


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

        //绘制虚线
        private void GetImaglinaryLine(ref VertexHelper vh, Vector2 first, Vector2 second, Color color0, float imaginaryLenght, float spaceingWidth, float lineWidth = 2.0f)
        {
            if (first.y.Equals(second.y)) //  X轴
            {
                Vector2 indexSecond = first + new Vector2(imaginaryLenght, 0);
                while (indexSecond.x < second.x)
                {
                    vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    first = indexSecond + new Vector2(spaceingWidth, 0);
                    indexSecond = first + new Vector2(imaginaryLenght, 0);
                    if (indexSecond.x > second.x)
                    {
                        indexSecond = new Vector2(second.x, indexSecond.y);
                        vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    }
                }
            }
            if (first.x.Equals(second.x)) //  Y轴
            {
                Vector2 indexSecond = first + new Vector2(0, imaginaryLenght);
                while (indexSecond.y < second.y)
                {
                    vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    first = indexSecond + new Vector2(0, spaceingWidth);
                    indexSecond = first + new Vector2(0, imaginaryLenght);
                    if (indexSecond.y > second.y)
                    {
                        indexSecond = new Vector2(indexSecond.x, second.y);
                        vh.AddUIVertexQuad(GetQuad(first, indexSecond, color0));
                    }
                }
            }
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