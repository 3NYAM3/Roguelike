using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private TMP_Text coinText;
    private int curretnCoin;
    private int maxCoin=30;

    const string COIN_AMOUNT_TEXT = "coin amount";
    private void Awake() {
        curretnCoin = 0;
    }

    private void Start() {
        curretnCoin = 0;
    }

    public void UPdateCurrentCoin() {
        curretnCoin++;

        if(coinText == null) {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = curretnCoin.ToString()+"/"+maxCoin;
    }

    public int GetCoin() {
        return curretnCoin;
    }
}
