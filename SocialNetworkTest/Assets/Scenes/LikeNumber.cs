using Firebase.Firestore;

[FirestoreData]
public struct Like
{
    [FirestoreProperty] public int LikeNumber { get; set; }
}
