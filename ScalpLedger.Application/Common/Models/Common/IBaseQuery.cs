namespace ScalpLedger.Application.Common.Models.Common;

public interface IBaseQuery;

public interface IBaseQuery<TResponse> : IRequest<BaseResult<TResponse>> where TResponse : class;

// public interface IBaseSearchQuery<TResponse> : IRequest<SearchResult<TResponse>>
//     where TResponse : class;