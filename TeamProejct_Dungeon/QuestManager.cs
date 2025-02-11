﻿using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    enum QuestType
    {
        kill,
        find,
        talk
    }

    class Reward
    {
        public int gold;
        public int exp;
        public List<IItem> reward_items = new List<IItem>();
        public string description;

        public Reward(int g,int e, params IItem[] items)
        {
            gold = g;
            exp = e;
            foreach(IItem item in items)
            {
                reward_items.Add(item);
            }
            if(gold != 0)
            {
                description += $"골드 : {gold} ";
            }
            if(exp != 0)
            {
                description += $"경험치 : {exp}";
            }
            if(reward_items.Count > 0)
            {
                description += "장비 : ";
                foreach(IItem item in reward_items)
                {
                    description += item.name + " ";
                }
            }
        }

        public void RewardGet()
        {
            
        }
    }
    class Quest
    {
        private string title;
        private string description;
        private string target;
        private QuestType qt;
        private int TargetClearCount;
        private int CurClearCount;

        bool isCleared;

        public Quest(string title, string des, QuestType type, ICharacter target, int TargetCount = 1)
        {
            this.title = title;
            this.description = des;
            this.qt = type;
            this.target = target.Name;
            this.TargetClearCount = TargetCount;
            CurClearCount = 0;
            isCleared = false;
        }

        public void PlusCount()
        {
            CurClearCount++;
            Renew();
        }
        public void Renew()
        {
            if( CurClearCount == TargetClearCount )
            {
                isCleared = true;
            }
        }
    }
    public static class QuestManager
    {

    }
}
