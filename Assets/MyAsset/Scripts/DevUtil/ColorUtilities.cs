using System;

namespace MyAsset.Scripts.DevUtil
{
    public class ColorUtil
    {
        private double[,] m = new double[3,3]{ { 0.4124f, 0.3576f, 0.1805f },
            { 0.2126f, 0.7152f, 0.0722f },
            { 0.0193f, 0.1192f, 0.9505f } };
        
        private double GetX(double r, double g, double b){
            return m[0,0] * r + m[0,1] * g + m[0,2] * b;
        }
        private double GetY(double r, double g, double b){
            return m[1,0] * r + m[1,1] * g + m[1,2] * b;
        }
        private double GetZ(double r, double g, double b){
            return m[2,0] * r + m[2,1] * g + m[2,2] * b;
        }
        
        private double[] RGB2XYZ(double[] c){
            return new double[]{
                GetX(c[0], c[1], c[2]),
                GetY(c[0], c[1], c[2]),
                GetZ(c[0], c[1], c[2])
            };
        }
        
        public double[] XYZ2Lab(double[] xyz){
            double[] dst = new double[3];
            double[] whitePoint = RGB2XYZ(new double[]{ 255, 255, 255 });
            double xxn = Func(xyz[0] / whitePoint[0]);
            double yyn = Func(xyz[1] / whitePoint[1]);
            double zzn = Func(xyz[2] / whitePoint[2]);
            dst[0] = 116f * yyn - 16;
            dst[1] = 500 * (xxn - yyn);
            dst[2] = 200 * (yyn - zzn);

            return dst;
        }
        
        private double Func(double t){
            if(t > Math.Pow(6d/29, 3)){
                return Math.Pow(t, 1d/3);
            } else {
                return 1d/3 * 29d/6 * 29d/6 * t + 4d/29;
            }
        }

        public double RadianToDegree(double radian)
        {
            return radian * (180 / Math.PI);
        }

        public static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }

        public double CalcColorDifferent(double[] src, double[] sample)
        {
            double[] labSrc = XYZ2Lab(RGB2XYZ(src));
            double[] labSample = XYZ2Lab(RGB2XYZ(sample));
            return Cie94(labSrc, labSample);
        }

        private double Cie94(double[] lab1, double[] lab2)
        {
            //https://en.wikipedia.org/wiki/Color_difference#CIE94

            double kL = 1, kC = 1, kH = 1;
            double K1, K2;
            if (kL == 1)
            {
                K1 = 0.045;
                K2 = 0.015;
            }
            else
            {
                K1 = 0.048;
                K2 = 0.014;
            }

            var deltaL = lab1[0] - lab2[0];
            var C1 = Math.Sqrt(Math.Pow(lab1[1], 2) + Math.Pow(lab1[2], 2));
            var C2 = Math.Sqrt(Math.Pow(lab2[1], 2) + Math.Pow(lab2[2], 2));
            var deltaCab = C1 - C2;
            var deltaa = lab1[1] - lab2[1];
            var deltab = lab1[2] - lab2[2];
            var deltaHab = Math.Sqrt(
                Math.Pow(deltaa, 2) +
                Math.Pow(deltab, 2) -
                Math.Pow(deltaCab, 2)
            );
            var SL = 1;
            var SC = 1 + (K1 * C1);
            var SH = 1 + (K2 * C1);

            return Math.Sqrt(
                Math.Pow((deltaL / (kL * SL)), 2) +
                Math.Pow((deltaCab / (kC * SC)), 2) +
                Math.Pow((deltaHab / (kH * SH)), 2)
            );
        }
    }
}
