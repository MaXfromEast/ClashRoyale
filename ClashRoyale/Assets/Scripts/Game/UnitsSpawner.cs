using UnityEngine;

public class UnitsSpawner : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;
    [SerializeField] private GameObject footman;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject giant;
    [SerializeField] private Transform startPoint;

    public void MakeFootman()
    {
        Unit unit = Instantiate(footman, startPoint.position, startPoint.rotation).GetComponent<Unit>();
        mapInfo.AddUnit(unit);
    }
    
    public void MakeArcher()
    {
        Unit unit = Instantiate(archer, startPoint.position, startPoint.rotation).GetComponent<Unit>();
        mapInfo.AddUnit(unit);
    }

    public void MakeGiant()
    {
        Unit unit = Instantiate(giant, startPoint.position, startPoint.rotation).GetComponent<Unit>();
        mapInfo.AddUnit(unit);
    }
}
