using System;

public class PlayerDetails
{
    public string username;
    public string weapon;
    public string score;
    public string id;
    public string moves;

    
    
    public PlayerDetails()
    {
        username = "";
        weapon = "";
        score = "";
        id = "";
        moves = "";
    }

    public PlayerDetails(string _username, string _weapon, string _score, string _moves)
    {
        username = _username;
        weapon = _weapon;
        score = _score;
        moves = _moves;

        Guid guid = Guid.NewGuid();
        string str = guid.ToString();

        id = str;
        moves = _moves;
    }

    public PlayerDetails(PlayerDetails player)
    {

        username = player.username;
        weapon = player.weapon;
        score = player.score;
        id = player.id;
        moves = player.moves;
    }





}