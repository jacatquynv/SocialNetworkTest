using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

public class FireStoreManager : MonoBehaviour
{
    FirebaseFirestore db;
    ListenerRegistration listenerRegistration;
    [SerializeField] Button like;
    [SerializeField] Text likeNumber;
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        like.onClick.AddListener(OnClickLike);

        listenerRegistration = db.Collection("ShareAvatar").Document("ShareAvaTest").Listen(snapshot =>
        {
            LikeNumber like = snapshot.ConvertTo<LikeNumber>();
            likeNumber.text = like.LikeNumber.ToString();
        });
    }


    void OnClickLike()
    {
        //int old = int.Parse(likeNumber.text);
        //Like counter = new Like
        //{
        //    Count = oldCount + 1,
        //    UpdatedBy = "Vikings"
        //};

        //// Using Dictionary
        //// Dictionary<string, object> counter = new Dictionary<string, object>
        //// {
        ////     {"Count",oldCount+1},
        ////     {"UpdatedBy","Vikings(Dictionary)"}
        //// };
        //DocumentReference countRef = db.Collection("counters").Document("counter");
        //countRef.SetAsync(counter).ContinueWithOnMainThread(task =>
        //{
        //    Debug.Log("Updated Counter");
        //    // GetData();
        //});
    }

    private void OnDestroy()
    {
        listenerRegistration.Stop();
    }
}
