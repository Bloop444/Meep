using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System.Globalization;
using System;
using TMPro;
using Photon.VR;
using Photon.VR.Cosmetics;
using System.Linq.Expressions;
using System.IO;
using Photon.Pun;

public class Playfablogin : MonoBehaviourPun
{
    public static Playfablogin instance;
    [Header("COSMETICS")]
    public string MyPlayFabID;
    public string CatalogName;
    [Space]
    public bool shouldQuitIfBanned = true;
    [Space]
    public List<GameObject> specialitems;
    public List<GameObject> disableitems;
    [Header("CURRENCY")]
    public string CurrencyName;
    public string CurrenyCode = "HS";
    public float TimeBeforeUpdatingMone = 2f;
    public TextMeshPro currencyText;
    [HideInInspector] public int coins;
    [Header("BAN STUFF")]
    public GameObject[] StuffToDisable;
    public GameObject[] StuffToEnable;
    public MeshRenderer[] StuffToMaterialChange;
    public Material MaterialToChangeToo;
    public TextMeshPro[] BanTimes;
    public TextMeshPro[] BanReasons;
    [Header("TITLE DATA")]
    public TextMeshPro MOTDText;
    [Header("PLAYER DATA")]
    public TextMeshPro UserName;
    public string StartingUsername;
    public string Name;
    [SerializeField]
    public bool UpdateName;
    [Header("DON'T DESTROY ON LOAD")]
    public GameObject[] DDOLObjects;
    [Header("Anticheat Part")]
    public string appName = "com.COMPANYNAME.GAMENAME";
    public string FolderPath = "/storage/emulated/0/Android/data/com.YOURCOMPANY.YOURGAMENAME/files/Mods";
    private List<ItemInstance> currentInventory;
    private bool CosmeticTaken = false;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        login();
        InvokeRepeating("checkmone", TimeBeforeUpdatingMone, 1.0f);

        string CurrentPackageName = Application.identifier;

        if (CurrentPackageName != appName)
        {
            Debug.Log("Modded APK");
            Application.Quit();
        }

        if (Directory.Exists(FolderPath))
        {
            Debug.Log("Mods detected!");
            Application.Quit();
        }
    }

    void Checkmone()
    {
        Playfablogin.instance.GetVirtualCurrencies();
    }

    public void login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logging in");
        GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);
        StartCoroutine(DDOLStuff());
        GetVirtualCurrencies();
        GetMOTD();
        StartCoroutine(Inv());
        StartCoroutine(Banne());
    }

    public void AccountInfoSuccess(GetAccountInfoResult result)
    {
        MyPlayFabID = result.AccountInfo.PlayFabId;
        UpdateInventory();
    }

    void UpdateInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        (result) =>
        {
            HandleInventoryUpdate(result.Inventory);
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    IEnumerator Inv()
    {
        while (true)
        {
            CheckInventory();
            yield return new WaitForSeconds(10);
        }
    }

    void CheckInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventorySuccess, OnError);
    }

    void OnGetInventorySuccess(GetUserInventoryResult result)
    {
        if (currentInventory == null)
        {
            currentInventory = result.Inventory;
            HandleInventoryUpdate(result.Inventory);
        }
        else
        {
            if (IsInventoryChanged(result.Inventory))
            {
                currentInventory = result.Inventory;
                HandleInventoryUpdate(result.Inventory);
            }
        }
    }

    bool IsInventoryChanged(List<ItemInstance> newInventory)
    {
        if (newInventory.Count != currentInventory.Count)
            return true;

        foreach (var newItem in newInventory)
        {
            bool itemFound = false;
            foreach (var currentItem in currentInventory)
            {
                if (newItem.ItemInstanceId == currentItem.ItemInstanceId)
                {
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
                return true;
        }

        return false;
    }

    void HandleInventoryUpdate(List<ItemInstance> inventory)
    {

        HashSet<string> inventoryItemIds = new HashSet<string>();
        foreach (var item in inventory)
        {
            if (item.CatalogVersion == CatalogName)
            {
                inventoryItemIds.Add(item.ItemId);
            }
        }

        foreach (var specialItem in specialitems)
        {
            if (inventoryItemIds.Contains(specialItem.name))
            {
                specialItem.SetActive(true);
                Debug.Log("Good Boi");
            }
            else
            {
                specialItem.SetActive(false);
                if(!CosmeticTaken)
                {
                    CosmeticTaken = true;
                    PhotonVRManager.SetCosmetic(CosmeticType.Head, "");
                    PhotonVRManager.SetCosmetic(CosmeticType.Face, "");
                    PhotonVRManager.SetCosmetic(CosmeticType.Body, "");
                    PhotonVRManager.SetCosmetic(CosmeticType.RightHand, "");
                    PhotonVRManager.SetCosmetic(CosmeticType.LeftHand, "");
                    Debug.Log("Fatass Got Demoted");
                }
            }
        }

        foreach (var disableItem in disableitems)
        {
            if (inventoryItemIds.Contains(disableItem.name))
            {
                disableItem.SetActive(false);
            }
            else
            {
                disableItem.SetActive(true);
            }
        }
    }

    async void Update()
    {
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        if (result.VirtualCurrency.ContainsKey(CurrenyCode))
        {
            coins = result.VirtualCurrency[CurrenyCode];
            currencyText.text = "You Have : " + coins.ToString() + " " + CurrencyName;
        }
    }

    private void OnError(PlayFabError error)
    {
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            PhotonVRManager.Manager.Disconnect();
            foreach (GameObject obj in StuffToDisable)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in StuffToEnable)
            {
                obj.SetActive(true);
            }
            foreach (MeshRenderer rend in StuffToMaterialChange)
            {
                rend.material = MaterialToChangeToo;
            }
            foreach (var item in error.ErrorDetails)
            {
                foreach (TextMeshPro BanTime in BanTimes)
                {
                    if (item.Value[0] == "Indefinite")
                    {
                        BanTime.text = "Permanent Ban";
                    }
                    else
                    {
                        string playFabTime = item.Value[0];
                        DateTime unityTime;
                        try
                        {
                            unityTime = DateTime.ParseExact(playFabTime, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                            TimeSpan timeLeft = unityTime.Subtract(DateTime.UtcNow);
                            int hoursLeft = (int)timeLeft.TotalHours;
                            BanTime.text = string.Format("Hours Left: {0}", hoursLeft);
                        }
                        catch (FormatException ex)
                        {
                            Debug.LogErrorFormat("Failed to parse PlayFab time '{0}': {1}", playFabTime, ex.Message);
                        }
                    }
                }
                foreach (TextMeshPro BanReason in BanReasons)
                {
                    BanReason.text = string.Format("Reason: {0}", item.Key);
                }
            }
            if (shouldQuitIfBanned)
            {
                Application.Quit();
            }
        }
        else
        {
            login();
        }
    }

    public void GetMOTD()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), MOTDGot, OnError);
    }

    public void MOTDGot(GetTitleDataResult result)
    {
        if (result.Data == null || result.Data.ContainsKey("MOTD") == false)
        {
            Debug.Log("No MOTD");
            return;
        }
        MOTDText.text = result.Data["MOTD"];
    }

    IEnumerator DDOLStuff()
    {
        Scene scene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject Obj in DDOLObjects)
        {
            DontDestroyOnLoad(Obj);
        }
    }

    IEnumerator Banne()
    {
        while (true)
        {
            lolll();
            yield return new WaitForSeconds(15f);
        }
    }
    void lolll()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, AccountInfoSuccess, OnError);
    }
}