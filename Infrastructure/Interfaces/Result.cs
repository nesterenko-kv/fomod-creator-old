using System;

namespace FomodInfrastructure.Interfaces
{
    public class Result<TData> where TData : IData
    {
        #region Properties

        public Exception Exception { get; }

        public TData Data { get; }

        public bool Success => Exception == null;

        #endregion
        
        #region Constructors

        public Result(TData data): this(null)
        {
            Data = data;
        }

        public Result(Exception exception)
        {
            Exception = exception;
        }

        #endregion
    }
}