using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject Thing;
    public GameObject ThingText0;
    public GameObject ThingText1;
    public GameObject ThingText2;
    public GameObject ThingText3;
    public GameObject ThingText4;

    public void Close()
    {
        Thing.SetActive(false);
        ThingText0.SetActive(false);
        ThingText1.SetActive(false);
        ThingText2.SetActive(false);
        ThingText3.SetActive(false);
        ThingText4.SetActive(false);
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
