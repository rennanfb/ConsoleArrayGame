using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArrayGame
{
    internal class PlayerShield
    {
        public static int collectableCharListsIndex;
        public static int collectableCharListIndex;
        public static char shieldCollectableChar = 'Ʊ';

        public static char centralChar = '═';
        public static char rightChar = '╝';
        public static char leftChar = '╚';

        public static bool shieldActive = false;


        public static void ResetCollectableIndexes()
        {
            collectableCharListsIndex = 0;
            collectableCharListIndex = 0;
        }
    }
}
