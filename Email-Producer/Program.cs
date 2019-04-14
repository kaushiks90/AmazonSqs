using Amazon.SQS;
using System;
using Amazon;
using Amazon.SQS.Model;
using Amazon.Runtime;
//using static Amazon.Internal.RegionEndpointProviderV2;

namespace Email_Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Amazon SQS");
            Console.WriteLine("*************************************\n");
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);

            Console.WriteLine("Create a queue called email queue\n");



            var sqsRequest = new CreateQueueRequest
            {
                QueueName = "EmailQueue"
            };

            var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;

            var myQueueUrl = createQueueResponse.QueueUrl;

            var listQueuesRequest = new ListQueuesRequest();
            var listQueuesResponse = sqs.ListQueuesAsync(listQueuesRequest);

            Console.WriteLine("List Amazon queues\n");

            foreach (var queueUrl in listQueuesResponse.Result.QueueUrls)
            {
                Console.WriteLine($"QueueUrl {queueUrl}\n");
            }

            Console.WriteLine("Sending a message to our Email Queue\n");
            var sqsMessageRequest = new SendMessageRequest
            {
                QueueUrl = myQueueUrl,
                MessageBody = "Email information Sent by Kaushik"
            };
            sqs.SendMessageAsync(sqsMessageRequest);

            Console.WriteLine("Finished Sending message to our SQS queue \n");
            Console.ReadLine();
        }
    }
}
