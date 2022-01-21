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
        
        
        StorageReference blueCircle = storageRef.Child("DLC").Child("blueCircle.png");
        StorageReference redBox = storageRef.Child("DLC").Child("redBox.png");

        DownloadBlueImage(blueCircle);
        DownloadRedImage(redBox);
    }



    //Download File from Firebase Storage
    private void DownloadBlueImage(StorageReference reference)
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
                Texture2D blueTexture = new Texture2D(1024, 1024);
                blueTexture.LoadImage(fileContents);
                

                //Create Sprite
                Sprite mySprite = Sprite.Create(blueTexture, new Rect(0.0f, 0.0f, blueTexture.width, blueTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                GameObject blueCircle = new GameObject();
                blueCircle.gameObject.transform.localScale = new Vector2(0.2f, 0.2f);
                blueCircle.transform.position = new Vector2(-4f, 0f);

                blueCircle.AddComponent<SpriteRenderer>().sprite = mySprite;
                blueCircle.AddComponent<Rigidbody2D>().gravityScale = 0;


                if (GameManager.gamePlayer == 1)
                {
                    blueCircle.AddComponent<PlayerController>();
                }
                   

                blueCircle.name = "Blue Circle";
                Debug.Log("Finished downloading blue circle!");
            }
        });

    }

    //Download File from Firebase Storage
    private void DownloadRedImage(StorageReference reference)
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
                redBox.gameObject.transform.localScale = new Vector2(0.2f, 0.2f);
                redBox.transform.position = new Vector2(4f, 0f);

                redBox.AddComponent<SpriteRenderer>().sprite = mySprite;
                redBox.AddComponent<Rigidbody2D>().gravityScale = 0;

                if (GameManager.gamePlayer == 2)
                {
                    redBox.AddComponent<PlayerController>();
                }


                redBox.name = "Red Box";
                Debug.Log("Finished downloading red box!");
            }
        });

    }
}
