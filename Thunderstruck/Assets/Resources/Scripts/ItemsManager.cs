using System;
    public class ItemsManager
    {
        //have these appeared
        public static int numItems = 6;
        public static bool[] haveItemsAppeared = new bool[numItems];
        public static Items GetRandomItem(Random r){
            while(true){
                int x = r.Next(0, numItems);
                if(!haveItemsAppeared[x]){
                    return (Items)x;
                }
            }
        }
    }
    public enum Items{
        blueCoat, 
        redCoat, 
        redUmbrella, 
        blueUmbrella, 
        hat, 
        boots
    }

