using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    [SerializeField] private DeckManager manager;
    private List<int> available—ards = new List<int>();
    private int[] selectedCards;

    private void Start()
    {
        StartLoad();
    }
    private void StartLoad()
    {
        Loginer.Instance.Post(URLLibrary.MAIN + URLLibrary.GETDECKINFO, new Dictionary<string, string> { { "userID", /*UserInfo.Instance.ID.ToString()*/"1" } }, SuccessLoad, ErrorLoad);
    }

    private void ErrorLoad(string error)
    {
       Debug.LogError(error);
       StartLoad();
    }

    private void SuccessLoad(string data)
    {
        DeckData deckData = JsonUtility.FromJson<DeckData>(data);
        selectedCards = new int[deckData.selectedIDs.Length];
        for(int i = 0; i < deckData.selectedIDs.Length; i++)
        {
            int.TryParse(deckData.selectedIDs[i], out selectedCards[i]);
        }

        for (int i = 0; i < deckData.availableCards.Length; i++)
        {
            int.TryParse(deckData.availableCards[i].id, out int id);
            available—ards.Add(id);
        }

        manager.Init(available—ards, selectedCards);
    }
}

[System.Serializable]
public class DeckData
{
    public Availablecards[] availableCards;
    public string[] selectedIDs;
}

[System.Serializable]
public class Availablecards
{
    
    public string name;
    public string id;
}