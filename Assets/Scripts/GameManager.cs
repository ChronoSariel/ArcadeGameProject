using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
public TextMeshProUGUI P1CoinsText;
public TextMeshProUGUI P2CoinsText;
public int P1Coins;
public int P2Coins;
public GameObject Camera;
public GameObject Results;
public TextMeshProUGUI WinText;
public bool displayResults;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        P1CoinsText.text = P1Coins.ToString();
        P2CoinsText.text = P2Coins.ToString();
        displayResults = Camera.GetComponent<CameraMovement>().actCleared;
        if (displayResults)
        {
            Results.gameObject.SetActive(true);
            if (P1Coins > P2Coins)
            {
                WinText.text = "Player 1 Won!";
            }
            else if (P1Coins < P2Coins)
            {
                WinText.text = "Player 2 Won!";
            }
            else
            {
                WinText.text = "It's a Draw!";
            }
        }
    }
}
