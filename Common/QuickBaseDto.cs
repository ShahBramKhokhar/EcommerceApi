namespace WebRexErpAPI.Services.Common
{
    public class QuickBaseDto
    {
        public List<Dictionary<string, QBDataItem>>? data { get; set; }
        public Dictionary<string, object>? metadata { get; set; }
    }
   

    public class QBDataItem
    {
        public object value { get; set; }
    }

    public class MetaData
    {
        public int totalRecords { get; set; }
    }


   public class CustomerQBDto
    {
        public string? CustomerName { get; set; }
        public string? phoneNumber { get; set; }
        public int? CustomerId { get; set; }
        public int QBId { get; internal set; } = 0;
        public string Address { get; set; }
        public LocationQBDto? CustomerLocation { get;  set; }
    }


    public class SO_ItemQBDto
    {
        public int QBId { get; set; }
        public int OrderId { get; set; }
        public int ItemRecordId { get; set; }
        public int SelectedQty { get; set; }
        public decimal Freight { get; set; } = decimal.Zero;
        public decimal Postal { get; set; } = decimal.Zero;
        public decimal Packing { get; set; } = decimal.Zero;
        public string? ShippingType { get; set; }
    }

    public class QBOrderDto
    {
        public int QBContactID { get; set; }

        public decimal TotalTax { get; set; } = 0;
        public string? date { get; set; }
        public int QBId { get; internal set; }
        public string? OrderNo { get; internal set; }
        public string? StripePaymnetId { get; set; }
        

        public LocationQBDto? CustomerLocation { get; set; }
    }

    public class QBChargeDto
    {
        public decimal Amout { get; set; }
        public string? ChargeType { get; set;}
        public string? PaymentType { get; set; }
        public int OrderId { get; set;}
        public int QBId { get; set;}
    }


    public class ContactQBDto
    {
        public string? ContactName { get; set;}
        public string? ContactTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public LocationQBDto? CustomerLocation { get; set; }

        public int QBId { get; internal set; }

        public bool IsCreateCustomer { get; set; } = false;
        public int CustomerRecordId { get; set; }


    }


    public class LocationQBDto
    {
        public int CustomerRecordId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public int QBId { get;  set; }
    }
}
