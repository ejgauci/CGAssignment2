using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Extensions;


public class FirebaseDLCManager : MonoBehaviour
{
    const long maxAllowedSize = 1 * 1024 * 1024;

    public GameObject sliderGO;
    public Slider slider;
    private static float byteTransferred;
    private static float byteCount;

    void Start()
    {
        slider = sliderGO.GetComponent<Slider>();

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://cg-assignment1-b592f.appspot.com");


        int randNum = Random.Range(1, 4);

        StorageReference background = storageRef.Child("DLC").Child("background"+randNum+".jpg");
        //DownloadBackground(background);
        
        DownloadBackgroundDLC(background);
    }

    /*
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
    */

    

    private void DownloadBackgroundDLC(StorageReference reference)
    {
        //const long maxAllowedSize = 1 * 5096 * 5096;
        reference.GetBytesAsync(maxAllowedSize, new StorageProgress<DownloadState>(state =>
        {
            byteTransferred = state.BytesTransferred;
            byteCount = state.TotalByteCount;


            slider.value = ((byteTransferred / byteCount) * 100);


            Debug.Log(string.Format("Progress: {0} of {1} bytes transferred.",
                state.BytesTransferred,
                state.TotalByteCount
            ));

        })).ContinueWithOnMainThread(task => {


            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
            }
            else
            {


                byte[] fileContentsDLC1 = task.Result;
                slider.value = 0;
                sliderGO.SetActive(false);
                Debug.Log("Finished!");

                // Load the image into Unity

                //Create Texture
                Texture2D tex = new Texture2D(1024, 1024);
                tex.LoadImage(fileContentsDLC1);

                

                //Create Sprite
                Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
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


            }
        });
    }
    


}
