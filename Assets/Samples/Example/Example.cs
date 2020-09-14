using System.Collections;
using System.Collections.Generic;
using info.shibuya24.osc;
using UnityEngine;
using UnityOSC;

public class Example : MonoBehaviour
{
    private OSCSender _sender;
    private OSCReceiver _receiver;

    // Start is called before the first frame update
    void Start()
    {
        _sender = OSCSender.Create("testClient", 1000, "192.168.11.4");

        _receiver = OSCReceiver.Create("testServer", 1000);
        _receiver.onListenToOSCMessage += OnListenToOSCMessage;
    }

    private void OnListenToOSCMessage(OSCPacket obj)
    {
        Debug.Log($"{obj.Address} / {obj.Data[0]} / {obj.Data.Count} / {obj.TimeStamp}");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Send"))
        {
            _sender.Send("/aaa", "hogehoge");
        }
    }

    void Update()
    {
        _receiver.UpdateListen();
    }
}