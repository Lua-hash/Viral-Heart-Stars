using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heart : MonoBehaviour
{

    public int Sta;

    void Start()
    {
        Sta = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Sta == 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
