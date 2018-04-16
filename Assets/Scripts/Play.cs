 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour {

    public void OnPlay()
    {
        Application.LoadLevel("Main");
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
