using System;

public class PlayerDetails
{
    public string username;
    public string posX;
    public string posY;
    public string shape;
    public string time;
    public string id;

    
    
    public PlayerDetails()
    {
        username = "";
        posX = "";
        posY = "";
        shape = "";
        id = "";
    }

    public PlayerDetails(string _username, string _posX, string _posY, string _shape)
    {
        username = _username;
        posX = _posX;
        posY = _posY;
        shape = _shape;

        DateTime now = DateTime.Now;
        time = now.ToString();

        Guid guid = Guid.NewGuid();
        string str = guid.ToString();

        id = str;
    }

    public PlayerDetails(PlayerDetails player)
    {

        username = player.username;
        posX = player.posX;
        posY = player.posY;
        shape = player.shape;
        time = player.time;
        id = player.id;
    }


    //public string username;
    //public string icon;
    //public string attack;



}