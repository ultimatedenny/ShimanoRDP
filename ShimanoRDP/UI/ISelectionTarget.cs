using System.Drawing;

namespace ShimanoRDP.UI
{
    public interface ISelectionTarget<out T>
    {
        string Text { get; set; }
        Image Image { get; }
        T Config { get; }
    }
}