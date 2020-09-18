using System.Drawing;

namespace ShimanoRDP.UI.GraphicsUtilities
{
    public interface IGraphicsProvider
    {
        SizeF GetResolutionScalingFactor();
    }
}