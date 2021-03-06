#nullable disable
namespace deliverycart.Models
{
    public class Vendor : User
    {
        public int VendorID {get;set;}      
        public string storeAddress {get;set;}= default!;
        public string storeCity {get;set;}= default!;
        public string storeState {get;set;}= default!;
        public int storeZip {get;set;}
        public List<Item> Items {get;set;} = default!;
        public Item Item {get;set;} = default!;
        
    }
}