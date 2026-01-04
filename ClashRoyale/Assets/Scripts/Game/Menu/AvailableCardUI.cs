
using UnityEngine;
using UnityEngine.UI;

public class AvailableCardUI : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Color availableColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color lockedColor;
    private CardStateType currentState = CardStateType.None;
    [SerializeField] private CardSelecter selecter;
    [SerializeField] private int id;
    #region Editor
#if UNITY_EDITOR
    [SerializeField] private Image image;
    

    public void Create(CardSelecter selecter, Card card, int id)
    {
        this.selecter = selecter;
        this.id = id;
        image.sprite = card.Sprt;
        text.text = card.Name;
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    #endregion
    public void Click()
    {
        switch (currentState)
        {
            case CardStateType.None:
                break;
            case CardStateType.Available:
                selecter.SelectCard(id);
                break;
            case CardStateType.Selected:
                break;
            case CardStateType.Locked:
                break;
            default:
                break;
        }
    }

    public void SetState(CardStateType state)
    {
        currentState = state;
        switch (state)
        {
            case CardStateType.None:
                break;
            case CardStateType.Available:
                text.color = availableColor;
                break;
            case CardStateType.Selected:
                text.color = selectedColor;
                break;
            case CardStateType.Locked:
                text.color = lockedColor;
                break;
            default:
                break;
        }
    }
    public enum CardStateType
    {
        None = 0,
        Available = 1,
        Selected = 2,
        Locked = 3

    }
}
