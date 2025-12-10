using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    #region SingletonOneScene
    private static MapInfo instance;

    public static MapInfo Instance
    {
        private set
        {
            instance = value;
        }
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; 
        }

    }

    private void OnDestroy()
    {
        if (Instance = this) Instance = null;
    }
    #endregion
    [SerializeField] private List<Tower> playerTowers = new List<Tower>();
    [SerializeField] private List<Tower> enemyTowers = new List<Tower>();

    [SerializeField] private List<Unit> playerWalkingUnits = new List<Unit>();
    [SerializeField] private List<Unit> enemyWalkingUnits = new List<Unit>();

    [SerializeField] private List<Unit> playerFlyUnits = new List<Unit>();
    [SerializeField] private List<Unit> enemyFlyUnits = new List<Unit>();
   

    private void Start()
    {
        SubscribeDestroy(enemyTowers);
        SubscribeDestroy(playerTowers);
        SubscribeDestroy(enemyWalkingUnits);
        SubscribeDestroy(playerWalkingUnits);
        SubscribeDestroy(enemyFlyUnits);
        SubscribeDestroy(playerFlyUnits);
    }


    public void AddUnit(Unit unit)
    {
        List<Unit> list;
        if(unit.IsEnemy)
        {
            list = unit.UnitParameters.IsFly ? enemyFlyUnits : enemyWalkingUnits;
        }
        else
        {
            list = unit.UnitParameters.IsFly ? playerFlyUnits : playerWalkingUnits;
        }
        AddObjectToList(list, unit);
    }


    public Tower GetNearestTower(in Vector3 currentPosition, bool enemy, out float distance)
    {
        List<Tower> towers = enemy ? enemyTowers : playerTowers;
        Tower tower = GetNearest(currentPosition, towers, out distance);
        return tower;
    }

    public Unit TryGetNearestWalkingUnit(in Vector3 currentPosition, bool enemy, out float distance)
    {
        List<Unit> units = enemy ? enemyWalkingUnits : playerWalkingUnits;
        Unit unit = GetNearest(currentPosition, units, out distance);
        return unit;
    }

    public Unit TryGetNearestFlyUnit(in Vector3 currentPosition, bool enemy, out float distance)
    {
        List<Unit> units = enemy ? enemyFlyUnits : playerFlyUnits;
        Unit unit = GetNearest(currentPosition, units, out distance);

        return unit;
    }

    public Unit TryGetNearestAnyUnit(in Vector3 currentPosition, bool enemy, out float distance)
    {
        Unit walking = TryGetNearestWalkingUnit(currentPosition, enemy, out float walkingDistance);
        Unit fly = TryGetNearestFlyUnit(currentPosition, enemy,  out float flyDistance);
        Unit unit;
        if (flyDistance < walkingDistance)
        {
            unit = fly;
            distance = flyDistance;
        }
        else
        {
            unit = walking;
            distance = walkingDistance;
        }
        return unit;
    }

    //public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance)
    //{
    //    List<Unit> units = enemy ? enemyWalkingUnits : playerWalkingUnits;
    //    unit = GetNearest(currentPosition, units, out distance);

    //    return unit;
    //}




    private T GetNearest<T> (in Vector3 currentPosition, List<T> objects, out float distance) where T : MonoBehaviour
    {
        distance = float.MaxValue;
        if (objects.Count <= 0)
        {
            return null;
        }

        distance = Vector3.Distance(currentPosition, objects[0].transform.position);
        T nearest = objects[0];

        for(int i = 1; i < objects.Count; i++)
        {
            float tmpDistance = Vector3.Distance(currentPosition, objects[i].transform.position);
            if (tmpDistance < distance)
            {
                distance = tmpDistance;
                nearest = objects[i];
            }
        }
        return nearest;
    }

    private void SubscribeDestroy<T>(List<T> objects) where T : IDestroyed
    {
        for (int i = 0; i < objects.Count; i++)
        {
            T obj = objects[i];
            objects[i].destroyed += RemoveAndUnsubscribe;

            void RemoveAndUnsubscribe()
            {
                RemoveObjectFromList(objects, obj);
                obj.destroyed -= RemoveAndUnsubscribe;
            }
        }
    }



    private void AddObjectToList<T>(List<T> list, T obj) where T : IDestroyed
    {
        list.Add(obj);
        obj.destroyed += RemoveAndUnsubscribe;
        void RemoveAndUnsubscribe()
        {
            RemoveObjectFromList(list, obj);
            obj.destroyed -= RemoveAndUnsubscribe;
        }
       
    }

    private void RemoveObjectFromList<T>(List<T> list, T obj) where T : IDestroyed
    {
        if(list.Contains(obj))
        {
            list.Remove(obj);
        }
    }
}
