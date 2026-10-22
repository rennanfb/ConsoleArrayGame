
using ConsoleArrayGame.Managers;

Console.OutputEncoding = System.Text.Encoding.UTF8;

GameManager.Start();
GameManager.TextManager.ShowTable();

while (true)
{
    await Task.Delay(10);

    if(GameManager.GameOver)
    {
        break;
    }

    GameManager.GameMomentCheck();
    GameManager.TextManager.ShowTable();
}

GameManager.EndGame();

await Task.Delay(1000000000);
