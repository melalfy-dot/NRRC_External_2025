using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace NRRC_External_2025.Models.Custom
{
    public class BlockGridContentModel
    {
        public BlockGridItem? BlockGridItem { get; set; }
        public IPublishedContent? PublishedContentItem { get; set; }
        public bool isHomePage { get; set; }
    }
}
