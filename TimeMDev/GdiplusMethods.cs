﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace TimeMDev
{
    public class GdiplusMethods
    {
        private GdiplusMethods() { }

        private enum DriverStringOptions
        {
            CmapLookup = 1,
            Vertical = 2,
            Advance = 4,
            LimitSubpixel = 8,
        }

        public static void DrawDriverString(Graphics graphics, string text,
            Font font, Brush brush, PointF[] positions)
        {
            DrawDriverString(graphics, text, font, brush, positions, null);
        }

        public static void DrawDriverString(Graphics graphics, string text,
            Font font, Brush brush, PointF[] positions, Matrix matrix)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (text == null)
                throw new ArgumentNullException("text");
            if (font == null)
                throw new ArgumentNullException("font");
            if (brush == null)
                throw new ArgumentNullException("brush");
            if (positions == null)
                throw new ArgumentNullException("positions");

            // Get hGraphics
            FieldInfo field = typeof(Graphics).GetField("nativeGraphics",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hGraphics = (IntPtr)field.GetValue(graphics);

            // Get hFont
            field = typeof(Font).GetField("nativeFont",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hFont = (IntPtr)field.GetValue(font);

            // Get hBrush
            field = typeof(Brush).GetField("nativeBrush",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hBrush = (IntPtr)field.GetValue(brush);

            // Get hMatrix
            IntPtr hMatrix = IntPtr.Zero;
            if (matrix != null)
            {
                field = typeof(Matrix).GetField("nativeMatrix",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                hMatrix = (IntPtr)field.GetValue(matrix);
            }

            int result = GdipDrawDriverString(hGraphics, text, text.Length,
                hFont, hBrush, positions, (int)DriverStringOptions.CmapLookup, hMatrix);
        }

        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal extern static int GdipMeasureDriverString(IntPtr graphics,
            string text, int length, IntPtr font, PointF[] positions,
            int flags, IntPtr matrix, ref RectangleF bounds);

        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal extern static int GdipDrawDriverString(IntPtr graphics,
            string text, int length, IntPtr font, IntPtr brush,
            PointF[] positions, int flags, IntPtr matrix);





        public static int IsChineseLetter(char str)
        {
            int num = (int)str;
            if (num >= 0x4e00 && num <= 0x9fff)//中文
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static int IsChineseLetter(string letter, int index)
        {
            int num = Char.ConvertToUtf32(letter, index);
            if (num >= 0x4e00 && num <= 0x9fff)//中文
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    
    
    }
}
