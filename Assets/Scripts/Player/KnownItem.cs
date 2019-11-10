using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HomoCuriositatis;
using HomoCuriositatis.Base;

namespace HomoCuriositatis.Player{
    public struct KnownItem{
        private Item _item;
        private int _itemLevel;
        private int _currentXp;
        private Dictionary<int, List<Interaction>> _interactions;
        
        public Item item{ get{ return _item; } }
        public int itemLevel{ get{ return _itemLevel; } }
        public int currentXp{ get{ return _currentXp; } }

        public KnownItem(Item i){
            _item = i;
            _itemLevel = 0;
            _currentXp = 0;
            _interactions = new Dictionary<int, List<Interaction>>();
            foreach (Interaction interaction in i.interactions){
                if(!_interactions.ContainsKey(interaction.minLevel))
                    _interactions.Add(interaction.minLevel, new List<Interaction>());
                _interactions[interaction.minLevel].Add(interaction);
            }
        }

        public bool UpdateXP(int experience){
            _currentXp += experience;
            if (_currentXp > 100){
                _itemLevel++;
                _currentXp -= 100;
                return true;
            }
            return false;
        }

        public Interaction Interact(){
            int interactionLevel = Random.Range(0, _itemLevel+1);
            int interactionID = Random.Range(0, _interactions[interactionLevel].Count);
            Interaction interaction = _interactions[interactionLevel][interactionID];
            while (interaction.maxLvlToShowInInteraction < _itemLevel){
                interactionLevel = Random.Range(0, _itemLevel+1);
                interactionID = Random.Range(0, _interactions[interactionLevel].Count);
                interaction = _interactions[interactionLevel][interactionID];
            }
            return interaction;
        }

        public void RemoveInteraction(Interaction interaction){
            _interactions[interaction.minLevel].Remove(interaction);
        }
        
    }
}
