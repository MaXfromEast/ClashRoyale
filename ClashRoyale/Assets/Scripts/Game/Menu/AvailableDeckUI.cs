using System.Collections.Generic;
using UnityEngine;

public class AvailableDeckUI : MonoBehaviour
{
    [SerializeField] private CardSelecter selecter;
    [SerializeField] private List<AvailableCardUI> availableCardsUI = new List<AvailableCardUI>();
#region Editor
 #if UNITY_EDITOR
    [SerializeField] private Transform content;
    [SerializeField] private AvailableCardUI availableCardUIPrefab;
    
    public void SetAllCardsCount(Card[] cards)
    {
        for (int i = 0; i < availableCardsUI.Count; i++)
        {
            GameObject go = availableCardsUI[i].gameObject;
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(go);
        }
        availableCardsUI.Clear();

        for(int i = 1; i < cards.Length; i++)
        {
            AvailableCardUI card = Instantiate(availableCardUIPrefab, content);
            card.Create(selecter, cards[i], i);
            availableCardsUI.Add(card);
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    #endregion

    public void UpdateCardList(IReadOnlyList<Card> available, IReadOnlyList<Card> selected)
    {
        for (int i = 0; i < availableCardsUI.Count; i++)
        {
            availableCardsUI[i].SetState(AvailableCardUI.CardStateType.Locked);
        }

        for (int i = 0; i < available.Count; i++)
        {
            availableCardsUI[available[i].ID - 1].SetState(AvailableCardUI.CardStateType.Available);
        }

        for (int i = 0; i < selected.Count; i++)
        {
            availableCardsUI[selected[i].ID - 1].SetState(AvailableCardUI.CardStateType.Selected);
        }
    }
}
