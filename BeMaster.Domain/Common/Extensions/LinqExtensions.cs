// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Domain.Common.Filters;

namespace BeMaster.Domain.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, Pagination pagination) =>
            source.Skip((pagination.PageToken - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

        public static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> source, Pagination pagination) =>
            source.Skip((pagination.PageToken - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
    }
}
