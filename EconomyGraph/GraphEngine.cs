using EconomyGraph.Models;
using SkiaSharp;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;

namespace EconomyGraph
{
    public class GraphEngine
    {
        static Dictionary<string, SKPaint> lineBrushes = new Dictionary<string, SKPaint>();
        static Dictionary<string, SKPaint> rectangleBrushes = new Dictionary<string, SKPaint>();
        static Dictionary<string, SKPaint> textBrushes = new Dictionary<string, SKPaint>();

        SKPaint GetLineBrush(GraphLine graphLine)
        {
            SKPaint brush;
            string key = $"{graphLine.Color}{graphLine.StrokeWidth}";
            if (lineBrushes.ContainsKey(key))
            {
                brush = lineBrushes[key];
            }
            else
            {
                brush = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = graphLine.Color,
                    StrokeWidth = graphLine.StrokeWidth
                };
                lineBrushes[key] = brush;
            }
            return brush;
        }

        SKPaint GetRectangleBrush(GraphRectangle graphRectangle)
        {
            SKPaint brush;
            string key = $"{graphRectangle.Style}{graphRectangle.Color}";
            if (rectangleBrushes.ContainsKey(key))
            {
                brush = rectangleBrushes[key];
            }
            else
            {
                brush = new SKPaint
                {
                    Style = graphRectangle.Style == PaintStyle.Fill ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                    Color = graphRectangle.Color
                };
                rectangleBrushes[key] = brush;
            }
            return brush;
        }

        public SKPaint GetTextBrush(GraphText graphText)
        {
            SKPaint brush;
            string key = $"{graphText.Color}{graphText.PointSize}{graphText.Alignment}{graphText.Bold}";
            if (textBrushes.ContainsKey(key))
            {
                brush = textBrushes[key];
            }
            else
            {
                SKTextAlign alignment = SKTextAlign.Left;
                switch(graphText.Alignment)
                {
                    case TextAlignment.End:
                        alignment = SKTextAlign.Right;
                        break;
                    case TextAlignment.Center:
                        alignment = SKTextAlign.Center;
                        break;
                }
                brush = new SKPaint
                {
                    Color = graphText.Color,
                    TextSize = graphText.PointSize,
                    TextAlign = alignment,
                    FakeBoldText = graphText.Bold
                };
                textBrushes[key] = brush;
            }
            return brush;
        }

        public void Draw(SKSurface surface, List<IGraphItem> graphItems)
        {
            SKCanvas canvas = surface.Canvas;
            foreach (var item in graphItems)
            {
                string type = item.GetType().ToString();
                int pos = type.LastIndexOf('.');
                type = type.Substring(pos + 1);
                switch (type)
                {
                    case "GraphClear":
                        GraphClear graphClear = item as GraphClear;
                        canvas.Clear(graphClear.Color);
                        break;
                    case "GraphLine":
                        GraphLine graphLine = item as GraphLine;
                        canvas.DrawLine(graphLine.XPosStart, graphLine.YPosStart, graphLine.XPosEnd, graphLine.YPosEnd, GetLineBrush(graphLine));
                        break;
                    case "GraphRectangle":
                        GraphRectangle graphRectangle = item as GraphRectangle;
                        var rect = SKRect.Create(graphRectangle.XPos, graphRectangle.YPos, graphRectangle.Width, graphRectangle.Height);
                        canvas.DrawRect(rect, GetRectangleBrush(graphRectangle));
                        break;
                    case "GraphText":
                        GraphText graphText = item as GraphText;
                        if (graphText.Rotation == 0)
                        {
                            canvas.DrawText(graphText.Text, graphText.XPos, graphText.YPos, GetTextBrush(graphText));
                        }
                        else
                        {
                            using (new SKAutoCanvasRestore(canvas)) // https://stackoverflow.com/questions/41908497/draw-rotated-text-in-skiasharp
                            {
                                // do any transformations
                                canvas.RotateDegrees(graphText.Rotation, graphText.XPos, graphText.YPos);
                                // do serious work
                                canvas.DrawText(graphText.Text, graphText.XPos, graphText.YPos, GetTextBrush(graphText));
                                // auto restore, even on exceptions or errors
                            }
                        }
                        break;
                }
            }    
        }
    }
}
