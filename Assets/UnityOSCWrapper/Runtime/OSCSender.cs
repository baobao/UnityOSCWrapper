// Copyright (c) 2017 Shunsuke Ohba
// Released under the MIT license
// https://opensource.org/licenses/mit-license.php

using UnityEngine;
using System.Net;

namespace info.shibuya24.osc
{
    /// <summary>
    /// UnityOSC Sender
    /// </summary>
    public class OSCSender
    {
        /// <summary>
        /// initialized flag
        /// </summary>
        public bool IsInit { get; private set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Create Sender Instance
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="port"></param>
        /// <param name="receiverIPAddressString">ex:192.168.11.4</param>
        public static OSCSender Create(string clientId, int port, string receiverIPAddressString)
        {
            var result = new OSCSender();
            result.Init(clientId, port, receiverIPAddressString);
            return result;
        }

        private void Init (string clientId, int port, string receiverIPAddressString)
        {
            try
            {
                var ip = IPAddress.Parse(receiverIPAddressString);
                IsInit = true;
                ClientId = clientId;
                OSCHandler.Instance.CreateClient(clientId, ip, port);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Invalid ip address : {receiverIPAddressString} / {e.Message}");
            }
        }

        /// <summary>
        /// Send Message
        /// value is int, long, float, double, string byte[]
        /// </summary>
        public void Send<T> (string key, T value)
        {
            if (IsInit == false)
            {
                Debug.LogError("First we need to call the Init function");
                return;
            }
            OSCHandler.Instance.SendMessageToClient (ClientId, key, value);
        }
    }
}