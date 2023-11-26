namespace BrregAPI.Modals.Response
{
    public class ListResponse<T>
    {
        public ListResponse(List<T> list, int total, int start, int amount)
        {
            Items = list;
            TotalItems = total;
            Start = start;
            End = start + amount;
            Count = amount;
        }

        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Count { get; set; }
    }    
}