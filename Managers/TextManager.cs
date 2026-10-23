
namespace ConsoleArrayGame.Managers
{
    internal class TextManager
    {
        public List<char[]>? gameCharsLists { get; set; } = new List<char[]>();

        public char gameBaseTextChar = '█';
        public char enemyChar = '▯';
        public char playerChar = '•';
        public char explosionChar = '*';

        public int gameCharsListsSize = 26;
        public int gameCharsListSize = 61;

        public string baseText = string.Empty;

        public void SkipFrame()
        {
            Console.Clear();
        }

        public void ShowPoints()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"                          Score: {GameManager.GamePoints}");
            Console.ResetColor();
        }

        public void ShowTable()
        {
            ShowPoints();

            for (int i = 0; i < gameCharsListsSize; i++)
            {
                Console.WriteLine(gameCharsLists[i]);
            }
        }
    
        public void FillBaseText()
        {
            char[] temporaryCharArray = new char[gameCharsListSize];

            for (int i = 0; i < gameCharsListSize; i++)
            {
                temporaryCharArray[i] = gameBaseTextChar;
            }

            string temporaryString = new string(temporaryCharArray);

            baseText = temporaryString;
        }

        public void InitiatePlayerCharPosition()
        {
            char[] targetList = gameCharsLists[GameManager.ActualPlayerListIndex];
            targetList[GameManager.ActualPlayerIndex] = playerChar;
        }

        public void InitiateGameCharsLists()
        {
            for (int i = 0; i < gameCharsListsSize; i++)
            {
                gameCharsLists.Add(baseText.ToCharArray());
            }
        }

        public void MoveEnemyChar(Enemy enemy)
        {
            char[] actualTemporaryList = gameCharsLists[enemy.enemyListIndex];
            actualTemporaryList[enemy.enemyIndex] = gameBaseTextChar;

            char[] nextTemporaryList = gameCharsLists[enemy.enemyListIndex - 1];
            nextTemporaryList[enemy.enemyIndex] = enemyChar;

            gameCharsLists[enemy.enemyListIndex] = actualTemporaryList;
            gameCharsLists[enemy.enemyListIndex - 1] = nextTemporaryList;

            enemy.enemyListIndex--;
        }

        public void RemovePlayerCharFromList()
        {
            var temporaryList = gameCharsLists[GameManager.ActualPlayerListIndex];
            temporaryList[GameManager.ActualPlayerIndex] = gameBaseTextChar;
            gameCharsLists[GameManager.ActualPlayerListIndex] = temporaryList;
        }

        public void RemoveEnemyCharFromFirstList(Enemy enemy)
        {
            char[] temporaryList = gameCharsLists[0];

            temporaryList[enemy.enemyIndex] = gameBaseTextChar;


            if(GameManager.ActualPlayerIndex == enemy.enemyIndex && GameManager.ActualPlayerListIndex == enemy.enemyListIndex)
            {
                temporaryList[GameManager.ActualPlayerIndex] = playerChar;
            }

            gameCharsLists[0] = temporaryList;
        }

        public void PlaceExplosionChars()
        {
            var temporaryListA = gameCharsLists[GameManager.ActualPlayerListIndex - 1];
            var temporaryListB = gameCharsLists[GameManager.ActualPlayerListIndex];
            var temporaryListC = gameCharsLists[GameManager.ActualPlayerListIndex + 1];

            temporaryListA[GameManager.ActualPlayerIndex] = explosionChar;

            temporaryListB[GameManager.ActualPlayerIndex] = explosionChar;
            temporaryListB[GameManager.ActualPlayerIndex + 1] = explosionChar;
            temporaryListB[GameManager.ActualPlayerIndex - 1] = explosionChar;

            temporaryListC[GameManager.ActualPlayerIndex] = explosionChar;

            gameCharsLists[GameManager.ActualPlayerListIndex - 1] = temporaryListA;
            gameCharsLists[GameManager.ActualPlayerListIndex] = temporaryListB;
            gameCharsLists[GameManager.ActualPlayerListIndex + 1] = temporaryListC;
        }

        public void DisplayEndGameText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("************************* GAME OVER *************************");
            Console.WriteLine($"********************** FINAL SCORE: {GameManager.GamePoints} *********************");
            Console.WriteLine("********************* Congratulations ! *********************");
            Console.ResetColor();
        }
 
        public void DisplayMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("   ██████╗  ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗");
            Console.WriteLine("  ██╔════╝ ██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝");
            Console.WriteLine("  ██║      ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗  ");
            Console.WriteLine("  ██║      ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝  ");
            Console.WriteLine("  ╚██████╗ ╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗");
            Console.WriteLine("   ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("               █████╗ ██████╗ ██████╗  █████╗ ██╗   ██╗");
            Console.WriteLine("              ██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝");
            Console.WriteLine("              ███████║██████╔╝██████╔╝███████║ ╚████╔╝ ");
            Console.WriteLine("              ██╔══██║██╔══██╗██╔══██╗██╔══██║  ╚██╔╝  ");
            Console.WriteLine("              ██║  ██║██║  ██║██║  ██║██║  ██║   ██║   ");
            Console.WriteLine("              ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("                ██████╗  █████╗ ███╗   ███╗███████╗");
            Console.WriteLine("               ██╔════╝ ██╔══██╗████╗ ████║██╔════╝");
            Console.WriteLine("               ██║  ███╗███████║██╔████╔██║█████╗  ");
            Console.WriteLine("               ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ");
            Console.WriteLine("               ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗");
            Console.WriteLine("                ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║                    >> PRESS ANY KEY <<                   ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════╝");

            Console.ReadKey(true);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("          ░▒▓█▓▒░  INITIALIZING GAME SYSTEM  ░▒▓█▓▒░");
            Console.WriteLine("  ██████████████████████████████████████████████████████████");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}