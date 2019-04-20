using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class MainScript : MonoBehaviour
{
    static int seed;
    public static Dictionary<Point, Room> map;
    public static Random r;
    public static Random otherR;
    public static float mapWidth;
    public static float mapHeight;
    public static float mapBorderWidth;
    public static float mapBorderHeight;
    public static float placementWidthBuffer;
    public static float placementHeightBuffer;
    public static Room currentRoom;
    public static GameObject mainCamera;
    public static float currentRoomX;
    public static bool gameOver;
    public static float currentRoomY;
    public static HUDScript HUD;
  

    // Start is called before the first frame update
  public static void CalcSeed(int? _seed)
    {
        MainScript.gameOver = false;
        HUDScript.SetLevel(1);
        CharacterScript.SetAllItemsFalse();
        if (_seed == null)
        {
            seed = (int)System.DateTime.Now.Ticks;

        }
        else
        {
            seed = (int)_seed;
        }
        r = new Random(seed);
        Debug.Log("Seed is: " + seed);
        CharacterScript.health = 8;

    }
    void Awake()
    {
        HUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDScript>();
        if (r == null)
        {

            CalcSeed(null);
        }
        HUD.ResetTimer();
        CharacterScript.ResetItems();
        mainCamera = GameObject.Find("MainCamera");
        currentRoom = null;
        GameObject mapPic = GameObject.Find("templateRoom");
        Vector3 mapRend = mapPic.GetComponent<Renderer>().bounds.size;
        mapWidth = mapRend.x;
        mapHeight = mapRend.y;
        //mapBorderWidth = (float)1.54;
        //mapBorderHeight = (float)1.5;
        mapBorderWidth = (float)0;
        mapBorderHeight = (float)0;
        placementWidthBuffer = 10;
        placementHeightBuffer = 10;
        map =   new Dictionary<Point, Room>();

        var assetDoor = Resources.Load<GameObject>("Sprites/door");
        var assetRoom = Resources.Load<GameObject>("Sprites/room");


        otherR = new Random();
        int maxRooms = 10;
        int minRooms = 6;
        int numRooms = r.Next(minRooms, maxRooms);

        map[new Point(0, 0)] = new Room(0, new Point(0, 0), 1);
        map[new Point(0, 0)].SpawnNewRoom(numRooms);

        var keyRooms = RandomValues(map);
        //var keyRoom = RandomValues(map).Take(1).First();
        var keyRoom = keyRooms.First(item => item.numEnemies > 0);
     

        keyRoom.isKeyRoom = r.Next(keyRoom.numEnemies);
        MakeBossRoom();
        MakeItemRoom();
        SetRoom(GetRoomFromCoord(0, 0));
        currentRoom.SetDoors(true);
        HUDScript.SetKey(false);
        int xMin = 0;
        int yMin = 0;
        int xMax = 0;
        int yMax = 0;

        GameObject finalRoom = Instantiate(assetRoom);

        foreach (KeyValuePair<Point, Room> entry in map)
        {
            int x = entry.Key.x;
            int y = entry.Key.y;
            if (x < xMin) { xMin = x; };
            if (y < yMin) { yMin = y; };
            if (x > xMax) { xMax = x; };
            if (y > yMax) { yMax = y; };
            string test = string.Format("Point at {0},{1}", x, y);
            //Debug.Log(test);
            GameObject newBox = Instantiate(assetRoom);
            // GameObject newBox = Instantiate(mapPic);
            newBox.name = "madeBox";
            float placementX = x * (mapWidth + placementWidthBuffer);
            float placementY = y * (mapHeight + placementHeightBuffer);
            float placementZ = mapRend.z;

            newBox.transform.position = new Vector3(placementX, placementY, placementZ);
            placementZ--;
            Vector3 doorRend = assetDoor.GetComponent<Renderer>().bounds.size;
            for (int i = 0; i < 4; i++)
            {
                if (map[entry.Key].GetRoomInt(i) != null)
                {
                    GameObject newDoor = Instantiate(assetDoor);
                    newDoor.GetComponent<DoorScript>().Direction = i;
                    newDoor.name = "madeDoor";

                    switch (i)
                    {

                        case 0:
                            newDoor.transform.Rotate(Vector3.forward * 0);

                            newDoor.transform.position = new Vector3(placementX + mapWidth / 2 - (MainScript.mapBorderWidth / 2 - doorRend.x / 2),
                                                                  placementY,
                                                                  placementZ);
                            break;
                        case 1:
                            newDoor.transform.Rotate(Vector3.forward * 90);
                            newDoor.transform.position = new Vector3(placementX,
                                                              placementY + mapHeight / 2 - (MainScript.mapBorderHeight / 2 - doorRend.x / 2),
                                                              placementZ);
                            break;
                        case 2:
                            newDoor.transform.Rotate(Vector3.forward * 180);

                            newDoor.transform.position = new Vector3(placementX - mapWidth / 2 + (MainScript.mapBorderWidth / 2 - doorRend.x / 2),
                                                              placementY,
                                                              placementZ);
                            break;
                        case 3:
                            newDoor.transform.Rotate(Vector3.forward * -90);

                            newDoor.transform.position = new Vector3(placementX,
                                                              placementY - mapHeight / 2 + (MainScript.mapBorderHeight / 2 - doorRend.x / 2),
                                                              placementZ);
                            break;
                        default:
                            throw new Exception("Something went wrong");
                    }
                    entry.Value.AddDoor(newDoor);
                }

            }
            entry.Value.SetDoors(true);



        }

        GameObject.Find("templateRoom").SetActive(false);
        GameObject.Find("templateDoor").SetActive(false);

        string output = "\n";

        for (int y = yMin; y <= yMax; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                if (x == 0 && y == 0)
                {
                    output += "2 ";
                }
                else
                {
                    if (map.ContainsKey(new Point(x, y)))
                    {
                        output += "1 ";
                    }
                    else
                    {
                        output += "0 ";

                    }
                }

            }
            output += "\n";
        }
        Debug.Log(output);




    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        
        List<TValue> values = Enumerable.ToList(dict.Values);
        int size = dict.Count;
        while (true)
        {
            yield return values[r.Next(size)];
        }
    }
    private void Start()
    {
    }

    void MakeItemRoom()
    {
        var assetRoom = Resources.Load<GameObject>("Sprites/room");

        foreach (int j in Enumerable.Range(0, map.Count).OrderBy(x => r.Next()))
        {
            var entry = map.ElementAt(j);
            var itemKey = entry.Key;
            var itemValue = entry.Value;
            Console.WriteLine(j);


            //if not all doors are filled, and this isnt the boss room
            if (entry.Value.GetDoorCount() != 4 && map[entry.Key].roomType != RoomType.Boss)
            {
                for (int i = 0; i < 4; i++)
                {
                    Point potential = GetNeighborByInt(entry.Key, i);
                    if (!(map.ContainsKey(potential)))
                    {
                        map[potential] = new Room(i, potential, 0)
                        {
                            roomType = RoomType.Item,
                            isRoomLocked = true,
                            numEnemies = 0
                        };
                        map[potential].SpawnItem(potential);
                        return;
                    }
                }

            }
        }
        
  
    
    }
    void MakeBossRoom(){
        var assetRoom = Resources.Load<GameObject>("Sprites/room");
  
        int x = 0;
        int y = 0;
        double longestPath = 0;
        foreach (KeyValuePair<Point, Room> entry in map)
        {
            if(entry.Value.GetDoorCount()==4){
                continue;
            }
            double diff = distance(0, 0, entry.Key.x, entry.Key.y);
            if(diff>longestPath){
                x = entry.Key.x;
                y = entry.Key.y;
                longestPath = diff;
            }
        }
        Room previous = map[new Point(x, y)];
        Point spawnAt = new Point(0,0);
        int dirToSpawn = -1;
        for (int i = 0; i < 4; i++)
        {
           var next =  GetNeighborByInt(previous.point, i);
            var dist = distance(
                0,0,
                next.x, next.y
            );
            if(dist>longestPath){
                longestPath = dist;
                spawnAt = next;
                dirToSpawn = i;
            }
        }
        map[spawnAt] = new Room((dirToSpawn + 2) % 4, spawnAt, 0)
        {
            roomType = RoomType.Boss,
            numEnemies = 1
        };
    }
    static double distance(int x1, int y1, int x2, int y2)
    {
        // Calculating distance 
        return Math.Sqrt(Math.Pow(x2 - x1, 2) +
                      Math.Pow(y2 - y1, 2) * 1.0);
    }


    public static Point GetNeighborByInt(Point point, int i)
    {
        switch (i)
        {
            case 0:
                return new Point(point.x + 1, point.y);
            case 1:
                return new Point(point.x, point.y + 1);
            case 2:
                return new Point(point.x - 1, point.y);
            case 3:
                return new Point(point.x, point.y - 1);
            default:
                throw new Exception("Room out of bounds");

        }
    }

    public static Room GetRoomFromCoord(int x, int y)
    {
        Point point = new Point(x, y);
        return MainScript.map.ContainsKey(point) ? MainScript.map[point] : null;

    }

    public static void SetRoom(Room room)
    {
        if(currentRoom!=null){
            currentRoom.mapIconSpriteRenderer.color = Color.black;

        }
        currentRoom = room;
        currentRoom.mapIconRenderer.enabled = true;
        currentRoom.mapIconSpriteRenderer.color = Color.red;
        currentRoom.TurnOnNeighborIcons();


        if (currentRoom.numEnemies > 0)
        {
            currentRoom.SetDoors(false);
            currentRoom.SpawnEnemies();
        }
        var xBound = (MainScript.mapWidth / 2) - MainScript.mapBorderWidth;

        var yBound = (MainScript.mapHeight / 2) - MainScript.mapBorderHeight;


        BaseSprite.SetBounds(xBound, yBound);

        float placementX = room.point.x * (mapWidth + placementWidthBuffer);
        float placementY = room.point.y * (mapHeight + placementHeightBuffer);
        MainScript.currentRoomX = placementX;
        MainScript.currentRoomY = placementY;

        mainCamera.transform.position = new Vector3(
            placementX - (float)1.22,
            placementY,
            mainCamera.transform.position.z);

          //HUDScript.MapAnchor.transform.position = new Vector3(
          //   (Room.baseX + room.point.x * Room.mult),
          //(Room.baseY + room.point.y * Room.mult),
                          //-1);
        HUDScript.MapAnchor.transform.localPosition = new Vector3(
         (-0.215f * room.point.x) +HUDScript.mapStartingPos.x ,
        (-0.215f * room.point.y) +HUDScript.mapStartingPos.y,
                      -1);

        HUDScript.MoveObjects(new Vector3(placementX, placementY, 0));



        var player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(
            placementX,
            placementY,
            player.transform.position.z);

        List<GameObject> killEmAll = new List<GameObject>();
        killEmAll.AddRange(GameObject.FindGameObjectsWithTag("PlayerBullet"));
        killEmAll.AddRange(GameObject.FindGameObjectsWithTag("EnemyBullet"));

        for (int i = 0; i < killEmAll.Count; i++)
        {
            if (null != killEmAll[i])
            {
                Destroy(killEmAll[i].gameObject);
            }
        }

  
    }
    public static void DecreaseEnemyCount()
    {
        currentRoom.numEnemies--;
        if (currentRoom.numEnemies <1)
        {
            currentRoom.SetDoors(true);
        }
    }
}

public struct Point
{
    public int x;
    public int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}
