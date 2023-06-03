using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSoundBehaviour : MonoBehaviour
{
    private int fireCount = 0;

    // Update is called once per frame
    void Update()
    {
        var fires = GameObject.FindGameObjectsWithTag("Fire");
        if (fires.Length > fireCount)
        {
            GetComponent<AudioSource>().Play();
        }

        fireCount = fires.Length;
    }
}
