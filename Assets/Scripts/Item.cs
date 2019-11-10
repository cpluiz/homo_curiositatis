using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomoCuriositatis.Base;

namespace HomoCuriositatis{

    [CreateAssetMenu(fileName = "NewItem", menuName = "HomoCuriositatis/Novo Item", order = 1)]
    public class Item : ScriptableObject{
        public string id;
        public string name;
        public Sprite image;
        public Description[] descriptions;
        public int durability;
        public int durabilityAutoDecay;
        public Interaction[] interactions;
    }

}

