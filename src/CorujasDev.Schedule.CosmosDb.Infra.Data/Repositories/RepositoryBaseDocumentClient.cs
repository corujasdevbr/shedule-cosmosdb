using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public abstract class RepositoryBaseDocumentClient<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public  DocumentClient client;
        private Uri collectionUri;
        private string Database = "dbsheduledev";

        public RepositoryBaseDocumentClient() {
            client = new DocumentClient(new Uri(
                                                "https://localhost:8081"),
                                                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
                                       );

            collectionUri = UriFactory.CreateDocumentCollectionUri(Database, typeof(TEntity).Name + "s");
            this.CreateDatabaseIfNotExists(Database).Wait();
            this.CreateDocumentCollectionIfNotExists(Database, typeof(TEntity).Name + "s").Wait();
        }

        public void Add(TEntity obj)
        {
            try
            {
                client.CreateDocumentAsync(collectionUri, obj).Wait();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return client.CreateDocumentQuery<TEntity>(collectionUri).ToList<TEntity>();
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
                TEntity item = client.ReadDocumentAsync<TEntity>(UriFactory.CreateDocumentUri(Database, typeof(TEntity).Name + "s", id).ToString()).Result;
                return item;
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
                client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, typeof(TEntity).Name + "s", id)).Wait();
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
                client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Database, typeof(TEntity).Name + "s", id), obj).Wait();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task CreateDatabaseIfNotExists(string databaseName)
        {
            try
            {
                await this.client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDatabaseAsync(new Database { Id = databaseName });
                }
                else
                {
                    throw;
                }
            }
        }


        private async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            try
            {
                await this.client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = collectionName;

                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    await this.client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        new DocumentCollection { Id = collectionName },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
