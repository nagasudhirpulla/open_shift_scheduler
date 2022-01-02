using System.Drawing;

namespace OSS.Domain.Utils;
public class ColorUtils
{
    public static string ToHexValue(Color color)
    {
        // https://stackoverflow.com/questions/14710698/colortranslator-tohtml-return-string-issue
        return "#" + color.R.ToString("X2") +
                        color.G.ToString("X2") +
                        color.B.ToString("X2");
    }
}