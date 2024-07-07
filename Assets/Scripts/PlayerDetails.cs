using MemoryGame;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    public static Player[] s_Players = null;

    // Start is called before the first frame update
    void Start()
    {
        if(s_Players == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            s_Players = new Player[2];
        }
    }
}
