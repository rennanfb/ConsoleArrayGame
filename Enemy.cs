using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArrayGame
{
    internal class Enemy
    {
        public int enemyIndex;
        public int enemyListIndex;

        public int enemyIndexInListOfEnemies;

        public DateTime? enemyMovementCooldown = null;

        public bool active = true;

        public Enemy(int index, int listIndex)
        {
            enemyIndex = index;
            enemyListIndex = listIndex;
        }

        ~Enemy()
        {

        }
    }
}
