using Firebase.Database;
using Firebase.Storage;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UploadAvatar : MonoBehaviour
{
    [SerializeField] Button upload;
    [SerializeField] InputField m_name;
    [SerializeField] Image image;

    StorageReference storage;
    private void Start()
    {
        DatabaseReference database = FirebaseDatabase.GetInstance("https://socialnetworktest-c60c5-default-rtdb.asia-southeast1.firebasedatabase.app/").GetReference("socialnetworktest-c60c5-default-rtdb");
        storage = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://socialnetworktest-c60c5.appspot.com");
        upload.onClick.AddListener(OnClickUpload);
    }

    public void OnClickUpload()
    {
        var customBytes = image.sprite.texture.GetRawTextureData();
        StorageReference imageRef = storage.Child($"ShareImage/{m_name.text}.jpg");

        // Upload the file to the path "images/rivers.jpg"
        imageRef.PutBytesAsync(customBytes).ContinueWith((Task<StorageMetadata> task) =>
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
             }
         });
    }


}

public class UserData
{
    public string name;
    public string urlImage;
    public int numberLike;
}
