using ConsoleArrayGame.Managers;

namespace ConsoleArrayGame.GameSystem.Input
{
    internal class InputManager
    {
        public void WInputLogic()
        {
            if (GameManager.ActualPlayerListIndex == 0)
            {
                //nothing
            }
            else
            {
                GameManager.TextManager.RemovePlayerCharFromList();

                GameManager.DecreasePlayerListIndex();

                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
            }
        }

        public void SInputLogic()
        {
            if (GameManager.ActualPlayerListIndex == GameManager.TextManager.gameCharsListsSize - 1)
            {
                //nothing
            }
            else
            {
                GameManager.TextManager.RemovePlayerCharFromList();

                GameManager.IncreasePlayerListIndex();

                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
            }
        }

        public void AInputLogic()
        {
            if (GameManager.ActualPlayerIndex == 0)
            {
                //nothing
            }
            else
            {
                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];
                
                nextCharList[GameManager.ActualPlayerIndex - 1] = GameManager.TextManager.playerChar;
                nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                GameManager.DecreasePlayerIndex();

                GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
            }
        }

        public void DInputLogic()
        {
            if (GameManager.ActualPlayerIndex == GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex].Length - 1)
            {
                //nothing
            }
            else
            {
                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                nextCharList[GameManager.ActualPlayerIndex + 1] = GameManager.TextManager.playerChar;
                nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                GameManager.IncreasePlayerIndex();

                GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
            }
        }
    }
}