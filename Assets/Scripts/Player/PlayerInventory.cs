using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HomoCuriositatis.Player{

    public class PlayerInventory : MonoBehaviour{ 
        public Dictionary<string, List<InventoryItem>> inventoryItems = new Dictionary<string, List<InventoryItem>>();

        public string[] KnownItensInInventory(){
            return inventoryItems.Keys.ToArray();
        }

        public int ItensInInventory(string itemID){
            List<InventoryItem> items = new List<InventoryItem>();
            inventoryItems.TryGetValue(itemID, out items);
            return items.Count;
        }

        public void AddInventoryItemToInventory(Item item, int quantity){
            List<InventoryItem> items = new List<InventoryItem>();
            if(!inventoryItems.TryGetValue(item.id, out items))
                inventoryItems.Add(item.id, items);
            for (int i = 0; i < quantity; i++){
                inventoryItems[item.id].Add(new InventoryItem(item));
            }
        }

        public bool InventoryHasItem(string itemID, int quantity = 1){
            return ItensInInventory(itemID) <= quantity;
        }

        //Retorna false caso inventario não possua itens o suficiente
        public bool RemoveItemFromInventory(string itemID, int quantity){
            if (!InventoryHasItem(itemID, quantity))
                return false;
            int indexToRemove = ItensInInventory(itemID) - (quantity + 1);
            OrderInventoryByDecay(itemID);
            inventoryItems[itemID].RemoveRange(indexToRemove, quantity);
            return true;
        }

        private void OrderInventoryByDecay(string itemID){
            List<InventoryItem> items;
            if (!inventoryItems.TryGetValue(itemID, out items))
                return;
            inventoryItems[itemID].Sort(
                (i1, i2) => i1.durability.CompareTo(i2.durability) 
            );
        }

        public void DecayInventory(){
            foreach (string itemID in inventoryItems.Keys){
                foreach (InventoryItem item in inventoryItems[itemID]){
                    item.Decay();
                }
                List<InventoryItem> items = inventoryItems[itemID];
                foreach (InventoryItem item in items){
                    if (item.HasDecayed())
                        inventoryItems[itemID].Remove(item);
                }
            }
        }

        public void DecayItem(string itemID, int decayValue){
            OrderInventoryByDecay(itemID);
            inventoryItems[itemID][0].Decay(decayValue);
            if(inventoryItems[itemID][0].HasDecayed())
                inventoryItems[itemID].RemoveAt(0);
        }
    }

}
