using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovePointsSupport
{
    public static int[] GetThreePointOpposite()
    {
        int startNum = GameController.Instance.player.currentPointNum;
        startNum = CheckNum(startNum, 6);
        return GetPointSector(startNum, 3);
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

    private static int[] GetPointSector(int startNum, int sectorSize)
    {
        int halfSector = sectorSize / 2;

        int[] leftPoints = new int[halfSector];
        int[] rightPoints = new int[halfSector];

        for (int i = 0; i < leftPoints.Length; i++)
        {
            int newNum = startNum + i + 1;
            newNum = CheckNum(newNum, i);
            leftPoints[i] = newNum;
        }

        for (int i = 0; i < rightPoints.Length; i++)
        {
            int newNum = startNum - i - 1;
            newNum = CheckNum(newNum, i);
            rightPoints[i] = newNum;
        }

        List<int> newList = new List<int>(leftPoints);
        newList.Add(startNum);
        newList.AddRange(rightPoints);

        return newList.ToArray();
    }

    private static int CheckNum(int num, int count)
    {
        int newNum = num;

        if (newNum >= 12)
            newNum = 0 + count;

        else if (newNum < 0)
            newNum = 11 - count;

        return newNum;
    }
}
