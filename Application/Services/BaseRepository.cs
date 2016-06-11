using System;
using System.IO;
using FomodInfrastructure.Interfaces;

namespace MainApplication.Services
{
    public abstract class BaseRepository<TData> : IRepository<TData> where TData : IRepositoryData
    {
        public TData Data { get; private set; }

        public RepositoryResult<TData> Create(string path)
        {
            RepositoryResult<TData> result;
            try
            {
                Data = CreateData(path);
                result = new RepositoryResult<TData>(Data);
            }
            catch (Exception e)
            {
                result = new RepositoryResult<TData>(e);
            }
            AfterCreate(result);
            return result;
        }
        
        public RepositoryResult<TData> Load(string path)
        {
            RepositoryResult<TData> result;
            try
            {
                Data = LoadData(path);
                result = new RepositoryResult<TData>(Data);
            }
            catch (Exception e)
            {
                result = new RepositoryResult<TData>(e);
            }
            AfterLoad(result);
            return result;
        }

        
        public RepositoryResult Save(string path)
        {
            RepositoryResult result;
            try
            {
                SaveData(Data, path);
                result = new RepositoryResult();
            }
            catch (Exception e)
            {
                result = new RepositoryResult(e);
            }
            AfterSave(result);
            return result;
        }

        protected abstract void AfterCreate(RepositoryResult<TData> result);
        protected abstract void AfterLoad(RepositoryResult<TData> result);
        protected abstract void AfterSave(RepositoryResult result);
        protected abstract TData LoadData(string path);
        protected abstract TData CreateData(string path);
        protected abstract void SaveData(TData data, string path);
    }
}