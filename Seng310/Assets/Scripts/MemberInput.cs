using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MemberInput : MonoBehaviour
{
    //variable to keep track of how many members are in the group
    public static int members = 1; //base of 1 people, max of 6

    private int clickedName = -1;
    private string[] saveName = new string[7];   //1 for group name, 6 for group members

    public Text[] inputText;
    public Text[] loadedName;
    public Text Header;
    public GameObject[] MemberObject;

    void Start()
    {
        loadedName[0].text = PlayerPrefs.GetString("name0", "Your Group");
        for(int i = 1 ; i < 5 ; i++){
            loadedName[i].text = PlayerPrefs.GetString("name"+i, "Person" + i);
        }
    }

    public void Clicked(int i){clickedName = i;}

    public void SetName()
    {
        if(clickedName != -1){
            saveName[clickedName] = inputText[clickedName].text;
            PlayerPrefs.SetString("name"+clickedName, saveName[clickedName]);
            loadedName[clickedName].text = saveName[clickedName];
        }
        clickedName = -1;
    }


    //UI has to be added/removed in order for desired effect
    //To change need to set up a group. (didn't to save time)
    public void Added()
    {
        members++;
    }

    public void Remove()
    {
        members--;
    }

    public void SaveClicked()
    {
        for(int i = 0 ; i < 6 ; i++){
            if(i < members){
                MemberObject[i].SetActive(true);
            }else{
                MemberObject[i].SetActive(false);
            }
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        print("Deleted all playerPrefs");
    }

    public void ChangeHeader(bool edit)
    {
        if(edit){
            Header.text = "Edit Group";
        }else{
            Header.text = "Create Group";
        }
    }
}
