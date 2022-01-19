using Firebase.Database;
using Firebase.Storage;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using LitJson;

public class UploadAvatar : MonoBehaviour
{
    [SerializeField] Button upload;
    [SerializeField] InputField m_name;
    [SerializeField] Image image;
    [SerializeField] RawImage testImage;

    StorageReference storage;
    DatabaseReference database;
    private void Start()
    {
        database = FirebaseDatabase.GetInstance("https://socialnetworktest-c60c5-default-rtdb.asia-southeast1.firebasedatabase.app/").GetReference("socialnetworktest-c60c5-default-rtdb");
        storage = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://socialnetworktest-c60c5.appspot.com");
        upload.onClick.AddListener(OnClickUpload);
    }

    public void OnClickUpload()
    {
        var customBytes = image.sprite.texture.EncodeToPNG();
        string url = $"ShareImage/{image.sprite.name}.png";
        StorageReference imageRef = storage.Child(url);
        MetadataChange metadata = new MetadataChange();
        metadata.ContentType = "image/png";

        imageRef.PutBytesAsync(customBytes, metadata).ContinueWith((Task<StorageMetadata> task) =>
         {
             if (task.IsFaulted || task.IsCanceled)
             {
                 Debug.Log(task.Exception.ToString());  
            }
             else
             {
                 StorageMetadata metadata = task.Result;
                 string md5Hash = metadata.Md5Hash;
                 Debug.Log("Finished uploading...");
                 Debug.Log("md5 hash = " + md5Hash);

                 UserData data = new UserData(m_name.text, url);
                 User user = new User(data.GetHashCode().ToString(), data);

                 string json = JsonMapper.ToJson(data);
                 database.Child("share_image").Child(user.id).SetRawJsonValueAsync(json);
             }
         });
    }


}

[Serializable]
public class UserData
{
    public string name;
    public string urlImage;
    public int numberLike;

    public UserData(string name, string urlImage)
    {
        this.name = name;
        this.urlImage = urlImage;
        this.numberLike = 0;
    }

    public UserData() { }
}

[Serializable]
public class User
{
    public string id;
    public UserData data;

    public User(string id, UserData data)
    {
        this.id = id;
        this.data = data;
    }

    public User(){}
}

