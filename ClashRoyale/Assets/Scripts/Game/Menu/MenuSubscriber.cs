using UnityEngine;

public class MenuSubscriber : MonoBehaviour
{
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private SelectedDeckUI selectedDeck;
    [SerializeField] private SelectedDeckUI selectedDeck2;
    [SerializeField] private AvailableDeckUI availableDeck;

    private void Start()
    {
        deckManager.UpdateSelected += selectedDeck.UpdateCardList;
        deckManager.UpdateSelected += selectedDeck2.UpdateCardList;
        deckManager.UpdateAvailable += availableDeck.UpdateCardList;
    }

    private void OnDestroy()
    {
        deckManager.UpdateSelected -= selectedDeck.UpdateCardList;
        deckManager.UpdateSelected -= selectedDeck2.UpdateCardList;
        deckManager.UpdateAvailable -= availableDeck.UpdateCardList;
    }
}
