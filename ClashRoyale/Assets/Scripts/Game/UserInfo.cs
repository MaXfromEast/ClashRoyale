using UnityEngine;

public class UserInfo : MonoBehaviour
{
    #region Singleton
    private static UserInfo instance;
    public static UserInfo Instance
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
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private int id;
    public int ID
    {
        set
        {
            id = value;
        }
        get
        {
            return id;
        }
    }
}
