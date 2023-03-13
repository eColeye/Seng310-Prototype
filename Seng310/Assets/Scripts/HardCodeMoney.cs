using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardCodeMoney : MonoBehaviour
{
    public Text[] Money; 
    public Text[] Costco = new Text[2];

    private string first = "$108.33";  //325 / 3
    private string addm = "$204.00";   //612 / 3

    private string addp1 = "$86.00";  //612 / 4
    private string addp2 = "$153.00";  

    private string edit = "$304.25"; //1217 / 4


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3 ; i++){
            Money[i].text = first;
        }
    }

    public void AddMoney()
    {
        for(int i = 1; i < 3; i++){
            Money[i].text = addm;
        } 
    }


    public void AddPerson()
    {
        Money[0].text = addp1;
        for(int i = 1; i < 4; i++){
            Money[i].text = addp2;
        } 
    }

    public void Edit()
    {
        for(int i = 1; i < 4; i++){
            Money[i].text = edit;
        }  
        Costco[0].text = "$872.00";   
        Costco[1].text = "02/25/2023";    
    }
}
