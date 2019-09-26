using CorujasDev.Schedule.CosmosDb.Common.Util.Config;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Net;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Migration
{
    public class DCCodeFirst
    {
        private static string nameCollection;
        private static string[] Collections = { "UserEntitys" };

        
        private static DocumentClient Client()
        {
            DocumentClient client = new DocumentClient(new Uri("https://localhost:8081"),
                                                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            return client;
        }

        public static void CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                Client().ReadDatabaseAsync(UriFactory.CreateDatabaseUri(ConfigurationManager.AppSetting["CosmosDB:DatabaseName"]));
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    Client().CreateDatabaseAsync(new Database { Id = ConfigurationManager.AppSetting["CosmosDB:DatabaseName"] });
                }
                else
                {
                    throw;
                }
            }
        }


        public static void CreateDocumentCollectionIfNotExistsAsync()
        {
            try
            {
                foreach (var collection in Collections)
                {
                    nameCollection = collection;
                    Client().ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(ConfigurationManager.AppSetting["CosmosDB:DatabaseName"], collection));
                }
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = nameCollection;

                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    Client().CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(ConfigurationManager.AppSetting["CosmosDB:DatabaseName"]),
                        new DocumentCollection { Id = nameCollection },
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
