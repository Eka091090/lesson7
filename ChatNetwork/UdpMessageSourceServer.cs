﻿
using ChatCommLib;
using CommonLib;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatNetwork
{
    public class UdpMessageSourceServer : IMessageSource
    {
        private readonly RouterSocket _routerSocket = new();

        public UdpMessageSourceServer() 
        {
            _routerSocket.Bind($"tcp://*:{12345}");
        }

        public MessageUdp Receive(ref string? clientID)
        {
            string messageReceived = _routerSocket.ReceiveFrameString();    

            return MessageUdp.FromJson(messageReceived) ?? new MessageUdp();
        }

        public void Send(MessageUdp message, string clientId)
        {

            _routerSocket.SendMoreFrame(clientId);


            _routerSocket.SendMoreFrame(""); 

            _routerSocket.SendFrame(Encoding.UTF8.GetBytes(message.ToJson()));
        }
    }
}
