using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace productuploader.Logic
{
    public class QueueLogic
    {
        private const string queueName = "egressevents";

        public static string PutMessageOnQueue(string fileName)
        {
            string queueName = string.Empty;
            try
            {
                string message = $"File path to image: {fileName}";

                var queue = GetCloudQueue();

                if (queue != null)
                    queueName = queue.Name;
                else
                    queueName = "queue is null";

                CloudQueueMessage queueMessage = new CloudQueueMessage(message);
                queue.AddMessage(queueMessage);

                InsightsLogic.LogInfo(string.Format("Message added to queue {0}. Message is: {1}.", queueName, message));

                return string.Format("Uploaded to queue with message: '{0}'", queueMessage.AsString);
            }
            catch (Exception e)
            {
                InsightsLogic.LogError(string.Format("Something went wrong when putting message to queue: {0}.", queueName), e);
                throw e;
            }
        }

        private static CloudQueue GetCloudQueue()
        {
            CloudStorageAccount storageAccount = GetStorageAccount();

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();

            return queue;
        }

        private static CloudStorageAccount GetStorageAccount()
        {
            string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

            return CloudStorageAccount.Parse(storageConnectionString);
        }
    }
}