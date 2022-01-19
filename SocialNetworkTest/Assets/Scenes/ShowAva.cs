using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class ShowAva : MonoBehaviour
{
    [SerializeField] Button refresh;
    [SerializeField] Transform rect;
    [SerializeField] AvaProfile avaPrefab;

    DatabaseReference database;
    private void Start()
    {
        database = FirebaseDatabase.GetInstance("https://socialnetworktest-c60c5-default-rtdb.asia-southeast1.firebasedatabase.app/").GetReference("socialnetworktest-c60c5-default-rtdb");
        refresh.onClick.AddListener(ReFresh);

    }

    void ReFresh()
    {
        database.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("refresh fail");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.Child("share_image").GetRawJsonValue();
                //Debug.Log(json);
                if (json != null)
                {
                    Dictionary<string,UserData> users = JsonMapper.ToObject<Dictionary<string, UserData>>(json);

                    if (users == null)
                    {
                        Debug.Log("json to obj fail");
                    }

                    foreach (var i in users)
                    {
                        AvaProfile profile = Instantiate<AvaProfile>(avaPrefab, rect);
                        profile.Init(new User(i.Key, i.Value));
                    }
                }
            }
        });
    }
}
