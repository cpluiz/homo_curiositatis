using UnityEngine;

namespace HomoCuriositatis.Base{

    [System.Serializable]
    public struct Effect{
        public EffectType effectType;
        [Range(0, 100)] public int chanceToFail;
        [Header("Preencher apenas caso o tipo seja Energia")]
        [Range(0,100)]
        public int energyRecharged;
        [Header("Preencher apenas caso o tipo seja Produção ou ganho de conhecimento")]
        [Range(0,100)]
        public int quantityValue;
        public Item resourceGenerated;
        [Header("Preencher apenas caso o tipo seja Skill")]
        public Skill skill;
        [Range(0,100)]
        public int skillExperience;
        [TextArea] public string successEffectText;
        [TextArea] public string failureEffectText;
    }

    [System.Serializable]
    public enum EffectType{
        recharge = 0,
        skill = 1,
        itemKnowledge = 3,
        production = 2,
    }
}