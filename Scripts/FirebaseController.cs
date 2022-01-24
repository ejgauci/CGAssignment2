using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;
using Firebase.Database;
using UnityEngine.SceneManagement;


public class FirebaseController : MonoBehaviour
{
    private static DatabaseReference _dbRef;
    public static string _key = "";
    public static string _started = "";
    public static string _player1 = "";
    public static string _player2 = "";

    public bool checkedGame = false;
    public bool inGame = false;

        
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        _dbRef = FirebaseDatabase.DefaultInstance.RootReference;


    }
    

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby_p2" && checkedGame==false)
        {
            checkedGame = true;
            CheckStarted();
        }

        if (_started == "true" && inGame==false)
        {
            inGame = true;
            SceneManager.LoadScene("Game");
            
        }
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
        //GameDetails game = new GameDetails();
        _key = _dbRef.Child("Games").Push().Key;

        GameDetails game = new GameDetails("false", "", _key, "");
        yield return _dbRef.Child("Games").Child(_key).Child("GameDetails").SetRawJsonValueAsync(JsonUtility.ToJson(game));


        //yield return _dbRef.Child("Games").Child(_key).Child("GameDetails").SetRawJsonValueAsync(JsonUtility.ToJson(game));


        PlayerDetails p1 = new PlayerDetails(player1, "", "", "");
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


        PlayerDetails p2 = new PlayerDetails(_player2, "", "", "");
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

        

        GameDetails game = new GameDetails("true", "", _key, "");
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
    public static void UpdateScore(string score)
    {
        if (GameManager.gamePlayer == 1)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player1").Child("score")
                .SetValueAsync(score);
        }
        else
        if (GameManager.gamePlayer == 2)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").Child("score")
                .SetValueAsync(score);
        }
    }

    public static void UpdateMoves(string moves)
    {
        if (GameManager.gamePlayer == 1)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player1").Child("moves")
                .SetValueAsync(moves);
        }
        else
        if (GameManager.gamePlayer == 2)
        {
            _dbRef.Child("Games").Child(_key).Child("Objects").Child("Player2").Child("moves")
                .SetValueAsync(moves);
        }
    }

    public static void setTotalTime(string ttime)
    {
        if (GameManager.gamePlayer == 1)
        {
            _dbRef.Child("Games").Child(_key).Child("GameDetails").Child("totalTime")
                .SetValueAsync(ttime);
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

    
    public static string _p1Score;
    public static void getP1Score()
    {
        Debug.Log("get p1 Score");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_GetP1Score;
    }

    private static void FirebaseController_GetP1Score(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            _p1Score = e.Snapshot.Child(_key).Child("Objects").Child("Player1").Child("score").GetValue(true).ToString();
        }

        Debug.Log("P1 score:" + _p1Score);
        WinSceneScript.setP1Score(_p1Score);

    }


    public static string _p2Score;
    public static void getP2Score()
    {
        Debug.Log("get p1 Score");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_GetP2Score;
    }

    private static void FirebaseController_GetP2Score(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            _p2Score = e.Snapshot.Child(_key).Child("Objects").Child("Player2").Child("score").GetValue(true).ToString();
        }

        Debug.Log("P2 score:" + _p2Score);
        WinSceneScript.setP2Score(_p2Score);

    }


    public static string _p1Moves;
    public static void getP1Moves()
    {
        Debug.Log("get p1 moves");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_GetP1Moves;
    }

    private static void FirebaseController_GetP1Moves(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            _p1Moves = e.Snapshot.Child(_key).Child("Objects").Child("Player1").Child("moves").GetValue(true).ToString();
        }

        Debug.Log("P1 moves:" + _p1Moves);
        WinSceneScript.setP1Moves(_p1Moves);

    }


    public static string _p2Moves;
    public static void getP2Moves()
    {
        Debug.Log("get p1 Score");
        FirebaseDatabase.DefaultInstance.GetReference("Games").ValueChanged += FirebaseController_GetP2Moves;
    }

    private static void FirebaseController_GetP2Moves(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("error msg");
            return;
        }
        else
        {
            _p2Moves = e.Snapshot.Child(_key).Child("Objects").Child("Player2").Child("moves").GetValue(true).ToString();
        }

        Debug.Log("P2 score:" + _p2Moves);
        WinSceneScript.setP2Moves(_p2Moves);

    }






}
