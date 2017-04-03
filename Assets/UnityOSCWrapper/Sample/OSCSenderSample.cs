using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using info.shibuya24.osc;

public class OSCSenderSample : MonoBehaviour 
{
    OSCSender m_sender; 
    public Slider slider;

    void Start () 
    {
        m_sender = new OSCSender ();
        // 適宜IPアドレスを調整してください
        m_sender.Init ("TestClient", 8890, IPAddress.Parse ("192.168.3." + Config.ipNum));


        var e = new Slider.SliderEvent ();
        e.AddListener (x => m_sender.Send ("/changeValue", x));
        slider.onValueChanged = e;
    }
}
