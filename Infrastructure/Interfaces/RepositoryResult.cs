using System;

namespace FomodInfrastructure.Interfaces
{
    public class RepositoryResult<TData> where TData : IData
    {
        #region Properties

        public Exception Exception { get; }

        public TData Data { get; }

        public bool Success => Exception == null;

        #endregion
        
        #region Constructors

        public RepositoryResult(TData data): this(null)
        {
            Data = data;
        }

        public RepositoryResult(Exception exception)
        {
            Exception = exception;
        }

        #endregion
    }
}