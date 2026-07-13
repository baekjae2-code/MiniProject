using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Gold = 10000;
    }

    public int Gold { get; private set; }

    public UnitsData unitsData;
    public UnitData[] printData;
    public GameObject[] unitsPrefab;
    public GameObject[] unitsImg;

    public int[] deckUnitNumber;  //덱에 있는 유닛의 이름들 저장, 덱이 다 정해졌는지 체크

    private void Start()
    {
        printData = new UnitData[unitsData.list.Count]; //struct라 원본값 안바뀜
        for (int i = 0; i < unitsData.list.Count; i++)
        {
            printData[i] = unitsData.list[i];
        }

        deckUnitNumber = new int[5] { -1, -1, -1, -1, -1 };
    }
    public void UseLevelUp(int useGold)
    {
        if (Gold > useGold)
            Gold -= useGold;
    }

    public void GetGold(int gold)
    {
        Gold += gold;
    }
}
