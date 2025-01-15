using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update

    //Clicker
    public Text scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    //Shop
    public int shop1Prize;
    public TextMeshProUGUI shop1text;

    public int shop2Prize;
    public TextMeshProUGUI shop2text;

    //Amount
    public TextMeshProUGUI amount1Text;
    public int amount1;
    public float amount1Profit;

    public TextMeshProUGUI amount2Text;
    public int amount2;
    public float amount2Profit;

    //Upgrade
    public TextMeshProUGUI upgradeText;
    public int upgradePrize;

    void Start()
    {
        //Clicker
        currentScore = 0;
        hitPower = 1;
        scoreIncreasedPerSecond = 1;
        x = 0f;

        //We must set all default variable before load
        shop1Prize = 25;
        shop2Prize = 125;
        amount1 = 0;
        amount1Profit = 1;
        amount2 = 0;
        amount2Profit = 5;

        //Reset line
        //PlayerPrefs.DeleteAll();

        //Load
        currentScore = PlayerPrefs.GetFloat("currentScore", 0f);
        hitPower = PlayerPrefs.GetFloat("hitPower", 1f);
        x = PlayerPrefs.GetFloat("x", 0f);

        shop1Prize = PlayerPrefs.GetInt("shop1Prize", 25);
        shop2Prize = PlayerPrefs.GetInt("shop2Prize", 125);

        amount1 = PlayerPrefs.GetInt("amount1", 0);
        amount1Profit = PlayerPrefs.GetFloat("amount1Profit", 1f);

        amount2 = PlayerPrefs.GetInt("amount2", 0);
        amount2Profit = PlayerPrefs.GetFloat("amount2Profit", 5f);

        upgradePrize = PlayerPrefs.GetInt("upgradePrize", 50);
        

    }

    // Update is called once per frame
    void Update()
    {
        //Clicker
        scoreText.text = ((int)currentScore) + " $";
        scoreIncreasedPerSecond = amount1Profit + amount2Profit;
        currentScore += scoreIncreasedPerSecond * Time.deltaTime;

        //Shop
        shop1text.text = "Tier 1: " + shop1Prize + " $";
        shop2text.text = "Tier 2: " + shop2Prize + " $";

        //Amount
        amount1Profit = amount1 * 1;
        amount2Profit = amount2 * 5;

        amount1Text.text = $"Tier 1: {amount1} arts, ${amount1Profit}/s";
        amount2Text.text = $"Tier 2: {amount2} arts, ${amount2Profit}/s";

        //Upgrade
        upgradeText.text = "Cost: " + upgradePrize + " $";

        SaveGame();

    }

    void SaveGame()
    {
        PlayerPrefs.SetFloat("currentScore", currentScore);
        PlayerPrefs.SetFloat("hitPower", hitPower);
        PlayerPrefs.SetFloat("x", x);

        PlayerPrefs.SetInt("shop1Prize", shop1Prize);
        PlayerPrefs.SetInt("shop2Prize", shop2Prize);

        PlayerPrefs.SetInt("amount1", amount1);
        PlayerPrefs.SetFloat("amount1Profit", amount1Profit);

        PlayerPrefs.SetInt("amount2", amount2);
        PlayerPrefs.SetFloat("amount2Profit", amount2Profit);

        PlayerPrefs.SetInt("upgradePrize", upgradePrize);

        PlayerPrefs.Save();
    }
    //Hit
    public void Hit()
    {
        currentScore += hitPower;
    }

    //Shop

    public void Shop1()
    {
        if (currentScore >= shop1Prize)
        {
            currentScore -= shop1Prize;
            amount1 += 1;
            amount1Profit += 1;
            x += 1;
            shop1Prize += 25;
        }
    }
    public void Shop2()
    {
        if (currentScore >= shop2Prize)
        {
            currentScore -= shop2Prize;
            amount2 += 1;
            amount2Profit += 5;
            x += 5;
            shop2Prize += 125;
        }
    }


    //Upgrade
    public void Upgrade()
    {
        if (currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize *= 3;
        }
    }
}
