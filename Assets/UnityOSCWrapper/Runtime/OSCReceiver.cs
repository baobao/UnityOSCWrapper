// Copyright (c) 2017 Shunsuke Ohba
// Released under the MIT license
// https://opensource.org/licenses/mit-license.php

using UnityEngine;
using UnityOSC;

namespace info.shibuya24.osc
{
    /// <summary>
    /// UnityOSC Receiver
    /// </summary>
    public class OSCReceiver
    {
        /// <summary>
        /// The event when the OSC message is received
        /// </summary>
        public event System.Action<OSCPacket> onListenToOSCMessage;

        /// <summary>
        /// initialized flag
        /// </summary>
        public bool IsInit { get; private set; }

        /// <summary>
        /// last received packet
        /// </summary>
        private OSCPacket _lastPacket;

        /// <summary>
        /// Create Server Instance
        /// </summary>
        public static OSCReceiver Create(string serverId, int port)
        {
            var result = new OSCReceiver();
            result.Init(serverId, port);
            return result;
        }

        private void Init(string serverId, int port)
        {
            if (IsInit)
            {
                return;
            }

            IsInit = true;

            OSCHandler.Instance.CreateServer(serverId, port);
        }

        /// <summary>
        /// Update message listen
        /// </summary>
        public void UpdateListen()
        {
            if (IsInit == false)
            {
                Debug.LogError("First we need to call the Init function");
                return;
            }

            OSCHandler.Instance.UpdateLogs();
            var servers = OSCHandler.Instance.Servers;
            if (servers == null)
            {
                return;
            }

            foreach (var item in servers.Values)
            {
                if (servers.Count <= 0 || item.packets.Count <= 0)
                {
                    continue;
                }

                int lastPacketIndex = item.packets.Count - 1;
                var tmp = item.packets[lastPacketIndex];
                if (_lastPacket == null ||
                    _lastPacket.TimeStamp != tmp.TimeStamp ||
                    _lastPacket.Address != tmp.Address)
                {
                    _lastPacket = tmp;
                    onListenToOSCMessage?.Invoke(_lastPacket);
                }
            }

            OSCHandler.Instance.UpdateLogs();
        }
    }
}