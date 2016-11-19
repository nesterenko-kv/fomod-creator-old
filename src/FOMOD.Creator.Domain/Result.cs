namespace FOMOD.Creator.Domain
{
    using System;

    public class Result<TData>
    {
        public Result(TData data)
            : this(null)
        {
            Data = data;
        }

        public Result(Exception exception)
        {
            Exception = exception;
        }

        public TData Data { get; }

        public Exception Exception { get; }

        public bool Success
        {
            get
            {
                return Exception == null;
            }
        }
    }
}
