using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    private TMP_Text coinText;
    private int curretnCoin = 0;

    const string COIN_AMOUNT_TEXT = "coin amount";

    public void UPdateCurrentCoin() {
        curretnCoin++;

        if(coinText == null) {
            coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        coinText.text = curretnCoin.ToString()+"/20";
    }
}
