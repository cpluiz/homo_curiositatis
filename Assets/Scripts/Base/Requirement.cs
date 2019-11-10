using UnityEngine;

namespace HomoCuriositatis.Base{

    [System.Serializable]
    public struct Requirement{
        public RequirementType type;
        [Header("Não preencher caso seja energia")]
        public int requirementMinLvl;
        
        [Header("Preencher apenas caso tipo = Item")]
        public Item item;
        [Range(0,100)]
        public int quantity;
        public bool consumeItem;
        public bool decayItem;
        [Range(0,100)]
        public int decayValue;

        [Header("Preencher apepnas caso tipo = Habilidade")]
        public Skill skill;
        public int skillMinLvl;

        [Header("Preencher apenas caso tipo = Energia")]
        [Range(0,100)]
        public int energyNeeded;
        
        [Header("Preencher apenas caso tipo = Tempo")]
        [Range(0, 12)] public int timeConsumption;
    }

    [System.Serializable]
    public enum RequirementType{
        item = 0,
        skill = 1,
        energy = 2,
        time = 3,
    }
}