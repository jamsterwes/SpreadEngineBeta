namespace SpreadRuntime.Utilities
{
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

        static float HexToFloat(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber) / 255.0f;
        }
    }
}
