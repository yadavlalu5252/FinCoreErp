using System;

namespace FinCoreErp.DTO.Asset
{
    public class AssetDisposalDto
    {
        public int AssetId { get; set; }

        public DateTime DisposalDate { get; set; }

        public string DisposalReason { get; set; }
    }
}