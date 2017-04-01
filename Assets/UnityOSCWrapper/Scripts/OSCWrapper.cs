using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityOSC;

namespace info.shibuya24.osc
{
    public class OSCWrapper
    {
        public const string VERSION = "0.0.1";
    }
    
    /// <summary>
    /// UnityOSC送信クラス
    /// </summary>
    public class OSCSender
    {
        string m_clientId;
        bool m_isInit;
        
        public void Init (string clientId, int port, IPAddress ip = null)
        {
            m_isInit = true;
            m_clientId = clientId;
            if (ip == null) 
            {
                ip = IPAddress.Parse (Network.player.ipAddress);
            }
            OSCHandler.Instance.CreateClient (clientId, ip, port);
        }
        
        public void Send<T> (string address, T value)
        {
            if (m_isInit == false)
                return;
            OSCHandler.Instance.SendMessageToClient (m_clientId, address, value);
        }
    }
    
    /// <summary>
    /// UnityOSC受信側クラス
    /// </summary>
    public class OSCReceiver
    {
        public event System.Action<OSCPacket> onListenToOSCMessage;
        
        bool m_isInit;
        OSCPacket m_lastPacket;
        public void Init (string serverId, int port)
        {
            if (m_isInit)
                return;
            m_isInit = true;
            OSCHandler.Instance.CreateServer (serverId, port);
        }
        
        public void UpdateListen () 
        {
            ListenToOSCMessage();
        }
        
        void ListenToOSCMessage()
        {
            if (m_isInit == false)
                return;
            OSCHandler.Instance.UpdateLogs ();
            var servers = OSCHandler.Instance.Servers;
            if (servers == null)
                return;
            
            foreach (var item in servers.Values) 
            {
                if (servers.Count <= 0 || item.packets.Count <= 0)
                    continue;
                int lastPacketIndex = item.packets.Count - 1;
                var tmp = item.packets [lastPacketIndex];
                if (m_lastPacket == null || 
                    m_lastPacket.TimeStamp != tmp.TimeStamp || 
                    m_lastPacket.Address != tmp.Address)
                {
                    m_lastPacket = tmp;
                    if (onListenToOSCMessage != null)
                        onListenToOSCMessage (m_lastPacket);
                }
            }
            OSCHandler.Instance.UpdateLogs ();
        }
    }
}