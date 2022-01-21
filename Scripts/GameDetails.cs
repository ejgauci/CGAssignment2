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
        //p1 = new PlayerDetails("","","","");
        //p1 = new PlayerDetails("", "", "", "");
    }

    //public GameDetails(bool _started, PlayerDetails _p1, PlayerDetails _p2)
    //{
    //    started = _started;
    //    p1 = _p1;
    //    p1 = _p1;
    //}
    public GameDetails(string _started, string _winner)
    {
        started = _started;
        winner = _winner;
        //p1 = _p1;
        //p1 = _p1;
    }

    public GameDetails(GameDetails game)
    {

        started = game.started;
        winner = game.winner;
        //p1 = game.p1;
        //p2 = game.p2;
    }



}