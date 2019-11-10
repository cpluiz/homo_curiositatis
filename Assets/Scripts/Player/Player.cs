using System.Collections;
using System.Collections.Generic;
using HomoCuriositatis.Base;
using UnityEngine;

namespace HomoCuriositatis.Player{

    public class Player : MonoBehaviour{
        
        public Dictionary<string, KnownItem> knownItems = new Dictionary<string, KnownItem>();
        public Dictionary<string, KnownSkill> knownSkills = new Dictionary<string, KnownSkill>();

        private PlayerInventory inventory;
        private int playerEnergy;
        private string _messagePool = "";
        public string messagePool{
            get{ return _messagePool; }
        }

        public void ClearMessagePool(){
            _messagePool = "";
        }

        private void addTextToMessagePool(string text){
            if(text != "")
                _messagePool += "\n\n"+text;
        }

        public string[] GetInventoryNames(){
            return inventory.KnownItensInInventory();
        }

        public int InventoryCount(string itemID){
            return inventory.ItensInInventory(itemID);
        }
        
        public string[] GetInventoryNamesWithCount(){
            string[] inventoryItems = GetInventoryNames();
            for (int i=0;i<inventoryItems.Length;i++){
                inventoryItems[i] += " - "+InventoryCount(inventoryItems[i]);
            }
            return inventoryItems;
        }

        public void UpdateItemLevel(string itemID, int xpToUpdate){
            //Não é possível upar nível de item que não possui
            if (!knownItems.ContainsKey(itemID))
                return;
            knownItems[itemID].UpdateXP(xpToUpdate);
        }

        public void UpdateSkill(Skill skill, int skillExperience = 0){
            //Se ainda não possui a skill, adiciona ela na lista
            if(!knownSkills.ContainsKey(skill.id))
                knownSkills.Add(skill.id, new KnownSkill(skill));
            knownSkills[skill.id].UpdateXP(skillExperience);
        }

        public void ReceiveItem(Item item, int quantity = 1){
            if(!knownItems.ContainsKey(item.id))
                knownItems.Add(item.id, new KnownItem(item));
            inventory.AddInventoryItemToInventory(item, quantity);
        }

        public void ConsumeItem(string itemID, int quantity = 1){
            if (!inventory.RemoveItemFromInventory(itemID, quantity)){
                Debug.LogWarning("Tentativa de remover "+quantity+" "+itemID+" falhou!");
            }
        }

        public bool HasRequirements(Requirement[] requirements){
            foreach (Requirement requirement in requirements){
                switch (requirement.type){
                    case RequirementType.energy:
                        if (requirement.energyNeeded > playerEnergy)
                            return false;
                        break;
                    case RequirementType.item:
                        if (!inventory.InventoryHasItem(requirement.item.id, requirement.quantity))
                            return false;
                        break;
                    case RequirementType.skill:
                        if (!knownSkills.ContainsKey(requirement.skill.id))
                            return false;
                        if (requirement.skillMinLvl > 0 &&
                            knownSkills[requirement.skill.id].skillLevel < requirement.skillMinLvl)
                            return false;
                        break;
                }
            }
            return true;
        }

        public void ConsumeRequirements(Requirement[] requirements){
            foreach (Requirement requirement in requirements){
                switch (requirement.type){
                    case RequirementType.energy:
                        if (requirement.energyNeeded > playerEnergy)
                            playerEnergy -= requirement.energyNeeded;
                        break;
                    case RequirementType.item:
                        if(requirement.consumeItem)
                            inventory.RemoveItemFromInventory(requirement.item.id, requirement.quantity);
                        if (requirement.decayItem)
                            inventory.DecayItem(requirement.item.id, requirement.decayValue);
                        break;
                }
            }
        }

        private bool Interact(Interaction interaction){
            if (HasRequirements(interaction.requirements)){
                ConsumeRequirements(interaction.requirements);
                foreach (Effect effect in interaction.effects){
                    int errorThreshold = Random.Range(0, 101);
                    if (errorThreshold <= effect.chanceToFail){
                        addTextToMessagePool(effect.failureEffectText);
                        return false;
                    }
                    addTextToMessagePool(effect.successEffectText);
                    switch (effect.effectType){
                        case EffectType.production:
                            ReceiveItem(effect.resourceGenerated, effect.quantityValue);                                
                            break;
                        case EffectType.recharge:
                            playerEnergy += effect.energyRecharged;
                            break;
                        case EffectType.skill:
                            UpdateSkill(effect.skill, effect.skillExperience);
                            break;
                    }
                }
                return true;
            }
            return false;
        }

        public void InteractWithItem(string itemID){
            //Se não há itens desse tipo no inventário, bloqueia a interação
            if (inventory.ItensInInventory(itemID) <= 0)
                return;
            KnownItem item = knownItems[itemID];
            Interaction interaction = item.Interact();
            if (Interact(interaction)){
                if(interaction.singleInteraction)
                    item.RemoveInteraction(interaction);
            }

        }
    }

}
