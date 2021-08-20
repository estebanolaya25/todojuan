using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using todojuan.Functions.Entities;

namespace todojuan.Functions.Functions
{
    public static class ScheduledFunction
    {
        [FunctionName("ScheduledFunction")]
        public static async Task Run([TimerTrigger("*/1 * * * *")]TimerInfo myTimer,
             [Table("todo", Connection = "AzureWebJobsStorage")] CloudTable todoTable, 
             ILogger log)
        {
            log.LogInformation($"Deleting complete todo function executed at: {DateTime.Now}");
            string filter = TableQuery.GenerateFilterConditionForBool("IsCompleted", QueryComparisons.Equal, true);
            TableQuery<TodoEntity> query = new TableQuery<TodoEntity>().Where(filter);
            TableQuerySegment<TodoEntity> completeTodos = await todoTable.ExecuteQuerySegmentedAsync(query, null);
            int deleted = 0;
            foreach (TodoEntity completedTodo in completeTodos)
            {
                await todoTable.ExecuteAsync(TableOperation.Delete(completedTodo));
                deleted++;
            }

            log.LogInformation($"Deleted: {deleted} item at: {DateTime.Now}");
       
        }

    }
}
