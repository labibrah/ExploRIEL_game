[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int score;
    public string currentCharacterClass;
    public int syntaxPoints;
    public int morphologyPoints;
    public int phonologyPoints;

    public PlayerData()
    {
        playerName = "New Player";
        score = 0;
        currentCharacterClass = "Linguist";
    }
}
