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

    public GameObject sliderGO;
    public Slider slider;
    private static float byteTransferred;
    private static float byteCount;

    public GameObject wallet;


    void Start()
    {
        slider = sliderGO.GetComponent<Slider>();

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl("gs://cg-assignment1-b592f.appspot.com");
        
    }

    void Update()
    {
        wallet.GetComponent<Text>().text = "Wallet: " + coins + " coins";
    }



    public void background1()
    {
        if (coins >= 250)
        {
            sliderGO.SetActive(true);

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
            sliderGO.SetActive(true);

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
            sliderGO.SetActive(true);

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
                background.transform.localScale = new Vector2(1.75f, 1.75f);


            }
        });



    }
    

}
