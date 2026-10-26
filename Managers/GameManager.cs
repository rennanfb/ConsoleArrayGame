using ConsoleArrayGame.GameSystem.Input;

namespace ConsoleArrayGame.Managers;

internal static class GameManager
{
    public static InputManager InputManager { get; private set; } = new InputManager();
    public static TextManager TextManager { get; private set; } = new TextManager();
    public static int GamePoints { get; set; }
    public static int InitialPlayerIndex { get; private set; } = 31;
    public static int ActualPlayerIndex { get; private set; }
    public static int InitialPlayerListIndex { get; private set; } = 13;
    public static int ActualPlayerListIndex { get; private set; }
    public static bool playerShielded { get; private set; } = false;
    public static List<Enemy> EnemiesList { get; private set; } = new List<Enemy>();
    public static DateTime? EnemySpawnCooldown { get; private set; } = null;
    public static bool GameOver { get; set; } = false;

    public static void Start()
    {
        ActualPlayerIndex = InitialPlayerIndex;
        ActualPlayerListIndex = InitialPlayerListIndex;

        TextManager.FillBaseText();

        TextManager.InitiateGameCharsLists();
        TextManager.InitiatePlayerCharPosition();
    }

    public static void ConfigurateInitialState()
    {
        GameOver = false;
        GamePoints = 0;
        playerShielded = false;
        PlayerShield.shieldActive = false;
        TextManager.gameCharsLists.Clear();
        EnemiesList.Clear();
    }

    public static void GameMomentCheck()
    {
        if (GameOver) { return; }
        GamePoints++;

        TextManager.SkipFrame();

        TryPlaceShieldCollectableChar();
        TextManager.RemoveOldShieldChars();
        SummonEnemy();
        CheckEnemyPlayerShieldCollision();
        MoveEnemies();
        CheckGameOver();
        InputLogic();
        CheckCollectables();
    }

    public static void TryPlaceShieldCollectableChar()
    {
        if (playerShielded == true  || PlayerShield.shieldActive) return;

        Random rnd = new Random();

        var chance = rnd.Next(0, 1000);

        if(chance >= 30) { return; }

        var ListIndex = rnd.Next(1, TextManager.gameCharsListsSize - 1);
        var CharIndex = rnd.Next(1, TextManager.gameCharsListSize - 1);

        PlayerShield.collectableCharListIndex = CharIndex;
        PlayerShield.collectableCharListsIndex = ListIndex;

        var temporaryList = TextManager.gameCharsLists[ListIndex];
        temporaryList[CharIndex] = PlayerShield.shieldCollectableChar;

        PlayerShield.shieldActive = true;

    }

    public static void CheckCollectables()
    {
        if (playerShielded) return;
        if (!PlayerShield.shieldActive) return;

        if (ActualPlayerIndex == PlayerShield.collectableCharListIndex && ActualPlayerListIndex == PlayerShield.collectableCharListsIndex)
        {

            var shieldCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1];

            shieldCharList[PlayerShield.collectableCharListIndex] = PlayerShield.centralChar;
            shieldCharList[PlayerShield.collectableCharListIndex + 1] = PlayerShield.rightChar;
            shieldCharList[PlayerShield.collectableCharListIndex - 1] = PlayerShield.leftChar;

            GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1] = shieldCharList;

            playerShielded = true;
            PlayerShield.shieldActive = false;

        }
    }

    public static void CheckEnemyPlayerShieldCollision()
    {

        if (playerShielded == false) return;

        Enemy enemyToRemove = null;

        foreach(var enemy in EnemiesList)
        {

            if (enemy.enemyListIndex == ActualPlayerListIndex + 1)
            {
                if (enemy.enemyIndex == ActualPlayerIndex || enemy.enemyIndex == ActualPlayerIndex - 1 || enemy.enemyIndex == ActualPlayerIndex + 1)
                {
                    var temporaryList = TextManager.gameCharsLists[ActualPlayerListIndex + 1];

                    temporaryList[ActualPlayerIndex] = TextManager.gameBaseTextChar;
                    temporaryList[ActualPlayerIndex + 1] = TextManager.gameBaseTextChar;
                    temporaryList[ActualPlayerIndex - 1] = TextManager.gameBaseTextChar;

                    TextManager.gameCharsLists[ActualPlayerListIndex + 1] = temporaryList;
                    enemyToRemove = enemy;
                    playerShielded = false;
                    PlayerShield.ResetCollectableIndexes();
                    PlayerShield.shieldActive = false;

                }
            }
        }
        if (enemyToRemove != null)
        {
            EnemiesList.Remove(enemyToRemove);
        }
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
        Enemy? enemyToRemove = null;

        foreach (var enemy in EnemiesList)
        {
            if (PlayerShield.shieldActive && enemy.enemyListIndex == PlayerShield.collectableCharListsIndex)
            {

                if (enemy.enemyIndex == PlayerShield.collectableCharListIndex)
                {
                    enemyToRemove = enemy;
                    PlayerShield.shieldActive = false;

                    var temporaryList = TextManager.gameCharsLists[PlayerShield.collectableCharListsIndex];
                    temporaryList[PlayerShield.collectableCharListIndex] = TextManager.gameBaseTextChar;

                    TextManager.gameCharsLists[PlayerShield.collectableCharListsIndex] = temporaryList;
                }
            }
        }
        if (enemyToRemove != null)
        {
            var temporaryList = TextManager.gameCharsLists[enemyToRemove.enemyListIndex];
            temporaryList[enemyToRemove.enemyIndex] = TextManager.gameBaseTextChar;
            EnemiesList.Remove(enemyToRemove);
        }


        foreach (var enemy in EnemiesList)
        {
            if (enemy.enemyMovementCooldown == null && enemy != enemyToRemove)
            {
                enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.15);
            }

            if (DateTime.Now >= enemy.enemyMovementCooldown && enemy.enemyListIndex != 0 && enemy != enemyToRemove)
            {
                TextManager.MoveEnemyChar(enemy);
                UpdateEnemyMovementCooldown(enemy);
            }

            if (DateTime.Now >= enemy.enemyMovementCooldown && enemy.enemyListIndex == 0 && enemy != enemyToRemove)
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
                if (enemy.enemyIndex == ActualPlayerIndex && enemy.enemyListIndex == ActualPlayerListIndex)
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
        else if (GameManager.GamePoints >= 2000 && GameManager.GamePoints <= 2500)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.03);
        }
        else if (GameManager.GamePoints >= 2500 && GameManager.GamePoints <= 3000)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.02);
        }
        else if (GameManager.GamePoints >= 3000)
        {
            EnemySpawnCooldown = DateTime.Now.AddSeconds(0.015);
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
        else if (GameManager.GamePoints >= 2000 && GameManager.GamePoints <= 2500)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.05);
        }
        else if (GameManager.GamePoints >= 2500 && GameManager.GamePoints <= 3000)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.03);
        }
        else if (GameManager.GamePoints >= 3000)
        {
            enemy.enemyMovementCooldown = DateTime.Now.AddSeconds(0.015);
        }
    }

    public static void IncreasePlayerListIndex()
    {
        ActualPlayerListIndex++;
    }

    public static void DecreasePlayerListIndex()
    {
        ActualPlayerListIndex--;
    }

    public static void IncreasePlayerIndex()
    {
        ActualPlayerIndex++;
    }

    public static void DecreasePlayerIndex()
    {
        ActualPlayerIndex--;
    }
}

