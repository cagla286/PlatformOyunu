using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public Text timeValue;
    public float time;

    void Start()
    {
        timeValue.text = time.ToString();
    }

    
    void Update()
    {
        time -= Time.deltaTime;
        timeValue.text = ((int)time).ToString();


    }
}
