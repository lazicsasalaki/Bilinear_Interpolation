using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinear_Interpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bilinear interpolation is a method that, from four nearest points with known values,
            //determines the average value at a point where the value is unknown.

            //## tp 45.76861111 16.26805556

            //** t1 45.76666667 16.26666667 - 195.4 121.7
            //** t2 45.76666667 16.275 - 201.9 113.4
            //** t3 45.27083333 16.275 - 156.4 51.3
            //** t4 45.77083333 16.26666667 - 310.4 222.3

            double fiTrazena = 0.0;
            double laTrazena = 0.0;

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(@"..\lazic.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                List<double> fi = new List<double>();
                List<double> la = new List<double>();
                List<double> dx = new List<double>();
                List<double> dy = new List<double>();

                while ((line = streamReader.ReadLine()) != null)
                {

                    // Split input string into lines
                    string[] lines = line.Split(' ', '\t', '|', '#', '$', '%', '&', '(', ')', '=', ';', '_', '\n', ',');

                    // Process line
                    if (line.Contains("##"))
                    {
                        fiTrazena = Convert.ToDouble(lines[4]);
                        laTrazena = Convert.ToDouble(lines[5]);
                    }
                    if (line.Contains("**"))
                    {
                        fi.Add(Convert.ToDouble(lines[2]));
                        la.Add(Convert.ToDouble(lines[3]));
                        dx.Add(Convert.ToDouble(lines[4]));
                        dy.Add(Convert.ToDouble(lines[5]));
                    }
                }

                double deltaLA = la[1] - la[0];
                double deltaFI = fi[3] - fi[0];

                double X = (laTrazena - la[0]) / deltaLA;
                double Y = (fiTrazena - fi[0]) / deltaFI;

                double a0 = dx[0];
                double b0 = dy[0];
                double a1 = dx[1] - dx[0];
                double b1 = dy[1] - dy[0];
                double a2 = dx[3] - dx[0];
                double b2 = dy[3] - dy[0];
                double a3 = dx[0] + dx[2] - (dx[1] + dx[3]);
                double b3 = dy[0] + dy[2] - (dy[1] + dy[3]);

                double dxT = a0 + a1 * X + a2 * Y + a3 * X * Y;
                double dyT = b0 + b1 * X + b2 * Y + b3 * X * Y;
            }
        }
    }
}
