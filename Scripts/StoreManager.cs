using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.SceneManagement;


public class StoreManager : MonoBehaviour
{
    const long maxAllowedSize = 1 * 1024 * 1024;

    private StorageReference storageRef;

    public int coins = 1000;

    void Start()
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://cg-assignment1-b592f.appspot.com");
        
    }



    public void background1()
    {
        if (coins >= 250)
        {
            if (GameObject.Find("Background"))
            {
                print("there is already a background");
                Destroy(GameObject.Find("Background"));
            }
            coins = coins - 250;
            StorageReference background1 = storageRef.Child("DLC").Child("background1.jpg");

            DownloadBackground(background1);
        }
        
    }

    public void background2()
    {
        if (coins >= 600)
        {
            if (GameObject.Find("Background"))
            {
                print("there is already a background");
                Destroy(GameObject.Find("Background"));
            }

            coins = coins - 600;
            StorageReference background2 = storageRef.Child("DLC").Child("background2.jpg");

            DownloadBackground(background2);
        }
    }

    public void background3()
    {
        if (coins >= 1000)
        {
            if (GameObject.Find("Background"))
            {
                print("there is already a background");
                Destroy(GameObject.Find("Background"));
            }

            coins = coins - 1000;
            StorageReference background3 = storageRef.Child("DLC").Child("background3.jpg");

            DownloadBackground(background3);
        }
    }

    public void goBack()
    {
        SceneManager.LoadScene("Welcome");
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
                Texture2D bgTex = new Texture2D(1024, 1024);
                bgTex.LoadImage(fileContents);


                //Create Sprite

                Sprite mySprite = Sprite.Create(bgTex, new Rect(0.0f, 0.0f, bgTex.width, bgTex.height), new Vector2(0.5f, 0.5f), 100.0f);
                GameObject background = new GameObject();
                background.gameObject.transform.localScale = new Vector2(1.25f, 1.25f);
                background.transform.position = new Vector2(0f, 0f);
                

                background.AddComponent<SpriteRenderer>().sprite = mySprite;

                if (GameManager.gamePlayer == 2)
                {
                    background.AddComponent<PlayerController>();
                }


                background.name = "Background";
                Debug.Log("Finished downloading Background!");
                background.transform.localScale = new Vector2(1.75f, 1.75f);
            }
        });

        

    }
    
    

}
