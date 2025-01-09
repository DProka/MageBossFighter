
public static class ArenaSupportScript
{
    public static int[] GetThreePointOpposite()
    {
        int startNum = GameController.Instance.player.currentPointNum + 6;
        
        int newNum = CheckNum(startNum);
        int[] sectorArray = new int[]
        {
            newNum - 1,
            newNum,
            newNum + 1
        };

        sectorArray = CheckSectorNums(sectorArray);

        return sectorArray;
    }

    public static int[] GetTwoNextClosestPoints()
    {
        int startNum = GameController.Instance.player.currentPointNum;
        int[] sector = GetPointSector(startNum, 5);
        return new int[] { sector[0], sector[4] };
    }

    public static int[] GetThreePointSector()
    {
        int startNum = GameController.Instance.player.currentPointNum;
        return GetPointSector(startNum, 5);
    }

    public static int CheckNum(int pointNum)
    {
        int newNum = pointNum;

        if (newNum > 11)
            newNum = pointNum - 12;

        else if (newNum < 0)
            newNum = 12 + pointNum;

        return newNum;
    }

    private static int[] GetPointSector(int startNum, int sectorSize)
    {
        int halfSector = sectorSize / 2;

        int[] newSector = new int[sectorSize];
        int counter = -halfSector;

        for (int i = 0; i < newSector.Length; i++)
        {
            newSector[i] = startNum + counter;
            newSector[i] = CheckNum(newSector[i]);
            counter++;

            UnityEngine.Debug.Log($"Point num = {newSector[i]}, counter = {counter}");
        }

        newSector = CheckSectorNums(newSector);

        return newSector;
    }

    private static int[] CheckSectorNums(int[] sectorArray)
    {
        for (int i = 0; i < sectorArray.Length; i++)
        {
            sectorArray[i] = CheckNum(sectorArray[i]);
        }

        return sectorArray;
    }
}
