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
        currentScore = 0f;
        hitPower = 1f;
        scoreIncreasedPerSecond = 1f;
        x = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Clicker
        scoreText.text = (int)currentScore + " $";
        scoreIncreasedPerSecond = x * Time.deltaTime;
        currentScore = currentScore + scoreIncreasedPerSecond;

        //Shop
        shop1text.text = "Tier 1: " + shop1Prize + " $";
        shop2text.text = "Tier 2: " + shop2Prize + " $";

        //Amount
        amount1Text.text = "Tier 1: " + amount1 + " arts $: " + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + " arts $: " + amount2Profit + "/s";

        //Upgrade
        upgradeText.text = "Cost: " + upgradePrize + " $";
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
