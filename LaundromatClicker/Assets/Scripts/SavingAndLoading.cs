using UnityEngine;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingAndLoading : MonoBehaviour
{
    public static SavingAndLoading instance;

    private GameState GS;
    private UIManager UIM;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GS = GameState.instance;
        UIM = UIManager.instance;
    }

    public void Save()
    {
        //Button press SFX
        SFXManager.instance.PlayEffect(SoundEffectNames.WHOOSH);

        SavePurchases();

        SaveGameState();
    }

    private void SaveGameState()
    {
        //Save data path
#if UNITY_EDITOR
        string gameStatePath = Application.dataPath + "/SaveFiles/gamestate.dat";
#else
        string gameStatePath = Application.persistentDataPath + "/gamestate.dat";
#endif
        FileStream file;

        //If the file exists then rewrite it, otherwise make a new file
        if (File.Exists(gameStatePath))
        {
            file = File.OpenWrite(gameStatePath);
        }
        else
        {
            file = File.Create(gameStatePath);
        }

        //Save the game state data into the file
        GameStateData gsData = new GameStateData();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, gsData);
        file.Close();
    }

    //Automators are saved first
    //Same process as SaveGameState()
    private void SavePurchases()
    {
        //Save data path
#if UNITY_EDITOR
        string purchasesPath = Application.dataPath + "/SaveFiles/purchases.dat";
#else
        string purchasesPath = Application.persistentDataPath + "/purchases.dat";
#endif

        FileStream file;

        if (File.Exists(purchasesPath))
        {
            file = File.OpenWrite(purchasesPath);
        }
        else
        {
            file = File.Create(purchasesPath);
        }

        PurchasesData pData = new PurchasesData();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, pData);
        file.Close();
    }

    public void LoadGame()
    {
       //Load data path
#if UNITY_EDITOR
       string purchasesPath = Application.dataPath + "/SaveFiles/purchases.dat";
       string gameStatePath = Application.dataPath + "/SaveFiles/gamestate.dat";
#else
       string purchasesPath = Application.persistentDataPath + "/purchases.dat";
       string gameStatePath = Application.persistentDataPath + "/gamestate.dat";
#endif

        FileStream file;

        //If the file exists then open it, otherwise there is no file to be read
        if (File.Exists(gameStatePath)) file = File.OpenRead(gameStatePath);
        else
        {
            Debug.Log("File not found");
            return;
        }

        //Deserialize the file
        BinaryFormatter bf = new BinaryFormatter();
        GameStateData gsData = (GameStateData)bf.Deserialize(file);
        file.Close();

        //Put file data into GameState variables
        GS.prestigeScore = gsData.prestigeScore;
        GS.lastDatePlayed = gsData.lastDatePlayed;
        GS.collectedDailyReward = gsData.collectedDailyReward;
        GS.SetMoney(gsData.money);
        GS.SetLooseChange(gsData.looseChange);
        GS.moneyPerClick = gsData.moneyPerClick;
        GS.numAutoClicks = gsData.numAutoClicks;
        GS.currentDailyReward = gsData.currentDailyReward;

        //The above for the UIM purchasables
        if (File.Exists(purchasesPath)) file = File.OpenRead(purchasesPath);
        else
        {
            Debug.Log("File not found");
            return;
        }

        PurchasesData pData = (PurchasesData)bf.Deserialize(file);
        file.Close();

        UIM.automators = pData.automators;
        UIM.accelerators = pData.accelerators;
    }
}
