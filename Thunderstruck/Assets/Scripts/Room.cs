using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{

    public class Room
    {
        public Point point;
        //right is 0, top is 1, etc
        private readonly int previousDirection;
        public int numEnemies;
        public int difficulty;
        private List<GameObject> doors = new List<GameObject>();

        public Room(int previousDirection, Point point, int difficulty)
        {
            this.previousDirection = previousDirection;
            this.point = point;
            this.difficulty = difficulty;
            CalculateEnemies();

        }

        public void CalculateEnemies()
        {
            if (point.x == 0 && point.y == 0)
            {
                numEnemies = 0;
                return;
            }
            int minEnemies = 1;
            int maxEnemies = 5 + difficulty * 2;
            numEnemies = MainScript.r.Next(minEnemies, maxEnemies);

        }
        public void AddDoor(GameObject door)
        {
            doors.Add(door);
        }
        public void SpawnEnemies()
        {
            var enemyAsset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprites/EnemyCloudLevel1.prefab");
            for (int i = 0; i < numEnemies; i++)
            {
                float xLoc = UnityEngine.Random.Range(-1 * MainScript.mapWidth / 2, MainScript.mapWidth / 2);
                float yLoc = UnityEngine.Random.Range(-1 * MainScript.mapHeight / 2, MainScript.mapHeight / 2);
                xLoc += point.x * (MainScript.mapWidth + MainScript.placementWidthBuffer);
                yLoc += point.y * (MainScript.mapHeight + MainScript.placementHeightBuffer);
                GameObject newEnemy = MainScript.Instantiate(enemyAsset);
                Vector3 position = new Vector3(
                  xLoc,
                  yLoc,
                  0);

                newEnemy.transform.position = position;
            }
        }

        public void SetDoors(bool isOpen)
        {
            var open = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Artwork/Long Stone Grass Path.png");
            var closed = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Artwork/Long Grass Path.png");

            foreach (GameObject door in doors)
            {
                if (isOpen)
                {
                    //Long Grass Path
                    door.GetComponent<SpriteRenderer>().sprite = open;
                }
                else
                {
                    door.GetComponent<SpriteRenderer>().sprite = closed;

                }
            }
        }



        public Room GetRoomInt(int i)
        {
            switch (i)
            {
                case 0:
                    return GetRoomRight();
                case 1:
                    return GetRoomUp();
                case 2:
                    return GetRoomLeft();
                case 3:
                    return GetRoomDown();
                default:
                    throw new Exception("Room out of bounds");

            }

        }
        public Room GetRoomRight()
        {
            Point pt = MainScript.GetNeighborByInt(point, 0);
            return MainScript.map.ContainsKey(pt) ? MainScript.map[pt] : null;
        }
        public Room GetRoomUp()
        {
            Point pt = MainScript.GetNeighborByInt(point, 1);
            return MainScript.map.ContainsKey(pt) ? MainScript.map[pt] : null;
        }
        public Room GetRoomLeft()
        {
            Point pt = MainScript.GetNeighborByInt(point, 2);
            return MainScript.map.ContainsKey(pt) ? MainScript.map[pt] : null;
        }
        public Room GetRoomDown()
        {
            Point pt = MainScript.GetNeighborByInt(point, 3);
            return MainScript.map.ContainsKey(pt) ? MainScript.map[pt] : null;
        }
        public void SpawnNewRoom(int roomsToSpawn)
        {
            int dir = MainScript.r.Next(6);

            //give a higher chance to spawn in the same direction
            if (dir > 3)
            {
                dir = previousDirection;
            }

            if (GetRoomInt(dir) != null)
            {
                GetRoomInt(dir).SpawnNewRoom(roomsToSpawn);
            }
            else
            {
                //spawn the room on dir
                //0 is 2, 1 is 3, 2 is 0, 3 is 1
                Point pointOfNewRoom = MainScript.GetNeighborByInt(point, dir);
                MainScript.map[pointOfNewRoom] = new Room((previousDirection + 2) % 4, pointOfNewRoom, 1);
                if (roomsToSpawn > 1)
                {
                    GetRoomInt(dir).SpawnNewRoom(roomsToSpawn - 1);
                }
            }



        }
    }



}
