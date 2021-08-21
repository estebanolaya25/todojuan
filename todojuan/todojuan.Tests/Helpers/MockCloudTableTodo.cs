using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace todojuan.Tests.Helpers
{
    public class MockCloudTableTodo : CloudTable
    {
        public MockCloudTableTodo(Uri tableAdress) : base(tableAdress)
        {
        }

        public MockCloudTableTodo(Uri tableAdress, StorageCredentials credentials) : base(tableAdress,credentials)
        {
        }

        public MockCloudTableTodo(StorageUri tableAdress, StorageCredentials credentials) : base(tableAdress,credentials)
        {
        }

        public override async Task<TableResult> ExecuteAsync(TableOperation operation)
        {
            return await Task.FromResult(new TableResult
            {
                HttpStatusCode = 200,
                Result = TestFactory.GetTodoEntity()

            }); 
        }



    }
}
