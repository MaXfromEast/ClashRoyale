using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDeckUI : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private DeckManager deckManager;

    private void Start()
    {
        //deckManager.UpdateSelected += UpdateCardList;
    }

    public void UpdateCardList(IReadOnlyList<Card> cards)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < cards.Count)
            {
                images[i].sprite = cards[i].Sprt;
                images[i].enabled = true;
            }
            else
            {
                images[i].sprite = null;
                images[i].enabled = false;
            }
        }
    }
}
