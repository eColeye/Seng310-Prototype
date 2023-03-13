using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;

public class MoneyScript : MonoBehaviour
{
    //Float variable to keep track of the amm of money owed per month for the group
    private static float monthlyTotal = 0.0f; //Make into array for multi months 

    //Float variable to keep track of all non shared costs
    private static float memberPlus = 0.0f; //Make into array for multi members 

    //Array of all total money that is desplayed
    public Text[] MoneyText;

    //When code is started up:
    //Resets all money text to $0
    //Need to change if a later version with saving is needed
    void Start()
    {
        PlayerPrefs.DeleteAll();
        for(int i = 0 ; i < 6 ; i++){
            MoneyText[i].text = "$0";
        }
    }


    //Method to be called by other files to update the money and any other calculations needed with it
    //Takes in addded money, and later on date 

    //BUGS GOING BETWEEN NOT SHARED TO SHARED THIS IS A QUICK FIX
    public static void UpdateMoneyShared(float money)
    {
        if(MoneyInput.wasShared2){
            monthlyTotal += money;
        }else{
            monthlyTotal += money;
            memberPlus -= money;
        }
    }

    //SAME QUICK FIX AS ABOVE
    public static void UpdateMoneyNotShared(float money)
    {
        if(MoneyInput.wasShared){
            memberPlus += money;
            monthlyTotal -= money;
        }else{
            memberPlus += money;
        }
    }

    public void UpdateText()
    {
        MoneyText[0].text = "$" + (monthlyTotal/MemberInput.members + memberPlus).ToString("F2"); 

        for(int i = 1 ; i < MemberInput.members ; i++){
            MoneyText[i].text = "$" +  (monthlyTotal/MemberInput.members).ToString("F2");
        }
    }
}
