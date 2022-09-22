using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLate : MonoBehaviour
{
    private float Delay;
    void Start()
    {
        this.gameObject.SetActive (false);
        float Delay = 0.0f;
    }

    public void second()
    {
        Debug.Log(Delay);
        Delay += Time.deltaTime;
        if(Delay >= 2)
        {
            this.gameObject.SetActive(true);
        } 
    }

    void Update()
    {
        second();
    }
}
