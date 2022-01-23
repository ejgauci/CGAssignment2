using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField playerNameInput;
    [SerializeField] private TMPro.TMP_InputField uniqueCodeOutput;
    [SerializeField] private TMPro.TMP_InputField uniqueCodeInput;
    [SerializeField] private TMPro.TMP_Text player1Name;
    [SerializeField] private TMPro.TMP_Text player2Name;
    [SerializeField] public static int gamePlayer = 0;


    private void Awake()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Welcome":
                break;
            case "Lobby":
                uniqueCodeOutput.text = FirebaseController._key;
                player1Name.text = "Player 1: " + FirebaseController._player1;
                player2Name.text = "Player 2: " + FirebaseController._player2;
                break;
            case "Join":
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        //if (SceneManager.GetActiveScene().name == "Welcome")
        //DontDestroyOnLoad(this.gameObject);

    }


    public static void NextScene(string SceneName)
    {
        
        SceneManager.LoadScene(SceneName);
    }

    //Welcome Scene
    public void CreateGame(){
        if (playerNameInput.text != "")
        {
            gamePlayer = 1;
            StartCoroutine(FirebaseController.CreateGame(playerNameInput.text));
        }
    }

    public void JoinGame()
    {
        if (playerNameInput.text != "")
        {
            gamePlayer = 2;
            FirebaseController._player2 = playerNameInput.text;
            NextScene("Join");
        }
    }


    //Lobby
    public void RefreshLobby()
    {
        player1Name.text = "Player 1: " + FirebaseController._player1;
        player2Name.text = "Player 2: " + FirebaseController._player2;
    }

    //public void GetP1()
    //{
    //    StartCoroutine(FirebaseController.GetP1(uniqueCodeInput.text));
    //}

    public void StartGame(){

        if ( player2Name.text != "Player 2: ")
        {
            StartCoroutine(FirebaseController.GameStarted());
            NextScene("Game");
        }        
    }


    public void EnterGame()
    {
        NextScene("Game");
    }

  

    //Join Scene
    public void JoinGameLobby()
    {
        if (uniqueCodeInput.text != "")
        {
            StartCoroutine(FirebaseController.KeyExists(uniqueCodeInput.text));
            
            NextScene("Lobby_p2");
        }
    }

    public void EnterStore()
    {
        NextScene("Store");
    }

}
