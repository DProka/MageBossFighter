
public class ArenaStatistic
{
    public int gameLvl { get; private set; }
    public int arenaNum { get; private set; }
    public int bossNum { get; private set; }

    public ArenaStatistic()
    {
        gameLvl = 0;
        arenaNum = 0;
        bossNum = 0;
    }

    public void UpdateStatistic(int[] stats)
    {
        gameLvl = stats[0];
        arenaNum = stats[1];
        bossNum = stats[2];
    }
}
