using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
    private static AudioControllerScript s_Instance = null;
    public static bool s_AudioOn;

    static AudioControllerScript()
    {
        s_AudioOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if( s_Instance==null)
        {
            DontDestroyOnLoad(gameObject);
            s_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
