
namespace ConsoleArrayGame.Managers
{
    internal class TextManager
    {
        public List<char[]>? gameCharsLists { get; set; } = new List<char[]>();

        public char gameBaseTextChar = '█';
        public char enemyChar = '▯';
        public char playerChar = '•';
        public char explosionChar = '*';

        public int gameCharsListsSize = 24;
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
            char[] targetList = gameCharsLists[GameManager.PlayerListIndex];
            targetList[GameManager.PlayerIndex] = playerChar;
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
            var temporaryList = gameCharsLists[GameManager.PlayerListIndex];
            temporaryList[GameManager.PlayerIndex] = gameBaseTextChar;
            gameCharsLists[GameManager.PlayerListIndex] = temporaryList;
        }

        public void RemoveEnemyCharFromFirstList(Enemy enemy)
        {
            char[] temporaryList = gameCharsLists[0];

            temporaryList[enemy.enemyIndex] = gameBaseTextChar;


            if(GameManager.PlayerIndex == enemy.enemyIndex && GameManager.PlayerListIndex == enemy.enemyListIndex)
            {
                temporaryList[GameManager.PlayerIndex] = playerChar;
            }

            gameCharsLists[0] = temporaryList;
        }

        public void PlaceExplosionChars()
        {
            var temporaryListA = gameCharsLists[GameManager.PlayerListIndex - 1];
            var temporaryListB = gameCharsLists[GameManager.PlayerListIndex];
            var temporaryListC = gameCharsLists[GameManager.PlayerListIndex + 1];

            temporaryListA[GameManager.PlayerIndex] = explosionChar;

            temporaryListB[GameManager.PlayerIndex] = explosionChar;
            temporaryListB[GameManager.PlayerIndex + 1] = explosionChar;
            temporaryListB[GameManager.PlayerIndex - 1] = explosionChar;

            temporaryListC[GameManager.PlayerIndex] = explosionChar;

            gameCharsLists[GameManager.PlayerListIndex - 1] = temporaryListA;
            gameCharsLists[GameManager.PlayerListIndex] = temporaryListB;
            gameCharsLists[GameManager.PlayerListIndex + 1] = temporaryListC;
        }

        public void DisplayEndGameText()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("************************* GAME OVER *************************");
            Console.WriteLine($"********************** FINAL SCORE: {GameManager.GamePoints} *********************");
            Console.WriteLine("********************* Congratulations ! *********************");
            Console.ResetColor();
        }
 
    }
}