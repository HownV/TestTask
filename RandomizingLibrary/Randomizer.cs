using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RandomizingLibrary
{
    public static class Randomizer
    {
        private static readonly Random Random = new Random();

        public static byte[] GetColorAsByteArray()
        {
            var color = new byte[4];
            Random.NextBytes(color);
            color[0] = 255;
            return color;
        }

        public static Point GetPoint(int maxX, int maxY)
        {
            Point point = new Point();
            point.X = Random.Next(maxX);
            point.Y = Random.Next(maxY);
            return point;
        }

        public static Point GetPoint(int maxValue)
        {
            return GetPoint(maxValue, maxValue);
        }

        public static int GetInt32(int maxValue)
        {
            int result = Random.Next(maxValue);
            return result;
        }

        public static int GetInt32(int minValue, int maxValue)
        {
            int result = Random.Next(minValue, maxValue);
            return result;
        }
    }
}
