using System;

public class PlayerDetails
{
    public string username;
    public string weapon;
    public string score;
    public string id;

    
    
    public PlayerDetails()
    {
        username = "";
        weapon = "";
        score = "";
        id = "";
    }

    public PlayerDetails(string _username, string _weapon, string _score)
    {
        username = _username;
        weapon = _weapon;
        score = _score;

        Guid guid = Guid.NewGuid();
        string str = guid.ToString();

        id = str;
    }

    public PlayerDetails(PlayerDetails player)
    {

        username = player.username;
        weapon = player.weapon;
        score = player.score;
        id = player.id;
    }





}