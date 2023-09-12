namespace LX.TestSpace.Server.BLL.Models.Records
{
    public record PaginationData<T>
        where T : class
    {
        public IEnumerable<T> FilterData { get; set; }

        public int DataCount { get; set; }
    }
}
