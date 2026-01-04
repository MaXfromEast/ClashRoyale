using System.Collections.Generic;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private int selectToggleIndex;
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private AvailableDeckUI availableDeckUI;
    [SerializeField] private SelectedDeckUI selectedDeckUI;
    private List<Card> availableCards = new List<Card>();
    private List<Card> selectedCards = new List<Card>();
    public IReadOnlyList<Card> AvailableCards { get { return availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return selectedCards; } }

    private void OnEnable()
    {
        availableCards.Clear();
        for (int i = 0; i < deckManager.AvailableCards.Count; i++)
        {
            availableCards.Add(deckManager.AvailableCards[i]);
        }
        selectedCards.Clear();
        for(int i = 0; i < deckManager.SelectedCards.Count; i++)
        {
            selectedCards.Add(deckManager.SelectedCards[i]);
        }
    }
    public void SetSelectToggleIndex(int index)
    {
        selectToggleIndex = index;
    }

    public void SelectCard(int cardID)
    {
        selectedCards[selectToggleIndex] = availableCards.Find(p => p.ID == cardID);
        selectedDeckUI.UpdateCardList(SelectedCards);
        availableDeckUI.UpdateCardList(AvailableCards, SelectedCards);
    }
}
