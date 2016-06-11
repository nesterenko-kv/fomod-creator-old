using System;

namespace FomodInfrastructure.Interfaces
{
    public class RepositoryResult
    {
        #region Constructors

        public RepositoryResult(Exception exception = null)
        {
            Exception = exception;
        }

        #endregion

        #region Properties

        public Exception Exception { get; }

        public bool Success => Exception == null;

        #endregion
    }

    public class RepositoryResult<TData> : RepositoryResult where TData : IRepositoryData
    {
        #region Properties

        public TData Data { get; }

        #endregion

        #region Constructors

        public RepositoryResult(TData data)
        {
            Data = data;
        }

        public RepositoryResult(Exception exception)
            : base(exception)
        {
            
        }

        #endregion
    }
}