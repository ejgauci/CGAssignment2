using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Extensions;

public class FirebaseDLCManager : MonoBehaviour
{
    const long maxAllowedSize = 1 * 1024 * 1024;

    void Start()
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://cg-assignment1-b592f.appspot.com");
        
        
        StorageReference background = storageRef.Child("DLC").Child("background.jpg");

        DownloadBackground(background);
    }


    private void DownloadBackground(StorageReference reference)
    {
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
            }
            else
            {

                byte[] fileContents = task.Result;

                // Load the image into Unity

                //Create Texture
                Texture2D redTexture = new Texture2D(1024, 1024);
                redTexture.LoadImage(fileContents);


                //Create Sprite
                Sprite mySprite = Sprite.Create(redTexture, new Rect(0.0f, 0.0f, redTexture.width, redTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                GameObject redBox = new GameObject();
                redBox.gameObject.transform.localScale = new Vector2(1.25f, 1.25f);
                redBox.transform.position = new Vector2(0f, 0f);

                redBox.AddComponent<SpriteRenderer>().sprite = mySprite;

                if (GameManager.gamePlayer == 2)
                {
                    redBox.AddComponent<PlayerController>();
                }


                redBox.name = "Background";
                Debug.Log("Finished downloading Background!");
            }
        });

    }

}
