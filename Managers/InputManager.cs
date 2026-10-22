using ConsoleArrayGame.Managers;

namespace ConsoleArrayGame.GameSystem.Input
{
    internal class InputManager
    {
        public void WInputLogic()
        {
            if (GameManager.PlayerListIndex == 0)
            {
                //nothing
            }
            else
            {

                GameManager.TextManager.RemovePlayerCharFromList();

                GameManager.DecreasePlayerListIndex();

                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex];

                nextCharList[GameManager.PlayerIndex] = GameManager.TextManager.playerChar;

                GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex] = nextCharList;

            }
        }

        public void SInputLogic()
        {
            if (GameManager.PlayerListIndex == GameManager.TextManager.gameCharsListsSize - 1)
            {
                //nothing
            }
            else
            {
                GameManager.TextManager.RemovePlayerCharFromList();

                GameManager.IncreasePlayerListIndex();

                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex];

                nextCharList[GameManager.PlayerIndex] = GameManager.TextManager.playerChar;

                GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex] = nextCharList;

            }
        }

        public void AInputLogic()
        {
           
            if (GameManager.PlayerIndex == 0)
            {
                //nothing
            }
            else
            {
                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex];
                
                nextCharList[GameManager.PlayerIndex - 1] = GameManager.TextManager.playerChar;
                nextCharList[GameManager.PlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                GameManager.DecreasePlayerIndex();

                GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex] = nextCharList;
            }
        }

        public void DInputLogic()
        {
            

            if (GameManager.PlayerIndex == GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex].Length - 1)
            {
                //nothing
            }
            else
            {
                var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex];

                nextCharList[GameManager.PlayerIndex + 1] = GameManager.TextManager.playerChar;
                nextCharList[GameManager.PlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                GameManager.IncreasePlayerIndex();

                GameManager.TextManager.gameCharsLists[GameManager.PlayerListIndex] = nextCharList;
            }
        }
    }
}