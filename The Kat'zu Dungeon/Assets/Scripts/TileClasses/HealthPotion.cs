﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
  public  class HealthPotion : Consumable
    {
        public int heal;
               
        public HealthPotion(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp) {
            name = "Health Potion";
            heal = 3;
            representedByID = 8;
        }
        public override void UseEffect(Character value) {
            value.hp+=heal;
            if(value is Hero)
            {
                Hero heroValue = (Hero)value;
                heroValue.maxHp += heal;
                float sliderHp = (float)heroValue.hp / (float)heroValue.maxHp;
                currentMap.SendUIInfo(2, (sliderHp.ToString()));
            }
            currentMap.SendLog("You used " + name + " and healed for 3hp!");
        }
    }
}
