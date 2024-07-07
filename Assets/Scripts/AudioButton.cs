using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private bool m_IsPlayAudio;

    //public GameObject m_Line;
    [SerializeField] private AudioSource m_BackgroundAudio;
    [SerializeField] private Sprite[] m_ButtonStatesSpriteArray;

    private uint m_ButtonStatesCount;
    private uint m_ButtonStateIndex;
    private Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
        m_ButtonStateIndex = 0;
        m_ButtonStatesCount = (uint)m_ButtonStatesSpriteArray.Length;
        m_IsPlayAudio = true;

        m_BackgroundAudio.Play();

        m_Button = transform.GetComponent<Button>();

        m_Button.onClick.AddListener(OnClicked);

        m_Button.image.sprite = m_ButtonStatesSpriteArray[m_ButtonStateIndex];
    }

    private void OnClicked()
    {
        m_IsPlayAudio = !m_IsPlayAudio;
        AudioControllerScript.s_AudioOn = m_IsPlayAudio;

        if (m_IsPlayAudio)
        {
            m_BackgroundAudio.UnPause();
        }
        else
        {
            m_BackgroundAudio.Pause();
        }

        m_ButtonStateIndex = (m_ButtonStateIndex + 1) % m_ButtonStatesCount;
        m_Button.image.sprite = m_ButtonStatesSpriteArray[m_ButtonStateIndex];
    }
}
