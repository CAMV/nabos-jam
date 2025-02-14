﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPartyController : PartyController
{
    [SerializeField] 
    AICommandHandler _AIHandler;

    protected override void Update() {
        foreach (AIUnit u in _myUnits)
        {
            if (u.IsIdle)
            {
                _AIHandler.ExecuteAction(u);
            }
        }
    }

}
