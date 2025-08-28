// using SharedKernel.Application.Models;
//
// namespace ScalpLedger.Application.Common.Models;
//
// public class SearchResult<T> : BaseResult<T[]> where T : class
// {
//     public Pagination? Pagination { get; }
//
//     private SearchResult(T[] data, Pagination pagination, string? message) : base(data, message)
//     {
//         Pagination = pagination;
//     }
//
//     public static SearchResult<T> WithSuccess(T[] data, Pagination pagination, string? message = null)
//     {
//         return new SearchResult<T>(data, pagination, message);
//     }
// }