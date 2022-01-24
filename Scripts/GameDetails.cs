public class GameDetails
{
    public string started;
    public string winner;
    public string keyID;
    public string totalTime;

    //PlayerDetails p1;
    //private PlayerDetails p2;


    public GameDetails()
    {
        started = "false";
        winner = "";
        keyID = "";
        totalTime = "";
;    }

 
    public GameDetails(string _started, string _winner, string _key, string _totalTime)
    {
        started = _started;
        winner = _winner;
        keyID = _key;
        totalTime = _totalTime;
    }

    public GameDetails(GameDetails game)
    {

        started = game.started;
        winner = game.winner;
        keyID = game.keyID;
        totalTime = game.totalTime;
    }



}