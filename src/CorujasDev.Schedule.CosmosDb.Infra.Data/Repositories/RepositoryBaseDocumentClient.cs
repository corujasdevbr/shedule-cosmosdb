using CorujasDev.Schedule.CosmosDb.Common.Util.Config;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Contexts;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public abstract class RepositoryBaseDocumentClient<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private string collectionUri;

        public RepositoryBaseDocumentClient()
        {
            collectionUri = UriFactory.CreateDocumentCollectionUri(ConfigurationManager.AppSetting["CosmosDB:DatabaseName"], $"{typeof(TEntity).Name}s").ToString();
        }
        public void Add(TEntity obj)
        {
            try
            {
                using(ScheduleContextDC ctx = new ScheduleContextDC())
                {
                    ctx.client.CreateDocumentAsync(collectionUri, obj).Wait();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                using (ScheduleContextDC ctx = new ScheduleContextDC())
                {
                    return ctx.client.CreateDocumentQuery<TEntity>(collectionUri).ToList<TEntity>();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TEntity GetById(string id)
        {
            try
            {
                using (ScheduleContextDC ctx = new ScheduleContextDC())
                {
                    TEntity item = ctx.client.ReadDocumentAsync<TEntity>($"{collectionUri}/docs/{id}").Result;
                    return item;
                }
            }
            catch (AggregateException ae)
            {
                if (ae.InnerException is DocumentClientException &&
                    (ae.InnerException as DocumentClientException).StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public void Remove(string id)
        {
            try
            {
                using (ScheduleContextDC ctx = new ScheduleContextDC())
                {
                    ctx.client.DeleteDocumentAsync($"{collectionUri}/{id}").Wait();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Update(string id, TEntity obj)
        {
            try
            {
                using (ScheduleContextDC ctx = new ScheduleContextDC())
                {
                    ctx.client.ReplaceDocumentAsync($"{collectionUri}/docs/{id}", obj).Wait();
                }               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
