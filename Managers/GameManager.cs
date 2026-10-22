using ConsoleArrayGame.GameSystem.Input;

namespace ConsoleArrayGame.Managers;

internal static class GameManager
{
    public static InputManager InputManager { get; private set; } = new InputManager();
    public static TextManager TextManager { get; private set; } = new TextManager();

    public static int GamePoints { get; private set; }
    public static int PlayerIndex { get; private set; } = 31;
    public static int PlayerListIndex { get; private set; } = 13;
    public static List<Enemy> EnemiesList { get; private set; } = new List<Enemy>();
    public static DateTime? EnemySpawnCooldown { get; private set; } = null;
    public static bool GameOver { get; private set; } = false;

    public static void Start()
    {
        TextManager.FillBaseText();

        TextManager.InitiateGameCharsLists();
        TextManager.InitiatePlayerCharPosition();
    }

    public static void GameMomentCheck()
    {
        if (GameOver) { return; }
        GamePoints++;

        TextManager.SkipFrame();

        SummonEnemy();
        MoveEnemies();
        CheckGameOver();
        InputLogic();
    }

    public static void EndGame()
    {
        GameManager.TextManager.SkipFrame();
        GameManager.TextManager.PlaceExplosionChars();
        GameManager.TextManager.ShowTable();
        GameManager.TextManager.DisplayEndGameText();
    }

    private static void InputLogic()
    {
        if (Console.KeyAvailable)
        {
            char playerInput = Console.ReadKey(true).KeyChar;

            if (playerInput == 'w' || playerInput == 'a' || playerInput == 's' || playerInput == 'd')
            {
                if (playerInput == 'w')
                {
                    InputManager.WInputLogic();
                }
                else if (playerInput == 's')
                {
                    InputManager.SInputLogic();
                }
                else if (playerInput == 'a')
                {
                    InputManager.AInputLogic();
                }
                else if (playerInput == 'd')
                {
                    InputManager.DInputLogic();
                }
            }
            else
            {
                return;
            }
        }
    }

    

    public static void SummonEnemy()
    {
        if (DateTime.Now <= EnemySpawnCooldown) { return; }

        var temporaryList = TextManager.gameCharsLists[TextManager.gameCharsListsSize - 1];

        Random rnd = new Random();

        int listIndexValue = rnd.Next(0, TextManager.gameCharsListSize);

        if (temporaryList[listIndexValue] == TextManager.enemyChar) 
        {
            List<int> indexesList = new List<int>();
        
            int iterationIndex = 0;

            foreach (var item in temporaryList)
            {
        
                if(item == TextManager.gameBaseTextChar)
                {
                    indexesList.Add(iterationIndex);
                }
        
                iterationIndex++;
            }

            listIndexValue = indexesList[rnd.Next(0, indexesList.Count - 1)];
        
            indexesList.Clear();
        }

        Enemy enemy = new Enemy(listIndexValue, TextManager.gameCharsListsSize - 1);

        GameManager.EnemiesList.Add(enemy);

        temporaryList[listIndexValue] = TextManager.enemyChar;

        UpdateEnemySpawnCooldown();

    }

    public static void MoveEnemies()
    {
        foreach (var enemy in EnemiesList)
        {
            if (enemy.enemyMovementCooldown == null)
            {
                enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.15);
            }

            if (DateTime.Now >= enemy.enemyMovementCooldown && enemy.enemyListIndex != 0)
            {
                TextManager.MoveEnemyChar(enemy);
                UpdateEnemyMovementCooldown(enemy);
            }

            if (DateTime.Now >= enemy.enemyMovementCooldown && enemy.enemyListIndex == 0)
            {
                TextManager.RemoveEnemyCharFromFirstList(enemy);
                enemy.active = false;
            }
        }
    }

    public static void CheckGameOver()
    {
        foreach (var enemy in EnemiesList)
        {
            if (enemy.active)
            {
                if (enemy.enemyIndex == PlayerIndex && enemy.enemyListIndex == PlayerListIndex)
                {
                    GameManager.GameOver = true;
                }
            }
        }
    }

    public static void UpdateEnemySpawnCooldown()
    {
        if (GameManager.GamePoints <= 500)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.25);
        }
        else if (GameManager.GamePoints >= 500 && GameManager.GamePoints <= 1000)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.20);
        }
        else if (GameManager.GamePoints >= 1000 && GameManager.GamePoints <= 1500)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.13);
        }
        else if (GameManager.GamePoints >= 1500 && GameManager.GamePoints <= 2000)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.07);
        }
        else if (GameManager.GamePoints >= 2000)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.03);
        }
    }

    public static void UpdateEnemyMovementCooldown(Enemy enemy)
    {
        if (GameManager.GamePoints <= 500)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.20);
        }
        else if (GameManager.GamePoints >= 500 && GameManager.GamePoints <= 1000)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.15);
        }
        else if (GameManager.GamePoints >= 1000 && GameManager.GamePoints <= 1500)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.10);
        }
        else if (GameManager.GamePoints >= 1500 && GameManager.GamePoints <= 2000)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.07);
        }
        else if (GameManager.GamePoints >= 2000)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.05);
        }
    }

    public static void IncreasePlayerListIndex()
    {
        PlayerListIndex++;
    }

    public static void DecreasePlayerListIndex()
    {
        PlayerListIndex--;
    }

    public static void IncreasePlayerIndex()
    {
        PlayerIndex++;
    }

    public static void DecreasePlayerIndex()
    {
        PlayerIndex--;
    }
}

