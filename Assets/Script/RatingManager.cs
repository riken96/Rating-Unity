using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RatingManager : MonoBehaviour
{
    private const string PlayStoreUrl = "https://play.google.com/store/apps/details?id={0}";
    public string androidBundleID;
    public string iosAppId;
    public Sprite BlankStarSprite;
    public Sprite FilledStarSprite;
    public Image[] StarImages;
    public int startReview = -1;

    public int lastSelectedStar = 0;

    public GameObject TapToRate;
    public GameObject Thanks;
    public GameObject rattingPopUp;

    private void OnEnable()
    {
        lastSelectedStar = startReview;
        for (int i = 0; i < StarImages.Length; i++)
        {
            StarImages[i].sprite = BlankStarSprite;
        }
    }

    public void SelectStar(int index)
    {
        Debug.Log("Win");

        lastSelectedStar = index;
        for (int i = 0; i < StarImages.Length; i++)
        {
            StarImages[i].sprite = BlankStarSprite;
        }

        for (int i = 0; i < index + 1; i++)
        {
            StarImages[i].sprite = FilledStarSprite;
        }
        if (lastSelectedStar >= 3)
        {
            //rattingPopUp.SetActive(false);
            Thanks.SetActive(true);
            Debug.Log("Ratting Star is > 3");
        }
        else
        {
            rattingPopUp.SetActive(true);
            Thanks.SetActive(true);
            Debug.Log("Selected Start is < 3");
        }
        StartCoroutine(OnRateButtonPressed());
        TapToRate.SetActive(false);
    }

    public IEnumerator OnRateButtonPressed()
    {
        if (lastSelectedStar >= 3)
        {
            OpenRateURLs();
            Debug.Log(lastSelectedStar);
            gameObject.SetActive(false);
        }
        else if (lastSelectedStar < 0)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("please give rate");
            gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("rate is less than 3");
            Debug.Log("open thanks here");
        }
    }
    public void OpenRateURLs()
    {
        Debug.Log("Link is Opened");
#if UNITY_ANDROID
        Application.OpenURL(String.Format(PlayStoreUrl, androidBundleID));
#elif UNITY_IOS
            Application.OpenURL(String.Format(AppStoreUrl, crossPromoAssetsRoot.appStoreID));
#endif
    }
}