﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomoCuriositatis.Base;

namespace HomoCuriositatis{

    [CreateAssetMenu(fileName = "NewExploration", menuName = "HomoCuriositatis/Cadastrar Exploração", order = 3)]
    public class Exploration : ScriptableObject{
        [TextArea] public string explorationText;
        public bool singleExploration;
        public Interaction[] interactions;
    }

}

