namespace LX.TestSpace.Server.BLL.Models.PaginationModel
{
    public class PaginationModel<T>
        where T : class
    {
        public int ItemsPerPage { get; set; }

        public int SelectedPage { get; set; }

        public int TotalPageCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
