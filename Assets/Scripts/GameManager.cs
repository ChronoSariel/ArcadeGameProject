using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
public TextMeshProUGUI P1CoinsText;
public TextMeshProUGUI P2CoinsText;
public int P1Coins;
public int P2Coins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1CoinsText.text = P1Coins.ToString();
        P2CoinsText.text = P2Coins.ToString();
    }
}
