using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Models
{
    [Index(nameof(QbRecordId))]
    [Index(nameof(Title))] 
    [Index(nameof(Description))] 
    [Index(nameof(SalePrice))] 
    [Index(nameof(Quantity))] 
    [Index(nameof(BrandName))] 
    [Index(nameof(Location))] 
    [Index(nameof(IsDeactivate))] 
    [Index(nameof(Area))] 
    [Index(nameof(RelatedTypeId))] 
    [Index(nameof(TypeName))] 
    [Index(nameof(RelatedCategoryId))] 
    [Index(nameof(CategoryName))] 
    [Index(nameof(RelatedIndustrId))] 
    [Index(nameof(IndustryName))] 
    public class Item : BaseQBEntity
    {
        public string? Title { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string? Description { get; set; }
        public string? Condition { get; set; }
        public string? SerialNo { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public double? SalePrice { get; set; }
        public string? Discount { get; set; }
        public string? BrandName { get; set; }
        public string? AssetNumber { get; set; }
        public string? InvWorkFlowStatus { get; set; }
        public string? Location { get; set; }
        public string? Area { get; set; }
        public string? Menufacturer { get; set; }
        public string? FactoryInformationOnly { get; set; }
        public string? InvYear { get; set; }
        public string? HP_AMP_KW_KVA { get; set; }
        public string? Amps { get; set; }
        public string? Voltage { get; set; }
        public string? Voltage2 { get; set; }
        public string? Phase { get; set; }
        public string? LengthInches { get; set; }
        public string? WidthInches { get; set; }
        public string? HeightInches { get; set; }
        public string? WeightLBS { get; set; }
        public string? EstiamtedPackaginWeight { get; set; }
        public string? EstimatedTotalWeight { get; set; }
        public string? DeliveryDetails { get; set; }
        public string? ReturnDetials { get; set; }
        public string? CoverImageURL { get; set; }
        public string? ItemAddedDate { get; set; }
        public string? ItemUpdateDate { get; set; }
        public Nullable<double> ItemTax { get; set; }
        public string? NewReplacementCostId { get; set; }
        public string? PackagingCostData { get; set; }
        public string? PerItemCost { get; set; }
        public string? ItemMaintenanceCost { get; set; }
        public Nullable<int> RelatedTypeId { get; set; } = 0;
        public string? TypeName { get; set; }
        public Nullable<int> RelatedCategoryId { get; set; } = 0;
        public string? CategoryName { get; set; }
        public Nullable<int> RelatedIndustrId { get; set; } = 0;
        public string? IndustryName { get; set; }
        public bool IsDeactivate { get; set; }
        public string GuaranteeOrAsis { get; set; }
        public string PaymentAccept { get; set; }
        public string Background { get; set; }
        public string SKU { get; set; }
        public string ManuFacturers_Specs { get; set; }
        public double? TotalWeightFt3 { get; set; }
        public double? ShippingClass { get; set; }
        public string? ExamplesforClassRating { get; set; }
        public string? FreightAdditionalOptions { get; set; }
        public bool? ISLocalPickupOnly { get; set; }
        public bool? IsSpecializedFreightTransit { get; set; }
        public string? PackageName { get; set; }
        public string? PackageCode { get; set; }
        public double AutoAccept { get; set; }
        public double AutoReject { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
