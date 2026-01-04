using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] cards;
    [SerializeField] private List<Card> availableCards = new List<Card>();
    [SerializeField] private List<Card> selectedCards = new List<Card>();
    public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;
    public IReadOnlyList<Card> AvailableCards { get { return availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return selectedCards; } }

    #region Editor
#if UNITY_EDITOR
    [SerializeField] private AvailableDeckUI availableDeckUI;
    private void OnValidate()
    {
        availableDeckUI.SetAllCardsCount(cards);
    }
#endif
#endregion
    public void Init(List<int> availableCardsIndexes, int[] selectedCardsIndexes)
    {
        for (int i = 0; i < availableCardsIndexes.Count; i++)
        {
            availableCards.Add(cards[availableCardsIndexes[i]]);
        }

        for (int i = 0; i < selectedCardsIndexes.Length; i++)
        {
            selectedCards.Add(cards[selectedCardsIndexes[i]]);
        }

        UpdateAvailable?.Invoke(availableCards, selectedCards);
        UpdateSelected?.Invoke(selectedCards);
    }





}
[System.Serializable]
public class Card
{
    [SerializeField] private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField] private Sprite sprite;
    public Sprite Sprt
    {
        get
        {
            return sprite;
        }
    }
    [SerializeField] private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

}
