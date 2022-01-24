public class GameDetails
{
    public string started;
    public string winner;
    public string keyID;

    //PlayerDetails p1;
    //private PlayerDetails p2;


    public GameDetails()
    {
        started = "false";
        winner = "";
        keyID = "";
    }

 
    public GameDetails(string _started, string _winner, string _key)
    {
        started = _started;
        winner = _winner;
        keyID = _key;
    }

    public GameDetails(GameDetails game)
    {

        started = game.started;
        winner = game.winner;
        keyID = game.keyID;
    }



}