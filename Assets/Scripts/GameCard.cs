using MemoryGame;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCard : MonoBehaviour
{
    private GameObject m_CardsImageArray;
    private Slot m_Slot;
    private GameObject m_CardImage;
    private GameObject m_BackSideObject;

    public int m_Row;
    public int m_Colomn;

    public Slot Slot
    {
        get { return m_Slot; }
        set { m_Slot = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        setMembers();
    }

    private void setMembers()
    {
        CardsImageArrayScript cardsImageArray;

        m_CardsImageArray = GameObject.FindGameObjectWithTag("CardImageArray");
        m_Slot = getSlotFromManager();

        cardsImageArray = m_CardsImageArray.GetComponent<CardsImageArrayScript>();

        m_CardImage = cardsImageArray.CardsImages[m_Slot.LogicValue];
        m_BackSideObject = cardsImageArray.CardsImages[cardsImageArray.CardsImages.Length - 1];

        m_CardImage = GameObject.Instantiate(m_CardImage);
        m_CardImage.transform.SetParent(transform, false);
        m_CardImage.SetActive(false);

        m_BackSideObject = GameObject.Instantiate(m_BackSideObject);
        m_BackSideObject.transform.SetParent(transform, false);
        m_BackSideObject.SetActive(true);

    }

    private Slot getSlotFromManager()
    {
        Slot slot;

        if (GameSceneManager.s_SceneGameManager!=null)
        {
            slot = GameSceneManager.s_SceneGameManager.GetSlotAt(m_Row, m_Colomn);
        }
        else
        {
            SampleGameManager sampleGameManager = GameObject.Find("SampleManager").GetComponent<SampleGameManager>();
            slot = sampleGameManager.getSlotInPlace(m_Row, m_Colomn);
        }

        return slot;
    }

    public void ResetCard()
    {
        Destroy(m_CardImage);
        Destroy(m_BackSideObject);
        setMembers();
    }

    private void OnMouseDown()
    {
        OnClick();
    }

    protected virtual void OnClick()
    {
        Debug.Log(this.gameObject.name + " has pressed and it " + m_Slot.State.ToString());

        if (m_Slot.State == eSlotState.Hidden && GameSceneManager.s_SceneGameManager.CanPress)
        {
            m_Slot.State = eSlotState.Bare;
            ChangeCardToHisState();

            GameSceneManager.s_SceneGameManager.AddCardToChoice(m_Slot);
        }
    }

    public void ChangeCardToHisState()
    {
        if(m_Slot.State == eSlotState.Hidden)
        {
            m_BackSideObject.SetActive(true);
            m_CardImage.SetActive(false);
        }
        else
        {
            m_BackSideObject.SetActive(false);
            m_CardImage.SetActive(true);
        }
    }

    private void OnMouseEnter()
    {
        changeFrameColor(Color.green, false);
    }

    private void OnMouseExit()
    {
        changeFrameColor(Color.white, true);
    }

    private void changeFrameColor(Color i_Color, bool i_Anyway)
    {
        if (GameSceneManager.s_SceneGameManager == null)
        {
            return;
        }

        if (GameSceneManager.s_SceneGameManager.CanPress || i_Anyway)
        {
            SpriteRenderer renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            renderer.color = i_Color;
        }
    }
}
