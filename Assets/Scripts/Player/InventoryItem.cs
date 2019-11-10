using UnityEngine;
using HomoCuriositatis;

namespace HomoCuriositatis.Player{
    public struct InventoryItem{
        private string _itemID;
        private int _durability;
        private int _durabilityAutoDecay;
        
        public string itemID { get { return _itemID; } }
        public int durability { get { return _durability; } }
        public int durabilityAutoDecay { get { return _durabilityAutoDecay; } }

        public InventoryItem(Item item){
            _itemID = item.id;
            _durability = item.durability;
            _durabilityAutoDecay = item.durabilityAutoDecay;
        }

        public void Decay(){
            _durability -= durabilityAutoDecay;
        }

        public void Decay(int decayValue){
            _durability -= decayValue;
        }

        public bool HasDecayed(){
            return _durability <= 0;
        }
    }
}
