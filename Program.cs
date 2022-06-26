using Amazon;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using System.Text.Json;
using hello_eventbridge_producer;

var client = new AmazonEventBridgeClient(RegionEndpoint.USWest2);

var evt = new TestEvent()
{
    Id = Guid.NewGuid().ToString(),
    Message = "Test event",
    Date = DateTime.Now
};

var response = await client.PutEventsAsync(new PutEventsRequest()
{
    Entries = new List<PutEventsRequestEntry>
    {
        new PutEventsRequestEntry
        {
            DetailType = "test-event",
            EventBusName = "hello-eventbridge",
            Source = "hello-eventbridge/producer",
            Detail = JsonSerializer.Serialize(evt)
        }
    }
});

Console.WriteLine($"Sent test event {evt.Id} to event bus, response status {response.HttpStatusCode}");

