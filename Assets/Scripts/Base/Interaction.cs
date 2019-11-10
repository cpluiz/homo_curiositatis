using UnityEngine;

namespace HomoCuriositatis.Base{

    [System.Serializable]
    public struct Interaction{
        public int minLevel;
        [Range(0, 12)] public int timeConsumption;
        public bool singleInteraction;
        [Range(0, 100)]
        public int bonusXP;
        public int maxLvlToGainXP;
        public int maxLvlToShowInInteraction;
        [TextArea] public string successDescription;
        [TextArea] public string failureDescription;
        public Effect[] effects;
        public Requirement[] requirements;
    }

    [System.Serializable]
    public enum InteractionType{
        item = 0,
        exploration = 1
    }
}
