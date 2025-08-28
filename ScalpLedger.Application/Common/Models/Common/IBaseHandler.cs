namespace ScalpLedger.Application.Common.Models.Common;

public interface IBaseHandler<in TRequest> : IRequestHandler<TRequest, BaseResult<long>> where TRequest : IBaseCommand;

public interface IBaseQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, BaseResult<TResponse>>
    where TRequest : IBaseQuery<TResponse> where TResponse : class;

// public interface IBaseSearchHandler<in TRequest, TResponse> :
//     IRequestHandler<TRequest, SearchResult<TResponse>> where TRequest : IBaseSearchQuery<TResponse>
//     where TResponse : class;