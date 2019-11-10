using UnityEngine;

namespace HomoCuriositatis.Base{

    [System.Serializable]
    public struct Effect{
        public EffectType effectType;
        [Range(0, 100)] public int chanceToFail;
        [Header("Preencher apenas caso o tipo seja Energia")]
        [Range(0,100)]
        public int energyRecharged;
        [Header("Preencher apenas caso o tipo seja Produção")]
        public int quantityValue;
        public Item resourceGenerated;
        public int decayValue;
        [Header("Preencher apenas caso o tipo seja Skill")]
        public Skill skill;
        public int skillExperience;
        [TextArea] public string successEffectText;
        [TextArea] public string failureEffectText;
    }

    [System.Serializable]
    public enum EffectType{
        recharge = 0,
        skill = 1,
        production = 2,
    }
}