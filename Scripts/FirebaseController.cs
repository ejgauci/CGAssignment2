using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;
using Firebase.Database;


public class FirebaseController : MonoBehaviour
{
    private static DatabaseReference _dbRef;
    public static string _key = "";
    public static string _started = "";
    public static string _player1 = "";
    public static string _player2 = "";

        
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        
    }

    private void Update()
    {

    }


    //When a player joins the lobby, we should know
    public static void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            Debug.Log("Someone joined the lobby");
            foreach (var child in args.Snapshot.Children)
            {
                if (child.Key == "username")
                {
                    _player2 = child.Value.ToString();
                    Debug.Log(_player2 + " has joined the lobby");
                    
                    //GameManager.RefreshLobby();
                }
            }
            
        }
    }
    
    public static IEnumerator CreateGame(string player1){

        _player1 = player1;
        GameDetails game = new GameDetails();
        _key = _dbRef.Child("Games").Push().Key;

        yield return _dbRef.Child("Games").Child(_key).Child("GameDetails").SetRawJsonValueAsync(JsonUtility.ToJson(game));


        PlayerDetails p1 = new PlayerDetails(player1, "", "");
        yield return _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player1").SetRawJsonValueAsync(JsonUtility.ToJson(p1));


        PlayerDetails p2 = new PlayerDetails();
        yield return _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").SetRawJsonValueAsync(JsonUtility.ToJson(p2));
        //_Player2ID = _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").Push().Key;

        //Listen to any changes in this lobby
        _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").ValueChanged += HandleValueChanged;
        GameManager.NextScene("Lobby");
        

    }



    public static void AddToLobby(string key)
    {
       
        Debug.Log("the key is" + key);
        Debug.Log("p2 name" + _player2);


        PlayerDetails p2 = new PlayerDetails(_player2, "", "");
        _dbRef.Child("Games").Child(key).Child("Objects").Child("Player2").SetRawJsonValueAsync(JsonUtility.ToJson(p2));

      
    }
    

    public static IEnumerator KeyExists(String key)
    {
        yield return _dbRef.Child("Games").Child(key).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null)
                {
                    Debug.Log("Correct Key");

                    //Optimise
                    foreach (var child in snapshot.Children)
                    {
                        if(child.Key == "_player1")
                        {
                            _player1 = child.Value.ToString();
                        }
                    }
                    _key = key;
                    AddToLobby(key);
                }  
            }
            else{
                Debug.Log("Incorrect Key");
            }
        });
    }


    public static IEnumerator GameStarted()
    {

        Debug.Log("(game started) the game key is" + _key);

        GameDetails game = new GameDetails("true", "");
        yield return _dbRef.Child("Games").Child(_key).Child("GameDetails").SetRawJsonValueAsync(JsonUtility.ToJson(game));
        

    }

    public void CheckStarted()
    {
        Debug.Log("Check if started co-routine");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_ValueChanged;
    }

    private void FirebaseController_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            _started = e.Snapshot.Child(_key).Child("GameDetails").Child("started").GetValue(true).ToString();
        }

        Debug.Log("did the game start:" + _started);
    }

    //public void ListenIfGameStarted()
    //{
    //    //Listen if game started

    //    Debug.Log("qijed aw bro");
    //    _dbRef.Child("Games").Child(_key).Child("GameDetails").ValueChanged += IfGameStarted;
    //}


    ////if game is started by player 1
    //public static void IfGameStarted(object sender, ValueChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null)
    //    {
    //        Debug.LogError(args.DatabaseError.Message);
    //        return;
    //    }
    //    else
    //    {
    //        Debug.Log("listening if started");
    //        foreach (var child in args.Snapshot.Children)
    //        {
    //            if (child.Key == "started")
    //            {
    //                _started = child.Value.ToString();
    //                Debug.Log("Game Started by player1");

    //            }
    //        }

    //    }
    //}


    public static void UpdateWeapon(string weapon)
    {
        if (GameManager.gamePlayer == 1)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player1").Child("weapon")
                .SetValueAsync(weapon);
        }
        else
        if (GameManager.gamePlayer == 2)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").Child("weapon")
                .SetValueAsync(weapon);
        }
    }

    public static void WonGame()
    {
        if (GameManager.gamePlayer == 1)
        {
            _dbRef.Child("Games").Child(_key).Child("GameDetails").Child("winner")
                .SetValueAsync("Player1");
        }
        else
        if (GameManager.gamePlayer == 2)
        {
            _dbRef.Child("Games").Child(_key).Child("GameDetails").Child("winner")
                .SetValueAsync("Player2");
        }
    }


    private static int enemyNumber = 0;
    public static string _enemyWeapon;
    public static void getEnemyWeapon()
    {
        Debug.Log("get enemy weapon");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_GetWeapon;
    }

    private static void FirebaseController_GetWeapon(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            if (GameManager.gamePlayer == 1)
            {
                enemyNumber = 2;
            }
            else
            if (GameManager.gamePlayer == 2)
            {
                enemyNumber = 1;
            }

            _enemyWeapon = e.Snapshot.Child(_key).Child("Objects").Child("Player"+ enemyNumber).Child("weapon").GetValue(true).ToString();
        }

        Debug.Log("Enemy Weapon:" + _enemyWeapon);
        PlayerController.setEnWeapon(_enemyWeapon);

    }
    


}
