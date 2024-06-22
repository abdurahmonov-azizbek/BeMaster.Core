// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

namespace BeMaster.Domain.Common.Filters
{
    public class Pagination
    {
        public int PageToken { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
