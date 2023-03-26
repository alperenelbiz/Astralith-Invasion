using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    BannerView bannerView;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }

    void RequestBanner()
    {
        string adID = "ca-app-pub-7493031358701454/9286748293";

        this.bannerView = new BannerView(adID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }
}
