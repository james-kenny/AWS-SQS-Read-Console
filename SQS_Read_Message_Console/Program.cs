using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQS_Read_Message_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting..");


            string sQueue = "https://sqs.eu-west-1.amazonaws.com/{CODE}/{QUEUENAME}"; // Region / CODE and QueueName

            try
            {
                var config = new AmazonSQSConfig()
                {
                    ServiceURL = "https://sqs.eu-west-1.amazonaws.com/" // Region and URL
                };
                
                AmazonSQSClient _client = new AmazonSQSClient("ACCESSKEY", "ACCESSSECRET", config);

                ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();

                receiveMessageRequest.QueueUrl = sQueue;

                ReceiveMessageResponse receiveMessageResponse = _client.ReceiveMessage(receiveMessageRequest);
                
                foreach (var oMessage in receiveMessageResponse.Messages)
                {
                    Console.WriteLine(oMessage.Body);






                    // Delete the message from the queue
                    DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest();

                    deleteMessageRequest.QueueUrl = sQueue;
                    deleteMessageRequest.ReceiptHandle = oMessage.ReceiptHandle;

                    DeleteMessageResponse response = _client.DeleteMessage(deleteMessageRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }



            Console.WriteLine("Complete");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
