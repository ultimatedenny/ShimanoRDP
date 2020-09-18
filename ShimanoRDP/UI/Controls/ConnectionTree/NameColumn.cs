using BrightIdeasSoftware;
using ShimanoRDP.Connection;

namespace ShimanoRDP.UI.Controls.ConnectionTree
{
    public class NameColumn : OLVColumn
    {
        public NameColumn(ImageGetterDelegate imageGetterDelegate)
        {
            AspectName = "Name";
            FillsFreeSpace = false;
            AspectGetter = item => ((ConnectionInfo)item).Name;
            ImageGetter = imageGetterDelegate;
            AutoCompleteEditor = false;
        }
    }
}