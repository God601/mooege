﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mooege.Core.GS.Powers;
using Mooege.Core.GS.AI.Brains;
using Mooege.Net.GS.Message;
using Mooege.Core.GS.Ticker;
using Mooege.Core.GS.Players;
using Mooege.Net.GS.Message.Definitions.Pet;

namespace Mooege.Core.GS.Actors.Implementations.Minions
{
    class CompanionMinion : Minion
    {
        //Changes creature with each rune,
        //RuneSelect(133741, 173827, 181748, 159098, 159102, 159144)

        public CompanionMinion(Map.World world, PowerContext context, int CompanionID)
            : base(world, 133741, context.User, null)
        {
            Scale = 1.2f; //they look cooler bigger :)
            //TODO: get a proper value for this.
            this.WalkSpeed *= 5;
            SetBrain(new MinionBrain(this));
            (Brain as MinionBrain).AddPresetPower(169081); //melee_instant
            (Brain as MinionBrain).AddPresetPower(133887); //ChargeAttack
            //TODO: These values should most likely scale, but we don't know how yet, so just temporary values.
            Attributes[GameAttribute.Hitpoints_Max_Total] = 20f;
            Attributes[GameAttribute.Hitpoints_Max] = 20f;
            Attributes[GameAttribute.Hitpoints_Total_From_Level] = 0f;
            Attributes[GameAttribute.Hitpoints_Cur] = 20f;
            Attributes[GameAttribute.Attacks_Per_Second_Total] = 1.0f;

            Attributes[GameAttribute.Damage_Weapon_Min_Total, 0] = context.ScriptFormula(0) * context.User.Attributes[GameAttribute.Damage_Weapon_Min_Total, 0];
            //Attributes[GameAttribute.Damage_Weapon_Delta_Total, 0] = context.ScriptFormula(13) * context.User.Attributes[GameAttribute.Damage_Weapon_Delta_Total, 0];

            Attributes[GameAttribute.Pet_Type] = 0x8;
            //Pet_Owner and Pet_Creator seems to be 0
            (context.User as Player).InGameClient.SendMessage(new PetMessage()
            {
                Field0 = 0,
                Field1 = CompanionID,
                PetId = this.DynamicID,
                Field3 = 0x8,
            });
        }
    }
}
