using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;

public class SaveFileTXT : MonoBehaviour
{

    private int _score = 27;
    private float _duration = 87.5f;
    private string _date = "14/12/2021 08:55";

    private string matchID = "";
    private int p1Moves = 0;
    private int p2Moves = 0;
    private string winner = "";
    private string timetaken = "";


    public void saveTXT(string _matchID, int _p1Moves, int _p2Moves, string _winner, string _timetaken)
    {
        matchID = _matchID;
        p1Moves = _p1Moves;
        p2Moves = _p2Moves;
        winner = _winner;
        timetaken = _timetaken;

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://cg-assignment1-b592f.appspot.com");

        string DataString = "Match ID: " + _matchID + "\nP1 Moves: " + _p1Moves.ToString() + "\nP2 Moves: " +
                            _p2Moves.ToString() + "\nWinner: " + _winner + "\nTime Taken: " + _timetaken;

        Debug.Log(DataString);
        byte[] data = Encoding.ASCII.GetBytes(DataString);
        StartCoroutine(UploadTextFile(data, storageRef, _matchID));
    }


    private IEnumerator UploadTextFile(byte[] data, StorageReference reference, string fileName)
    {
        // Create a reference to the file you want to upload
        StorageReference textFileRef = reference.Child(fileName+".txt");

        // Upload the file to the path "text.txt"
        yield return textFileRef.PutBytesAsync(data)
            .ContinueWithOnMainThread((task) => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                    // Uh-oh, an error occurred!
                }
                else
                {
                    // Metadata contains file metadata such as size, content-type, and md5hash.
                    StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
                    Debug.Log("Finished uploading...");
                    Debug.Log("md5 hash = " + md5Hash);
                }
            });
    }


}



