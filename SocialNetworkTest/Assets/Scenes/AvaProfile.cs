using Firebase.Database;
using Firebase.Storage;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Firebase.Extensions;

public class AvaProfile : MonoBehaviour
{
    [SerializeField] Image m_image;
    [SerializeField] Text numberLike;
    [SerializeField] Text m_name;
    [SerializeField] Button like;

    StorageReference m_storage;

    public void Init(User user)
    {
        //database = FirebaseDatabase.GetInstance("https://socialnetworktest-c60c5-default-rtdb.asia-southeast1.firebasedatabase.app/").GetReference("socialnetworktest-c60c5-default-rtdb");
        StorageReference storage = FirebaseStorage.DefaultInstance.GetReferenceFromUrl($"gs://socialnetworktest-c60c5.appspot.com/{user.data.urlImage}");
       // StorageReference storage = m_storage.Child(user.data.urlImage);

        storage.GetDownloadUrlAsync().ContinueWithOnMainThread( task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("Download URL: " + task.Result);
                StartCoroutine(GetTexture(task.Result.ToString()));
            }
        });

        m_name.text = user.data.name;
        numberLike.text = user.data.numberLike.ToString();
    }

    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            m_image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        }
    }
}
