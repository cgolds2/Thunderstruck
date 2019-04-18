using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public enum RoomType
    {
        Normal,
        Boss,
        Item
    }
    public class Room
    {
        public Point point;
        public RoomType roomType;
        //right is 0, top is 1, etc
        public readonly int previousDirection;
        public int numEnemies;
        public int difficulty;
        private List<GameObject> doors = new List<GameObject>();
        public int isKeyRoom = -1;
        public bool isRoomLocked = false;
        public List<GameObject> Doors
        {
            get
            {
                return doors;
            }
        }

        public GameObject mapIcon;
        public Renderer mapIconRenderer;
        public SpriteRenderer mapIconSpriteRenderer;
        public static float baseX = -8.434f;
        public static float baseY = -3.409f;
        public static float mult = 0.6f;

        public Room(int previousDirection, Point point, int difficulty)
        {

            this.previousDirection = previousDirection;
            this.point = point;
            this.difficulty = difficulty;
            CalculateEnemies();
            var iconAsset= Resources.Load<GameObject>("Sprites/RoomIcon");
            mapIcon = UnityEngine.Object.Instantiate(iconAsset);
            mapIcon.transform.position = new Vector3(
                (float) (baseX + point.x * mult), 
                (float) (baseY + point.y * mult), 
                -1);
            mapIconRenderer = mapIcon.GetComponent<Renderer>();
            mapIconSpriteRenderer = mapIcon.GetComponent<SpriteRenderer>();
            //mapIconSpriteRenderer.color = new Color(210f / 255f, 210f / 255f, 210f / 255f);
            mapIconSpriteRenderer.color = Color.grey;
            mapIconRenderer.enabled = false;
            mapIcon.transform.parent = HUDScript.MapAnchor.transform;

        }
        public void TurnOnNeighborIcons(){
            for (int i = 0; i < 4; i++)
            {
                var neighbor = GetRoomInt(i);
                if(neighbor!=null){
                    neighbor.mapIconRenderer.enabled = true;

                }

            }
        }
        public int GetDoorCount(){
            return doors.Count;
        }
        public void CalculateEnemies()
        {
          
            if (point.x == 0 && point.y == 0)
            {
                numEnemies = 0;
                return;
            }
            int minEnemies = 1;
            int maxEnemies = 1 + difficulty * 2;
            numEnemies = MainScript.r.Next(minEnemies, maxEnemies);

        }
        public void AddDoor(GameObject door)
        {
            doors.Add(door);
        }
        public GameObject GetBossPrefab()
        {
            switch (HUDScript.GetLevel())
            {
                case 1:
                    return Resources.Load<GameObject>("Sprites/EnemyCloudBoss");
                case 2:
                    return Resources.Load<GameObject>("Sprites/BossHail");
                case 3:
                    return Resources.Load<GameObject>("Sprites/TornadoBoss");
                default:
                    throw new Exception("Shouldnt hit this level");
            }
        }
        public void SpawnItem()
        {
            var  xLoc = point.x * (MainScript.mapWidth + MainScript.placementWidthBuffer);
            var yLoc = point.y * (MainScript.mapHeight + MainScript.placementHeightBuffer);
            var itemToSpawn = ItemsManager.GetRandomItem(MainScript.r);
            GameObject obToSpawn = null;
            switch (itemToSpawn)
            {
                case Application.Items.blueCoat:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_umberella_blue");
                    break;
                case Application.Items.redCoat:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_umberella_blue");
                    break;
                case Application.Items.redUmbrella:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_umberella_red");
                    break;
                case Application.Items.blueUmbrella:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_umberella_blue");
                    break;
                case Application.Items.hat:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_umberella_blue");
                    break;
                case Application.Items.boots:
                    obToSpawn = Resources.Load<GameObject>("Sprites/item_boots");
                    break;
            }
            var x  = UnityEngine.Object.Instantiate(obToSpawn);
            obToSpawn.transform.position = new Vector3(xLoc, yLoc, -1);
        }

        public
        void SpawnEnemies()
        {
            GameObject enemyAsset;
            float rateOfFire = 1.5f;
            switch (roomType)
            {
                case RoomType.Boss:

                    enemyAsset = GetBossPrefab();
                    break;
                case RoomType.Normal:
                    enemyAsset = Resources.Load<GameObject>("Sprites/EnemyCloudLevel1");
                    break;
                
                default:
                    throw new Exception("What type of room is this...");
            }

            if(roomType==RoomType.Boss){
                rateOfFire = 3f;
            }
            for (int i = 0; i < numEnemies; i++)
            {
               
                float xLoc = UnityEngine.Random.Range(-1 * MainScript.mapWidth / 2, MainScript.mapWidth / 2);
                float yLoc = UnityEngine.Random.Range(-1 * MainScript.mapHeight / 2, MainScript.mapHeight / 2);
                if(numEnemies==1){
                    xLoc = 0;
                    yLoc = 0;
                }
                xLoc += point.x * (MainScript.mapWidth + MainScript.placementWidthBuffer);
                yLoc += point.y * (MainScript.mapHeight + MainScript.placementHeightBuffer);
                GameObject newEnemy = UnityEngine.Object.Instantiate(enemyAsset);
                newEnemy.GetComponent<EnemyScript>().RateOfFire = rateOfFire;
                newEnemy.tag = "Enemy";
                Vector3 position = new Vector3(
                  xLoc,
                  yLoc,
                  0);
                if (i == isKeyRoom)
                {
                    newEnemy.GetComponent<EnemyScript>().isKeyEnemy = true;
                }
                newEnemy.transform.position = position;
            }
        }
        Sprite open = Resources.Load<Sprite>("Artwork/Long Stone Grass Path");
        Sprite closed = Resources.Load<Sprite>("Artwork/Long Grass Path");
        public void SetDoors(bool isOpen)
        {
            Sprite opend;
            Sprite closedd;
          
            foreach (GameObject door in doors)
            {
                var targetRoom = GetRoomInt(door.GetComponent<DoorScript>().Direction);
                var RoomType =targetRoom.roomType;
                if ( RoomType == RoomType.Boss)
                {
                    opend = Resources.Load<Sprite>("Artwork/Long Stone Grass Path Boss");
                    closedd = Resources.Load<Sprite>("Artwork/Long Grass Path Boss");
                }else if(RoomType == RoomType.Item && targetRoom.isRoomLocked)
                {
                    opend = open;
                    closedd = Resources.Load<Sprite>("Artwork/Long Grass Path Lock");
                }
                else
                {
                    opend = open;
                    closedd =closed;
                }
                if (isOpen && !(RoomType == RoomType.Item && targetRoom.isRoomLocked))
                {
                    //Long stone Grass Path
                    door.GetComponent<SpriteRenderer>().sprite = opend;
                    
                }
                else
                {
                    door.GetComponent<SpriteRenderer>().sprite = closedd;

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
