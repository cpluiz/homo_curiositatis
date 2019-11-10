using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomoCuriositatis.Base;

namespace HomoCuriositatis{

    [CreateAssetMenu(fileName = "NewSkill", menuName = "HomoCuriositatis/Nova Habilidade", order = 2)]
    public class Skill : ScriptableObject{
        public string id;
        public string name;
        public Description[] descriptions;
        public SkillType type;
        public Requirement[] requirements;
        public Effect[] effects;
    }

}

