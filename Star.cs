using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    public GameObject Stars;
    public GameObject Thing;
    public GameObject ThingText;
    public heart Heart;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Heart.Sta += 1;
        Stars.SetActive(false);
        Thing.SetActive(true);
        ThingText.SetActive(true);
    }
}
