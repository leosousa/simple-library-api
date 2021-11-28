using Polly;
using Polly.Retry;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;
using System.Data.SqlClient;

namespace SimpleLibrary.Application.Services.Base;

public class HandlerBase
{
    private const int MaxRetries = 3;
    protected readonly IUnitWork _unitWork;
    protected readonly AsyncRetryPolicy _retryPolicy;

    public HandlerBase(IUnitWork unitOfWork)
    {
        _unitWork = unitOfWork;
        _retryPolicy = Policy.Handle<SqlException>()
            .WaitAndRetryAsync(MaxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));
    }
}
