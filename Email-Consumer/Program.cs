using Amazon.SQS;
using System;
using Amazon;
using Amazon.SQS.Model;
using Amazon.Runtime;
using System.Linq;

namespace Email_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
            var queueUrl = sqs.GetQueueUrlAsync("EmailQueue").Result.QueueUrl;
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl
            };
            var receiveMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest).Result;
            foreach (var message in receiveMessageResponse.Messages)
            {
                Console.WriteLine("Message \n");
                Console.WriteLine($"Message id  {message.MessageId} \n");
                Console.WriteLine($"RecepientHandle  {message.ReceiptHandle} \n");
                Console.WriteLine($"MSD5Body  {message.MD5OfBody} \n");
                Console.WriteLine($" Body  {message.Body} \n");

                foreach (var attribute in message.Attributes)
                {
                    Console.WriteLine("Attributes \n");
                    Console.WriteLine($"Name  {attribute.Key} \n");
                    Console.WriteLine($"Value {attribute.Value} \n");
                }

                var messageReceiptHandle = receiveMessageResponse.Messages.FirstOrDefault()?.ReceiptHandle;
                var deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = queueUrl,
                    ReceiptHandle = messageReceiptHandle
                };
                sqs.DeleteMessageAsync(deleteRequest);
                Console.ReadLine();

            }
        }
    }
}
