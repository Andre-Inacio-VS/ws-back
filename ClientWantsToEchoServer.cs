using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fleck;
using lib;

namespace ws;

public class ClientWantsToEchoServerDto : BaseDto
{
    public string messageContent { get; set; }
}

public class ClientWantsToEchoServer : BaseEventHandler<ClientWantsToEchoServerDto>
{
    public override Task Handle(ClientWantsToEchoServerDto dto, IWebSocketConnection socket)
    {
        var echo = new ServerEchosClient()
        {
            echoValue = "echo: " + dto.messageContent
        };
        var messageToClient = JsonSerializer.Serialize(echo);
        socket.Send(messageToClient);

        return Task.CompletedTask;
    }
}


public class ServerEchosClient
{
    public string echoValue { get; set; }
}