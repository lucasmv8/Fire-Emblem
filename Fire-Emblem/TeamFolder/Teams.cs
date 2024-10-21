namespace Fire_Emblem.TeamFolder;

public class Teams
{
    public Team TeamOne { get; }
    public Team TeamTwo { get; }

    public Teams(Team teamOne, Team teamTwo)
    {
        TeamOne = teamOne;
        TeamTwo = teamTwo;
    }
}