using System;

namespace FinCoreErp.DTO.Asset
{
    public class AssetRegisterDto
    {
        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public int? CapexRequestId { get; set; }

        public int? PurchaseOrderId { get; set; }

        public int? GRNId { get; set; }

        public int? VendorId { get; set; }

        public int? DepartmentId { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchaseCost { get; set; }

        public string Status { get; set; }
    }
}