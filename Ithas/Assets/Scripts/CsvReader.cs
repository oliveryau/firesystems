using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class Player
    {
        public int level;
        public float hp;
        public float moveSpeed;
        public float exp;
    }

    [System.Serializable]
    public class PlayerLevel
    {
        public Player[] player;
    }

    public PlayerLevel myPlayerLevel = new PlayerLevel();

    private void Awake()
    {
        ReadCsv();
    }

    private void ReadCsv()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = (data.Length) / 4 - 1;
        myPlayerLevel.player = new Player[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myPlayerLevel.player[i] = new Player();

            myPlayerLevel.player[i].level = int.Parse(data[4 * (i + 1)]);
            myPlayerLevel.player[i].hp = float.Parse(data[4 * (i + 1) + 1]);
            myPlayerLevel.player[i].moveSpeed = float.Parse(data[4 * (i + 1) + 2]);
            myPlayerLevel.player[i].exp = float.Parse(data[4 * (i + 1) + 3]);
        }
    }
}
