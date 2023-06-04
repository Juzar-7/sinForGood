using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ReneManager : MonoBehaviour
{
    public static bool LoginStatus = false;
    public static Dictionary<string, bool> SkinStats = new Dictionary<string, bool>();
    public static string EmailHandler;

    public GameObject Email;
    public TextMeshProUGUI Timer;

    public GameObject SignInPanel;
    public GameObject CountdownPanel;

    ReneAPICreds _reneAPICreds;
    API ReneAPI;
    Prompts prompts;
    // Start is called before the first frame update
    void Start()
    {
        prompts = Prompts.prompts;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            prompts.Notify("WebGL version lacks login to reneverse, Kindly continue with Guest Mode");
        }

        _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        SkinStats["Cube"] = true;
        if (LoginStatus)
            SignInPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    public void SignUp()
    {
        Application.OpenURL("https://app.reneverse.io/register");
    }

 

    public void GuestMode()
    {
        SignInPanel.SetActive(false);
    }

    async Task ConnectUser()
    {
        _reneAPICreds.APIKey = "3eb2b58d-e575-485e-891d-b82ff544df6f";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCrNM5G3OYLDi1PUjIzxeXsfOnqloMEb0XLbIIzXPdxZLMwvCro6uZcAsNnZ1ZmTkxfdH4lA7ct6WBKaEpMvEewbHMCzvZhBT/qTFc0vxWEL1HbrDznBkxkWPlyf8IJAlbps98wkBV9/UuTFoH9/dugYmdmj2BOhHiA4j/rRGgkyLnc5afbfdjFbiisuhuijJCVmFKYLXqxd+ZEzVVnasBWuVqDpLO5kG3zmGtzD/2RVFB/QGiIyTz3zByT4FX78urJ2KPaBPkVQRF/VTgfX7FDDb7XvV+XTpHNUFiG5PEatYH1z0UfP1/sekMJatwJ5l4RZ0DPuDwBqXNuZdbsyP21AgMBAAECggEACW6vWVgc6cmuKrXsmM2eYpkUih12D8Mm+pN6g49A/WOpkQB3nL1+/jK+G+76451iDSBDPZF8K3C1eA6m3p4WP7IPOWhi9UAaIAkfWi3s5uDJ3GsbhTQ49o5bBTk6B3pe2iFUJT4gRlYI2TcJkmTEujaxsQTVLtOS5Vw8Xp82Mvsvp+54VwL0W+WgzRNZBk2eyO9HgriGFTjQDbj8hyk/scdTnvEAQqDvB+OfOJq172LVxm/WzIqD3I7ObEG2K+7mIkvgKpy9LEV6AN1W1yJSb0YGo47qjvPpzAVazJutZxnQSNN4LbmabcWEfcVv2/YbgowGMV3mt051S66Uw+BzQQKBgQDGOPNdKxb6nAjS6fmqMjuEV9n7SsJ7lM0Mi1XrCiM3QGQZENjX1IiwAnyjAmHZrBMx33I82uKCg+c4sdDcaVJ9T1/UuS7WgmOSGOFy1Og0xe1uE5pMNQh/2jAhUTv9YPiUZhqgOkejH6wtZKPfs3QmoDQ0GrNya4LHSjN3227RCwKBgQDdG/MHXq3oc+/3ymYDeOu5V5geD8dk/b0BWAWgDLRU4OVSZf1zpiyukaEj1tRvkE6+rQUyXUO40RrxFv2HuxstHCxAHCPbmdystJWikCakdVd7kpXJCqdjQRUk63a0wextkDIdb55L+Et3WTOt9rtshjgaIH981U/AmSUa1BokPwKBgE8Hi7e8o0e9iCMXF+xMZMBKCmWPE+UTmYED2HiCLTItIPuBZQMAAJM0Gp8fJwYS/gAnVzN+DUr97eUDuAL4luPRDqMQReVOTQaFlvUif4Xy18vIUv9JMZ9PzVqrOaC21cTMxJbXRZJ64Tmj41YgBRIVU1rmvl7DEHwGUGjb+t2rAoGBAIlpakLlQ10YWET9oyJ7TrvTxTTBh6Cq1IB2TKCn5JZvYfUwbzAlUNV4qdVVGJbw7w8vBfDD52d/hKfaKtvkm0IQpSt+kYZTe8JVRD1QKsPALQseETptBZP4iYR1VUOG1UFOAOcsTAtYlsXoSObPVLFgRJNwKOrSwoqmYlnnAQwPAoGBAJslQgUoBDB2tKEKHjoTTzq4/ADXppB7M5TSUCkVddOaNH6o5oSWsT3JUjmAzdUr+78mY+WEZ3NK2sCl4YLa+9VrA2/FVHa5P6V+WNOqNoOo/vsQ/fvOXDVZ7LGb9s20XH1JtAYXptfGtAyrbS8SXYpf5StODeZOt1rxu9T0bl5o";
        _reneAPICreds.GameID = "eaeb5559-bc6a-4b75-aa70-6d7e0e89f034";
        ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    private IEnumerator ConnectReneService(API reneApi)
    {
        CountdownPanel.SetActive(true);
        var timer = 30;
        var userConnected = false;
        var secondsToDecrement = 1;
        while (timer >= 0 && !userConnected)
        {
            Timer.text = timer.ToString();
            if (reneApi.IsAuthorized())
            {

                CountdownPanel.SetActive(false);
                SignInPanel.SetActive(false);


                yield return GetUserAssetsAsync(ReneAPI);

                userConnected = true;
                LoginStatus = true;
                Debug.Log("Connected To Reneverse");
                prompts.Notify("Connected to Reneverse");
            }

            yield return new WaitForSeconds(secondsToDecrement);
            timer -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
    }

    private async Task GetUserAssetsAsync(API reneApi)
    {
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        userAssets?.Items.ForEach(asset =>
        {
            string assetName = asset.Metadata.Name;
            string assetImageUrl = asset.Metadata.Image;
            string assetGemstone = "";
            asset.Metadata?.Attributes?.ForEach(attribute =>
            {
                if (attribute.TraitType == "Color")
                {
                    assetGemstone = attribute.Value;
                }
            });

            Asset assetObj = new Asset(assetName, assetImageUrl, assetGemstone);
       
        });
    }


public class Asset
{
    public string AssetName { get; set; }
    public string AssetUrl { get; set; }

    public string AssetColor { get; set; }
    public Asset(string assetName, string assetUrl, string assetColor)
    {
        AssetName = assetName;
        AssetUrl = assetUrl;
        AssetColor = assetColor;
    }
}

   
}
