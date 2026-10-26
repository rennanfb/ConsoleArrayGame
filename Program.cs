
using ConsoleArrayGame.Managers;

Console.OutputEncoding = System.Text.Encoding.UTF8;

int? iteration = 0;

while(true)
{
    if(iteration > 0)
    {
        GameManager.ConfigurateInitialState();
    }

    GameManager.TextManager.DisplayMainMenu();

    await Task.Delay(1000);

    Console.Clear();

    GameManager.Start();
    GameManager.TextManager.ShowTable();

    while (true)
    {
        await Task.Delay(10);

        if (GameManager.GameOver)
        {
            break;
        }

        GameManager.GameMomentCheck();
        GameManager.TextManager.ShowTable();
    }

    GameManager.EndGame();

    await Task.Delay(1000);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("           Press Any Key to Return to the main menu");
    Console.ForegroundColor = ConsoleColor.White;

    await Task.Delay(1000);

    while (Console.KeyAvailable)
    {
        Console.ReadKey(true);
    }
    Console.ReadKey(true);
    Console.Clear();

    if(iteration == 0)
    {
        iteration++;
    }
}
