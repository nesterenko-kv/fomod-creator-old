using System;
using FomodInfrastructure.Interfaces;

namespace MainApplication.Services
{
    public abstract class BaseRepository<TData> : IRepository<TData> where TData : IData
    {
        public TData Data { get; private set; }

        public Result<TData> Create(string path)
        {
            Result<TData> result;
            try
            {
                Data = CreateData(path);
                result = new Result<TData>(Data);
            }
            catch (Exception e)
            {
                result = new Result<TData>(e);
            }
            return result;
        }
        
        public Result<TData> Load(string path)
        {
            Result<TData> result;
            try
            {
                Data = LoadData(path);
                result = new Result<TData>(Data);
            }
            catch (Exception e)
            {
                result = new Result<TData>(e);
            }
            return result;
        }

        
        public Result<TData> Save(string path)
        {
            Result<TData> result;
            try
            {
                SaveData(Data, path);
                result = new Result<TData>(Data);
            }
            catch (Exception e)
            {
                result = new Result<TData>(e);
            }
            return result;
        }
        
        protected abstract TData LoadData(string path);
        protected abstract TData CreateData(string path);
        protected abstract void SaveData(TData data, string path);
    }
}