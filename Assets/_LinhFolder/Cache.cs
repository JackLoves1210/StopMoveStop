//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class Cache
//{
//    private static Dictionary<Collider, IHit> ihits = new Dictionary<Collider, IHit>();

//    public static IHit GetIHit(Collider collider)
//    {
//        if (!ihits.ContainsKey(collider))
//        {
//            ihits.Add(collider, collider.GetComponent<IHit>());
//        }

//        return ihits[collider];
//    }

//    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

//    public static Character GetCharacter(Collider collider)
//    {
//        if (!characters.ContainsKey(collider))
//        {
//            characters.Add(collider, collider.GetComponent<Character>());
//        }

//        return characters[collider];
//    }


//}
