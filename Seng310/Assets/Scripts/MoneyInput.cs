using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyInput : MonoBehaviour
{
    //Has the num ammount of bills 
    //public static int numBill = 0;                  //Array where each gives num of bills in each month 
    public static int[] numBill = new int[12];

    public int month = 1;      //Int to keep track of months from 1-12 where 1 is jan and 12 is dec

    //This is a variable that is -1 when a new bill is added and 0-7 when it is a edit
    private int isEdit = -1;
    private float editTemp = 0.0f;
    public static bool wasShared = false;
    public static bool wasShared2 = true; //This is a quick fix 

    //Tells code if current bill is sharing or not
    private string isShare = "notSharing";

    //All text input for the add screen
    public Text[] inputAdd;

    //term to control code workflow to save to the correct 
    private string type = "null";


    //Game objects for transfering screens between the add/stats/home screen
    public GameObject Add;
    public GameObject Stats;
    public GameObject Home;

    //GameObject for share icon in add screen.
    public GameObject AddShare;

    //Game object of bill to make them appear or not
    public GameObject[] BillUI;

    public Text[] TitleText; //0 is title, 1 is last month, 2 is next month

    //Name text for bills
    public Text[] BillText;

    //Date text for bills
    public Text[] DateText;

    //Object for Share
    public GameObject[] ShareUI;

    //money text for bills
    public Text[] MoneyText;



    /*Needs to be done:

        reset text after adding/editing (not working)
        Money math


        Profiles +/-
    */

    private void SetName(int clicked)
    {
        if(type == "name"){
            //Saving prefs for bills name
            PlayerPrefs.SetString("billN" + month + clicked, inputAdd[0].text);
        }else if(type == "date"){
            //Saving prefs for bills date
            PlayerPrefs.SetString("billD" + month + clicked, inputAdd[2].text);
        }else if(type == "cost"){
            //Saving prefs for bills cost
            editTemp = PlayerPrefs.GetFloat("billC"+isEdit, 0);
            PlayerPrefs.SetFloat("billC" + month + clicked, float.Parse(inputAdd[1].text));
        }else if(type == "share"){
            //Saving prefs if bill is shared or not
            PlayerPrefs.SetString("billS" + month + clicked, isShare);
        }else{
            print("ERRR, type = " + type);
        }
    }

    //Goes through here everytime someone clicks off the text field
    //This saves the current text in player prefs
    public void AddBill()
    {
        if(isEdit == -1){
            SetName(numBill[month]);
            print("BillName" + numBill[month] + "month" + month + " - " + PlayerPrefs.GetString("billN"+ month + numBill[month], "NO BILL"));
            print("BillCost" + numBill[month] + "month" + month + " - " + PlayerPrefs.GetFloat("billC"+ month + numBill[month], 0));
            print("BillDate" + numBill[month] + "month" + month + " - " + PlayerPrefs.GetString("billD"+ month + numBill[month], "NO Date"));
            print("BillSharing" + numBill[month] + "month" + month + " - " + PlayerPrefs.GetString("billS"+ month + numBill[month], "notSharing"));
        }else{
            SetName(isEdit);
            print("BillName" + isEdit  + "month" + month + " - " + PlayerPrefs.GetString("billN" + month + isEdit, "NO BILL"));
            print("BillCost" + isEdit + "month" + month + " - " + PlayerPrefs.GetFloat("billC"+ month + isEdit, 0));
            print("BillDate" + isEdit + "month" + month + " - " + PlayerPrefs.GetString("billD"+ month + isEdit, "NO Date"));
            print("BillSharing" + isEdit + "month" + month + " - " + PlayerPrefs.GetString("billS"+ month + isEdit, "notSharing"));
        }
    }

    //Goes here everytime a text feild is clicked.
    //Changes the "type" variable, to let the code which text doc it is saving
    public void Clicked(int i){
        if(i == 0){
            type = "name";
        }else if(i == 2){
            type = "date";
        }else if(i == 1){
            type = "cost";
        }else if(i == 3){
            type = "share";
            if(isShare == "notSharing"){
                isShare = "sharing";
                AddShare.SetActive(true);
            }else{
                isShare = "notSharing";
                AddShare.SetActive(false);
            }
            AddBill();
        }else{
            type = "null";
        }
    }

    //Goes here every time the save button is clicked
    //Changes screens depending on if its editing or adding
    public void Save(){
        if(!canAdd()){
            return;
        }
        if(PlayerPrefs.GetString("billS" + month + numBill, "notSharing") == "notSharing"){
            PlayerPrefs.SetString("billS" + month + numBill, "notSharing");
        }

        
        if(isEdit == -1){
            if(PlayerPrefs.GetString("billS"+ month + numBill[month], "notSharing") == "sharing"){
                MoneyScript.UpdateMoneyShared(PlayerPrefs.GetFloat("billC"+ month + numBill[month], 0));
            }else{
                MoneyScript.UpdateMoneyNotShared(PlayerPrefs.GetFloat("billC"+ month + numBill[month], 0));
            }

            numBill[month] =+ 1;
            Add.SetActive(false);
            Home.SetActive(true);
        }else{
            if(PlayerPrefs.GetString("billS" + month + isEdit, "notSharing") == "sharing"){
                MoneyScript.UpdateMoneyShared(PlayerPrefs.GetFloat("billC"+ month + isEdit, 0) - editTemp);
            }else{
                MoneyScript.UpdateMoneyNotShared(PlayerPrefs.GetFloat("billC"+ month + isEdit, 0) - editTemp);
            }

            isEdit = -1;
            Add.SetActive(false);
            Stats.SetActive(true);
        }
        ReloadText();
        isShare = "notSharing";
        wasShared = false;
        wasShared2 = true;
    }

    public void Back()
    {
        if(isEdit == -1){
            Add.SetActive(false);
            Home.SetActive(true);
        }else{
            Add.SetActive(false);
            Stats.SetActive(true);
            isEdit = -1;
        }
    }

    //Checks anything needed for adding a bill
    public bool canAdd(){
        if(numBill[month] < 8){
            return true;
        }
        return false;
    }

    //When edit is clicked, goes here to control the code to save for editing instead of adding
    public void EditBill(int billNum)
    {
        isEdit = billNum;

        Stats.SetActive(false);
        Add.SetActive(true);

        if(PlayerPrefs.GetString("billS"+ month + isEdit, "notSharing") == "sharing"){
            wasShared = true;
        }else{
            wasShared2 = false; 
        }

        isShare = PlayerPrefs.GetString("billS"+ month + billNum, "notSharing");
        SetText();
    }

    //After saving, reloads all text. And reloads all money scripts
    private void ReloadText()
    {
        for(int i = 0 ; i < 8 ; i++){
            if(i < numBill[month]){
                BillUI[i].SetActive(true);
                BillText[i].text = PlayerPrefs.GetString("billN"+ month + i, "No Name");
                DateText[i].text = PlayerPrefs.GetString("billD"+ month + i, "No Date");
                MoneyText[i].text = "$" + PlayerPrefs.GetFloat("billC"+ month + i, 0);

                if((PlayerPrefs.GetString("billS"+ month + i, "ERR") != "notSharing")){
                    print("true");
                    ShareUI[i].SetActive(true);
                }else{
                    print("false");
                    ShareUI[i].SetActive(false);
                }
            }else{
                BillUI[i].SetActive(false);
            }
        }
    }

    //This is to update the text in the add screen for when it is editing,
    //This doesn't currently work properly,
    public void SetText()
    {
        print("in settext");
        if(isEdit == -1){
            print("in edit");
            inputAdd[0].text = "Name";  //Name
            inputAdd[1].text = "Price";  //Price
            inputAdd[2].text = "MM/DD/YYYY";  //Date
            AddShare.SetActive(false);
        }else{
            print("else");
            inputAdd[0].text = PlayerPrefs.GetString("billN"+ month + isEdit, "Name");  //Name
            inputAdd[1].text = PlayerPrefs.GetString("billC"+ month + isEdit, "Price");  //Price
            inputAdd[2].text = PlayerPrefs.GetString("billD"+ month + isEdit, "MM/DD/YYYY");  //Date

            if(PlayerPrefs.GetString("billS"+ month + isEdit, "ERR") == "notSharing"){
                AddShare.SetActive(false);
            }else if(PlayerPrefs.GetString("billS"+ month + isEdit, "ERR") == "sharing"){
                AddShare.SetActive(true);
            }
        }
    }



    public void MonthClick(bool next)
    {
        if(next){
            if(month < 12){
                month ++;
            }
        }else{
            if(month > 1){
                month --;
            }
        }

        if(month == 1){
            TitleText[1].text = "";
            TitleText[2].text = ">";

        }else if(month == 12){
            TitleText[1].text = "<";
            TitleText[2].text = "";
        }else{
            TitleText[1].text = "<";
            TitleText[2].text = ">";
        }

        TitleText[0].text = month.ToString() + " Bills";
        ReloadText();
    }

}
