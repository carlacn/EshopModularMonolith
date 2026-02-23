using MediatR;

namespace Shared.CQRS;

public interface IQuery<out TResult> : IRequest<TResult>
    where TResult : notnull;
