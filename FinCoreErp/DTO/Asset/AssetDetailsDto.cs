using System;

namespace FinCoreErp.DTO.Asset
{
    public class AssetDetailsDto
    {
        public int AssetId { get; set; }

        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public string VendorCode { get; set; }

        public string DepartmentName { get; set; }

        public string CapexRequestTitle { get; set; }

        public string POCode { get; set; }

        public string GRNCode { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchaseCost { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}