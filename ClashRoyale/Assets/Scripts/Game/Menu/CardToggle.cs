using UnityEngine;

public class CardToggle : MonoBehaviour
{
    [SerializeField] private CardSelecter selecter;
    [SerializeField] private int index;

    public void Click(bool value)
    {
        
        {
            selecter.SetSelectToggleIndex(index);
        }
    }
}
