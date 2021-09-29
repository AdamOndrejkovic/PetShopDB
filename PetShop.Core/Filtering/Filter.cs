namespace PetShop.Core.Filtering
{
    public class Filter
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public string Sortorder { get; set; }
        
        public string SortBy { get; set; }
        
        public string Search { get; set; }
    }
}