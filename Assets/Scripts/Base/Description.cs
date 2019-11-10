using UnityEngine;
namespace HomoCuriositatis.Base{

    [System.Serializable]
    public struct Description{
        public int lvl;
        [TextArea]
        public string text;
    }

}