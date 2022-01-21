public class GameDetails
{
    public string started;
    public string winner;

    //PlayerDetails p1;
    //private PlayerDetails p2;


    public GameDetails()
    {
        started = "false";
        winner = "";
    }

 
    public GameDetails(string _started, string _winner)
    {
        started = _started;
        winner = _winner;
    }

    public GameDetails(GameDetails game)
    {

        started = game.started;
        winner = game.winner;
    }



}