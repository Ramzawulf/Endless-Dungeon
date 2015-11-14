#region

#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Assets.Scripts.Dungeon
{

    #endregion

    public struct DifficultyContributions
    {
        public float Enemy;
        public float Pit;
        public float Trap;
    }

    public class Dungeon
    {
        private readonly GameObject _chunkPrefab;
        private readonly List<Chunk> _chunks;
        private readonly GameObject _doorPrefab;
        private readonly List<Enemy> _enemies;
        private readonly GameObject _enemyPrefab;
        private readonly Vector3 _floorReference;
        private readonly int _length;
        private readonly GameObject _platformPrefab;
        private readonly List<GameObject> _platforms;
        private DifficultyContributions _contributions;
        /* Debugging variables*/
        private int _difficulty = 1;
        private int _pits;
        private Vector3 _roofReference;
        /* Difficulty Variables */
        private int _totalPits;
        private int _totalTraps;

        public Dungeon(GameObject chunkPrefab, GameObject platformPrefab,
            GameObject doorPrefab, GameObject enemyPrefab,
            Vector3 floorReference, Vector3 roofReference, int length)
        {
            _contributions.Pit = 0; //0.1f;
            _contributions.Enemy = 0.025f;
            _contributions.Trap = 0.02f;
            _length = length;
            _floorReference = floorReference;
            _roofReference = roofReference;
            _chunkPrefab = chunkPrefab;
            _platformPrefab = platformPrefab;
            _doorPrefab = doorPrefab;
            _enemyPrefab = enemyPrefab;
            _chunks = new List<Chunk>();
            _platforms = new List<GameObject>();
            _enemies = new List<Enemy>();
            CreateFloor();
        }

        private int NumberChunks
        {
            get { return Mathf.FloorToInt(_length/3); }
        }

        private float PitDifficultyContribution
        {
            get { return _contributions.Pit*_totalPits; }
        }

        private int TotalEnemies
        {
            get { return _enemies.Count; }
        }

        private float EnemyDifficultyContribution
        {
            get { return _contributions.Enemy*TotalEnemies; }
        }

        private float TrapDifficultyContribution
        {
            get { return _contributions.Trap*_totalTraps; }
        }

        public void SetDifficulty(int newDifficulty)
        {
            var neededDifficulty = newDifficulty - PitDifficultyContribution;
            _difficulty = newDifficulty;

            if (neededDifficulty > 0)
            {
                /* Enemy Spawning */
                var spawnPoints = GetElevationsByHeight(3);
                spawnPoints.AddRange(GetElevationsByHeight(2));
                spawnPoints.AddRange(GetElevationsByHeight(1));
                var elementsToBeSpawned = Mathf.FloorToInt(neededDifficulty*0.3f);
                Vector3 position;
                Elevation tempElevation;
                var usedIndexes = new List<float>();
                int rnd;
                var enemiesToSpawn = Mathf.Clamp(elementsToBeSpawned/_contributions.Enemy, 0, spawnPoints.Count);
                enemiesToSpawn = Mathf.Max(enemiesToSpawn, 10.0f);

                for (var i = 0; i < enemiesToSpawn; i++)
                {
                    do
                    {
                        rnd = Random.Range(0, spawnPoints.Count);
                    } while (usedIndexes.Find(item => item == rnd) != 0.0F);
                    usedIndexes.Add(rnd);
                    tempElevation = spawnPoints[rnd];
                    position = tempElevation.position;

                    SpawnEnemyAt(position + new Vector3(0, 1 + tempElevation.height, 0));
                    spawnPoints.RemoveAt(rnd);
                }

                /* Trap Spawn */
                usedIndexes.Clear();
                var tempBrick = 0;
                if (spawnPoints.Count == 0)
                {
                    return;
                }
                elementsToBeSpawned = Mathf.CeilToInt(neededDifficulty*0.7f);
                for (var i = 0; i < elementsToBeSpawned/_contributions.Trap; i++)
                {
                    do
                    {
                        rnd = Random.Range(0, spawnPoints.Count);
                    } while (usedIndexes.Find(index => index == rnd) != 0.0F);
                    tempElevation = spawnPoints[rnd];
                    tempBrick = Random.Range(1, tempElevation.height + 1);
                    if (spawnTraps(tempElevation, tempBrick, Random.Range(1, 4)))
                    {
                        _totalTraps++;
                    }
                }
            }
        }

        private bool spawnTraps(Elevation elevation, int brick, int trapLocation = 0)
        {
            if (trapLocation == 0)
            {
                trapLocation = Random.Range(1, 4);
            }
            switch (brick)
            {
                case 1:
                    switch (trapLocation)
                    {
                        case 1:
                            elevation.bottomBrick.activateSaw(Brick.SAW_LEFT);
                            return true;
                        case 2:
                            elevation.bottomBrick.activateSaw(Brick.SAW_UP);
                            return true;
                        case 3:
                            elevation.bottomBrick.activateSaw(Brick.SAW_RIGHT);
                            return true;
                    }
                    break;
                case 2:
                    switch (trapLocation)
                    {
                        case 1:
                            elevation.middleBrick.activateSaw(Brick.SAW_LEFT);
                            return true;
                        case 2:
                            elevation.middleBrick.activateSaw(Brick.SAW_UP);
                            return true;
                        case 3:
                            elevation.middleBrick.activateSaw(Brick.SAW_RIGHT);
                            return true;
                    }
                    break;
                case 3:
                    switch (trapLocation)
                    {
                        case 1:
                            elevation.upperBrick.activateSaw(Brick.SAW_LEFT);
                            return true;
                        case 2:
                            elevation.upperBrick.activateSaw(Brick.SAW_UP);
                            return true;
                        case 3:
                            elevation.upperBrick.activateSaw(Brick.SAW_RIGHT);
                            return true;
                    }
                    break;
            }
            return false;
        }

        private void CreateFloor()
        {
            GameObject obj;
            obj = (GameObject) Object.Instantiate(_chunkPrefab, _floorReference, Quaternion.identity);
            _chunks.Add(obj.GetComponent<Chunk>());
            _chunks[0].create(0, 3);

            for (var i = 1; i < NumberChunks; i++)
            {
                obj =
                    (GameObject)
                        Object.Instantiate(_chunkPrefab, _floorReference + new Vector3(3, 0, 0)*i, Quaternion.identity);
                _chunks.Add(obj.GetComponent<Chunk>());
                _chunks[i].create(0, _chunks[i - 1].rightHeight);
            }
            ValidateFloor();
            SpawnDoor();
        }

        private void ValidateFloor()
        {
            var i = 0;
            for (var c = 0; c < _chunks.Count; c++)
            {
                if (_chunks[c].leftHeight == 0)
                {
                    _totalPits++;
                    i++;
                    if (i > 2)
                    {
                        SpawnPlatform(_chunks[c].leftPosition + new Vector3(0, 1, 0));
                        _totalPits--;
                    }
                }
                else
                {
                    i = 0;
                }

                if (_chunks[c].middleHeight == 0)
                {
                    i++;
                    if (i > 2)
                    {
                        SpawnPlatform(_chunks[c].middlePosition + new Vector3(0, 1, 0));
                        _totalPits--;
                    }
                }
                else
                {
                    i = 0;
                }

                if (_chunks[c].rightHeight == 0)
                {
                    i++;
                    if (i > 2)
                    {
                        SpawnPlatform(_chunks[c].rightPosition + new Vector3(0, 1, 0));
                        _totalPits--;
                    }
                }
                else
                {
                    i = 0;
                }
            }
        }

        private void SpawnPlatform(Vector3 position)
        {
            _platforms.Add((GameObject) Object.Instantiate(_platformPrefab, position, Quaternion.identity));
        }

        private void SpawnDoor()
        {
            var position = _chunks[_chunks.Count - 1].middlePosition;
            Object.Instantiate(_doorPrefab, position + new Vector3(0, 5, 0), Quaternion.identity);
        }

        private void SpawnEnemyAt(Vector3 position)
        {
            var gmObj = (GameObject) Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
            _enemies.Add(gmObj.GetComponent<Enemy>());
        }

        private List<Elevation> GetElevationsByHeight(int height)
        {
            var result = new List<Elevation>();
            foreach (var c in _chunks)
            {
                if (c.leftHeight == height)
                {
                    result.Add(c.leftElevation);
                }
                if (c.middleHeight == height)
                {
                    result.Add(c.middleElevation);
                }
                if (c.rightHeight == height)
                {
                    result.Add(c.rightElevation);
                }
            }

            return result;
        }

        public void AddRoofSaws(int numOfSaws)
        {
        }

        public void addFloorSaws(int numOfSaws)
        {
        }
    }
}