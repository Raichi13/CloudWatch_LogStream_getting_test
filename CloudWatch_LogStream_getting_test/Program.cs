using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using System;
using System.Configuration;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;

namespace CloudWatch_LogStream_getting_test
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var accessKey = ConfigurationManager.AppSettings["accessKey"];
            var secretAccessKey = ConfigurationManager.AppSettings["secretAccessKey"];
            var region = Amazon.RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["region"]);
            var groupName = ConfigurationManager.AppSettings["groupName"];
            var cloudWatchLogClient = new AmazonCloudWatchLogsClient(accessKey, secretAccessKey, region);
            var reqestCounter = 0;
            BigInteger firstLogStreamName = 0;
            DescribeLogStreamsResponse lastResponse = new DescribeLogStreamsResponse();
            var isEndOfStream = false;
            while (isEndOfStream == false)
            {
                var describeLogStreamsRequest = (reqestCounter == 0)
                    ? new DescribeLogStreamsRequest(groupName) { Limit = 50 }
                    : new DescribeLogStreamsRequest(groupName) { Limit = 50, NextToken = lastResponse.NextToken };
                var describeLogStreamsResponse = await cloudWatchLogClient.DescribeLogStreamsAsync(describeLogStreamsRequest);
                var logStreams = describeLogStreamsResponse.LogStreams;
                for (int j = 0; j < logStreams.Count; j++)
                {
                    //0は正の符号
                    var i = BigInteger.Parse("0" + logStreams[j].LogStreamName, NumberStyles.HexNumber);
                    if (firstLogStreamName < i)
                    {
                        Console.WriteLine(logStreams[j].LogStreamName);
                    }
                    else
                    {
                        isEndOfStream = true;
                        break;
                    }
                }
                firstLogStreamName = (reqestCounter == 0)
                    ? BigInteger.Parse("0" + describeLogStreamsResponse.LogStreams[0].LogStreamName, NumberStyles.HexNumber)
                    : firstLogStreamName;
                lastResponse = describeLogStreamsResponse;
                reqestCounter++;
            }
            Console.ReadKey();
        }
    }
}
