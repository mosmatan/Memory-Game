using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAppButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Exit Button Pressed");
        Application.Quit();
    }
}
