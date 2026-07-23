using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Gold { get; private set; }

    public int ClearStage { get; private set; }
    public int NowStage { get; private set; }   //MainUI에서 스테이지 선택

    public UnitsData unitsData;
    public UnitData[] printData;
    public GameObject[] unitsPrefab;
    public GameObject[] unitsImg;   //덱 저장

    public int[] deckUnitNumber;  //덱에 있는 유닛의 이름들 저장, 덱이 다 정해졌는지 체크

    public float bgmSoundVolume;
    public float sfxSoundVolume;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        printData = new UnitData[unitsData.list.Count]; //struct라 원본값 안바뀜
        for (int i = 0; i < unitsData.list.Count; i++)
        {
            printData[i] = unitsData.list[i];
        }

        deckUnitNumber = new int[5];
        NowStage = 0;

        Gold = PlayerPrefs.GetInt("Gold", 10000);
        printData[0].level = PlayerPrefs.GetInt("MeleeUnitLv", 1);
        printData[1].level = PlayerPrefs.GetInt("RangedUnitLv", 1);
        printData[2].level = PlayerPrefs.GetInt("ShieldUnitLv", 1);
        printData[3].level = PlayerPrefs.GetInt("KingUnitLv", 0);
        printData[4].level = PlayerPrefs.GetInt("HealUnitLv", 0);
        printData[5].level = PlayerPrefs.GetInt("MagicUnitLv", 0);
        printData[6].level = PlayerPrefs.GetInt("GrabUnitLv", 0);
        printData[7].level = PlayerPrefs.GetInt("FlyUnitLv", 0);
        printData[8].level = PlayerPrefs.GetInt("SkillUnitLv", 0);
        for (int i = 0; i < printData.Length; i++)  //레벨에 맞춰서 스펙 세팅
        {
            for (int j = 1; j < printData[i].level; j++)    //레벨 1부터 본인보다 작으면 스펙업
            {
                printData[i].maxHP += (printData[i].maxHP / 10f);
                printData[i].damage += (printData[i].damage / 10f);
            }
        }
        ClearStage = PlayerPrefs.GetInt("ClearStage", 0);

        for (int i = 0; i < deckUnitNumber.Length; i++)
            deckUnitNumber[i] = int.Parse(PlayerPrefs.GetString("DeckData", "-1 -1 -1 -1 -1").Split()[i]);

        bgmSoundVolume = PlayerPrefs.GetFloat("bgmSoundVolume", 1);
        sfxSoundVolume = PlayerPrefs.GetFloat("sfxSoundVolume", 1);
    }
        
    public void UseGold(int useGold)
    {
        if (Gold >= useGold)
            Gold -= useGold;
    }

    public void RewardGold(int gold)
    {
        Gold += gold;
    }

    public void SetStage(int stage) //게임시작 버튼을 누를때 현재 선택한 스테이지 저장
    {
        NowStage = stage;
    }
    public void SetClearStage() //게임씬의 BattleManager에서 게임 클리어하면 현재 클리어 스테이지 갱신
    {
        if (ClearStage < NowStage)
            ClearStage = NowStage;
    }

    public void ClearData()
    {
        for (int i = 0; i < unitsData.list.Count; i++)
        {
            printData[i] = unitsData.list[i];
        }
        Gold = 10000;
        ClearStage = 0;
    }
    public void SaveDeckData()  //DeckSettinh에서 실행 -> 클리어했을때 덱이 이전덱으로 롤백됨
    {
        PlayerPrefs.SetString("DeckData", deckUnitNumber[0] + " " + deckUnitNumber[1] + " " + deckUnitNumber[2] + " " + deckUnitNumber[3] + " " + deckUnitNumber[4]);
    }
    public void LoadDeckData()  //DeckSetting에서 실행 -> 씬이동하고 클리어했을때 덱 사라지는 현상 보완
    {
        for (int i = 0; i < deckUnitNumber.Length; i++)
            deckUnitNumber[i] = int.Parse(PlayerPrefs.GetString("DeckData", "-1 -1 -1 -1 -1").Split()[i]);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("MeleeUnitLv", printData[0].level);
        PlayerPrefs.SetInt("RangedUnitLv", printData[1].level);
        PlayerPrefs.SetInt("ShieldUnitLv", printData[2].level);
        PlayerPrefs.SetInt("KingUnitLv", printData[3].level);
        PlayerPrefs.SetInt("HealUnitLv", printData[4].level);
        PlayerPrefs.SetInt("MagicUnitLv", printData[5].level);
        PlayerPrefs.SetInt("GrabUnitLv", printData[6].level);
        PlayerPrefs.SetInt("FlyUnitLv", printData[7].level);
        PlayerPrefs.SetInt("SkillUnitLv", printData[8].level);
        PlayerPrefs.SetInt("ClearStage", ClearStage);
        PlayerPrefs.SetString("DeckData", deckUnitNumber[0] + " " + deckUnitNumber[1] + " " + deckUnitNumber[2] + " " + deckUnitNumber[3] + " " + deckUnitNumber[4]);
        PlayerPrefs.SetFloat("bgmSoundVolume", bgmSoundVolume);
        PlayerPrefs.SetFloat("sfxSoundVolume", sfxSoundVolume);
    }
}
