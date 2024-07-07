using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevelButton : MonoBehaviour
{
    [SerializeField] private int m_LevelSceneIndex;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(m_LevelSceneIndex);
    }

    private void OnMouseEnter()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
