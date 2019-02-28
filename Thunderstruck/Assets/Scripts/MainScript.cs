using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class MainScript : MonoBehaviour
{
    static int seed;
    public static Dictionary<Point,Room> map = new Dictionary<Point, Room>();
    public static Random r;
    public static float mapWidth;
    public static float mapHeight;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mapPic = GameObject.Find("wholeMap");
        Vector3 mapRend = mapPic.GetComponent<Renderer>().bounds.size;
        mapWidth = mapRend.x;
        mapHeight = mapRend.y;
        seed = (int)System.DateTime.Now.Ticks;
        r = new Random(seed);
        map[new Point(0,0)] = new Room(0, new Point(0,0));
        map[new Point(0, 0)].SpawnNewRoom(10);
        int xMin=0;
        int yMin=0;
        int xMax=0;
        int yMax=0;

        foreach (KeyValuePair<Point, Room> entry in map)
        {
            int x = entry.Key.x;
            int y = entry.Key.y;
            if (x < xMin) { xMin = x; };
            if (y < yMin) { yMin = y; };
            if (x > xMax) { xMax = x; };
            if (y > yMax) { yMax = y; };
            string test = string.Format("Point at {0},{1}", x, y);
            Debug.Log(test);

            GameObject newBox = Instantiate(mapPic);
            newBox.transform.position = new Vector3(x*mapWidth, y*mapHeight + transform.localScale.y / 2, mapRend.z);
        }
        GameObject.Find("wholeMap").SetActive(false);
        
        string output = "\n";

        for (int y = yMin; y <= yMax; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                if(x==0 && y==0){
                    output += "2 ";
                }else{
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



}

public class Room
{
    public Point point;
    //right is 0, top is 1, etc
    private readonly int previousDirection;
    public Room(int previousDirection, Point point)
    {
        this.previousDirection = previousDirection;
        this.point = point;
    }



    public Room GetRoomInt(int i){
        switch(i){
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
        if(dir>3){
            dir = previousDirection;
        }

        if(GetRoomInt(dir)!=null){
            GetRoomInt(dir).SpawnNewRoom(roomsToSpawn);
        }
        else
        {
            //spawn the room on dir
            //0 is 2, 1 is 3, 2 is 0, 3 is 1
            Point pointOfNewRoom = MainScript.GetNeighborByInt(point, dir);
            MainScript.map[pointOfNewRoom] = new Room((previousDirection + 2) % 4, pointOfNewRoom);
            if (roomsToSpawn > 1){
                GetRoomInt(dir).SpawnNewRoom(roomsToSpawn - 1);
            }
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
