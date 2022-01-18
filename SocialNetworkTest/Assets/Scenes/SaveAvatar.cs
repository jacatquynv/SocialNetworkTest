using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class SaveAvatar : MonoBehaviour
{
    [SerializeField] Button like;
    [SerializeField] Text likeNumber;
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("https://socialnetworktest-c60c5-default-rtdb.asia-southeast1.firebasedatabase.app/");
    }


    void OnClickSave()
    {
        
    }

    void OnClickUpload()
    {

    }
}

public class UserData
{
    public string name;
    public string urlImage;
    public int numberLike;
}
