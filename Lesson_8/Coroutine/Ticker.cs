using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker
{
    private float _currentTime = 0.0f;
    private float _deltaTime = 1.0f;
    private string _tick = "Ы";

    public Ticker() { }

    public Ticker(float deltaTime, string tick = "Ы")
    {
        _deltaTime = deltaTime;
        _tick = tick;
    }

    public void Tick(float time, string myMessage = "")
    {
        if ((time - _currentTime) >= _deltaTime)
        {
            Debug.Log($"ы {myMessage}");
            _currentTime = time;
        }
    }

}
