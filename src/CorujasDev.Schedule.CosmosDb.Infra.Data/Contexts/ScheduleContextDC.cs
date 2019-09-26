using Microsoft.Azure.Documents.Client;
using System;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Contexts
{
    public class ScheduleContextDC : IDisposable
    {
        public DocumentClient client;

        public ScheduleContextDC()
        {
            client = new DocumentClient(new Uri(
                                                "https://localhost:8081"),
                                                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
                                       );
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
