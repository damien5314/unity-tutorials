using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int Minimum;
        public int Maximum;

        public Count (int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }
    }

    public int Columns = 8;
    public int Rows = 8;
    public Count WallCount = new Count(min: 5, max: 9);
    public Count FoodCount = new Count(min: 1, max: 5);
    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;
    public GameObject[] OuterWallTiles;
    
    // Used to collapse GameObjects so we don't clutter up the heirarchy
    private Transform _boardHolder;
    private List<Vector3> _gridPositions = new List<Vector3>();

    private void InitializeList()
    {
        _gridPositions.Clear();

        for (var x = 1; x < Columns - 1; x++)
        {
            for (var y = 1; y < Rows - 1; y++)
            {
                _gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    private void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;

        for (var x = -1; x < Columns + 1; x++)
        {
            for (var y = -1; y < Rows + 1; y++)
            {
                GameObject toInstantiate;

                // Check if we're at a position on the outer wall
                if (x == -1 || x == Columns || y == -1 || y == Rows)
                {
                    toInstantiate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];
                }
                // Otherwise we're on an inner floor tile
                else
                {
                    toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];
                }

                var instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(_boardHolder);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        var randomIndex = Random.Range(min: 0, max: _gridPositions.Count);
        var randomPosition = _gridPositions[randomIndex];
        _gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        var objectCount = Random.Range(minimum, maximum + 1);

        for (var i = 0; i < objectCount; i++)
        {
            var randomPosition = RandomPosition();
            var tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(WallTiles, WallCount.Minimum, WallCount.Maximum);
        LayoutObjectAtRandom(FoodTiles, FoodCount.Minimum, FoodCount.Maximum);
        // Minimum and maximum enemy tiles are the same because we're not selecting a value at random
        var enemyCount = (int) Mathf.Log(level, 2f);
        LayoutObjectAtRandom(EnemyTiles, enemyCount, enemyCount);
        Instantiate(Exit, new Vector3(Columns - 1, Rows - 1, 0f), Quaternion.identity);
    }

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
    private void Update () {
		
	}
}
