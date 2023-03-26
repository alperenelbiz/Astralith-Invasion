using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManagerIn : MonoBehaviour
{
    InterstitialAd interstitial;
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestInterstitial();

        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

    }

    void RequestInterstitial()
    {
#if UNITY_ANDROID
        string _adUnitId = "ca-app-pub-7493031358701454/7858116559";
#elif UNITY_IPHONE
        string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string _adUnitId = "unused";
#endif

        this.interstitial = new InterstitialAd(_adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }
}
