using Newtonsoft.Json.Linq;
using System;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class MessageBase : IMessage
    {
        public MessageBase() {}
        public MessageBase(string json, Type sometype)
        {
            var parsedJson = JObject.Parse(json);
            Type = parsedJson["type"]?.ToString();
            if(string.IsNullOrWhiteSpace(Type))
            {
                throw new ArgumentException("Invalid input");
            }
            var payloadType = sometype.Assembly.GetType(Type);
            Payload = (IMessage)parsedJson["payload"]?.ToObject(payloadType);
        }
        public string Type { get; }

        public IMessage Payload { get; }
    }
}