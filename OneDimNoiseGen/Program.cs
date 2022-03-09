using System.Drawing;

namespace OneDimNoiseGen;

internal class Program
{
    public static void Main()
    {
        var graph = new Bitmap(1001, 101);

        //Create the Graph
        for (var x = 0; x < graph.Width; x++)
        for (var y = 0; y < graph.Height; y++)
            if (x % 10 == 0 || y % 10 == 0)
                graph.SetPixel(x, y, Color.DarkGreen);
            else
                graph.SetPixel(x, y, Color.Black);

        graph.Save(@"C:\Users\tyler\RiderProjects\OneDimNoiseGen\OneDimNoiseGen\temp\graph.bmp");

        var points = 40;
        var freq = (graph.Width - 1.00f) / (points - 1.00f);
        

        //Set points on the graph
        var savePointy = new List<int>();
        var savePointx = new List<int>();
        for (var i = 0; i <= points - 1; i++)
        {
            var currentPointy = RandomNum(10, 80);
            var currentPointx = i * freq;

            graph.SetPixel((int) Math.Round(currentPointx), currentPointy, Color.White);
            savePointy.Add(currentPointy);
            savePointx.Add((int) Math.Round(currentPointx));
            
        }

        graph.Save(@"C:\Users\tyler\RiderProjects\OneDimNoiseGen\OneDimNoiseGen\temp\graph.bmp");

        //Liner Interpolation

        for (var i = 0; i <= Math.Round(freq) * (i + 1); i++)
        {
            if (i + 1 > savePointy.Count - 1) break;

            var usingPointx1 = savePointx[i];
            var usingPointy1 = savePointy[i];
            var usingPointx2 = savePointx[i + 1];
            var usingPointy2 = savePointy[i + 1];

            for (var x = savePointx[i]; x <= freq * (i + 1); x++)
                graph.SetPixel(x, (int) Lerp(x, usingPointx1, usingPointy1, usingPointx2, usingPointy2), Color.White);
        }

        graph.Save(@"C:\Users\tyler\RiderProjects\OneDimNoiseGen\OneDimNoiseGen\temp\graph.bmp");
    }

    private static int RandomNum(int min, int max)
    {
        var rd = new Random();
        return rd.Next(min, max + 1);
    }

    private static float Lerp(float point, int x1, int y1, int x2, int y2)
    {
        return (float) (y1 + (point - x1) * (y2 - y1) / (x2 - x1));
    }
}