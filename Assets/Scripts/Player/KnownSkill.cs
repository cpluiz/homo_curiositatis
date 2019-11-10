using UnityEngine;
using HomoCuriositatis;

namespace HomoCuriositatis.Player{
    public struct KnownSkill{
        private Skill _skill;
        private int _skillLevel;
        private int _currentXp;
        
        public Skill skill{ get{ return _skill; } }
        public int skillLevel{ get{ return _skillLevel; } }
        public int currentXp{ get{ return _currentXp; } }

        public KnownSkill(Skill s){
            _skill = s;
            _skillLevel = 0;
            _currentXp = 0;
        }

        public bool UpdateXP(int experience){
            _currentXp += experience;
            if (_currentXp > 100){
                _skillLevel++;
                _currentXp -= 100;
                return true;
            }
            return false;
        }
        
    }
}
