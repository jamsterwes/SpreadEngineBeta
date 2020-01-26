using System;
using System.Runtime.InteropServices;

namespace SpreadRuntime.Utilities
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct Color
    {
        public float r, g, b, a;

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(string hex)
        {
            if (hex.StartsWith("#")) hex = hex.Remove(0, 1);

            r = HexToFloat(hex.Substring(0, 2));
            g = HexToFloat(hex.Substring(2, 2));
            b = HexToFloat(hex.Substring(4, 2));
            if (hex.Length == 8)
            {
                a = HexToFloat(hex.Substring(6, 2));
            }
            else
            {
                a = 1.0f;
            }
        }

        public static Color FromHSV(float H, float S, float V)
        {
            HsvToRgb(H, S, V, out double r, out double g, out double b);
            return new Color((float)r, (float)g, (float)b, 1.0f);
        }

        static float HexToFloat(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber) / 255.0f;
        }

        static void HsvToRgb(double h, double S, double V, out double r, out double g, out double b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }

            r = R;
            g = G;
            b = B;
        }
    }
}
