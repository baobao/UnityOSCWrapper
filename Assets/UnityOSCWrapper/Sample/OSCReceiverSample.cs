using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using info.shibuya24.osc;

public class OSCReceiverSample : MonoBehaviour 
{
    OSCReceiver m_receiver;
    public Noise3 noise;
    public Text receiverIpTxt;

    void Start () 
    {
        // Receiverから起動しないとエラーになります
        m_receiver = new OSCReceiver();
        m_receiver.Init ("TestServer", 8890);
        m_receiver.onListenToOSCMessage += OnListenToOSCMessage;
        receiverIpTxt.text = "IP : " + Network.player.ipAddress;
    }

    void OnListenToOSCMessage (UnityOSC.OSCPacket obj)
    {
        if (obj.Address == "/changeValue")
        {
            noise.horizonValue = (float)obj.Data [0];
        }
    }
}
