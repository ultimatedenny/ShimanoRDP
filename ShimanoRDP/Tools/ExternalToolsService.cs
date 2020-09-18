using System.Linq;
using ShimanoRDP.Tools.CustomCollections;

namespace ShimanoRDP.Tools
{
    public class ExternalToolsService
    {
        public FullyObservableCollection<ExternalTool> ExternalTools { get; set; } =
            new FullyObservableCollection<ExternalTool>();

        public ExternalTool GetExtAppByName(string name)
        {
            return ExternalTools.FirstOrDefault(extA => extA.DisplayName == name);
        }
    }
}