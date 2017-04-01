using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Config
{
    public static int ipNum = 9;
}

public class OSCMainSample : MonoBehaviour 
{
    public Text ipTxt;
    public Slider ipSlider;

	void Start () 
    {
        var e = new Slider.SliderEvent ();
        e.AddListener (x => OnChangeIpChangeValue ((int)x));
        ipSlider.onValueChanged = e;
	}

    void OnChangeIpChangeValue(int value)
    {
        ipTxt.text = "<b>ReceiverIP</b>\n192.168.3." + value;
        Config.ipNum = value;
    }

    public void OpenReceiver ()
    {
        SceneManager.LoadScene ("SampleReceiverScene", LoadSceneMode.Single);
    }

    public void OpenSender ()
    {
        SceneManager.LoadScene ("SampleSenderScene", LoadSceneMode.Single);
    }
}