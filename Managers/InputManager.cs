using ConsoleArrayGame.Managers;

namespace ConsoleArrayGame.GameSystem.Input
{
    internal class InputManager
    {
        public void WInputLogic()
        {
            if (GameManager.ActualPlayerListIndex == 1)
            {
                //nothing
            }
            else
            {
                GameManager.TextManager.RemovePlayerCharFromList();
                GameManager.DecreasePlayerListIndex();

                if (!GameManager.playerShielded)
                {
                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                }
                else
                {
                    var nextPlayerCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];
                    nextPlayerCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                    var shieldCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1];

                    shieldCharList[PlayerShield.collectableCharListIndex] = PlayerShield.centralChar;
                    shieldCharList[PlayerShield.collectableCharListIndex + 1] = PlayerShield.rightChar;
                    shieldCharList[PlayerShield.collectableCharListIndex - 1] = PlayerShield.leftChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextPlayerCharList;
                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1] = shieldCharList;
                }
            }
        }

        public void SInputLogic()
        {
            if (GameManager.ActualPlayerListIndex == GameManager.TextManager.gameCharsListsSize - 2)
            {
                //nothing
            }
            else
            {
                GameManager.TextManager.RemovePlayerCharFromList();
                GameManager.IncreasePlayerListIndex();

                if (!GameManager.playerShielded)
                {

                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                }
                else
                {
                    var nextPlayerCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];
                    nextPlayerCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.playerChar;

                    var shieldCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1];

                    shieldCharList[PlayerShield.collectableCharListIndex] = PlayerShield.centralChar;
                    shieldCharList[PlayerShield.collectableCharListIndex + 1] = PlayerShield.rightChar;
                    shieldCharList[PlayerShield.collectableCharListIndex - 1] = PlayerShield.leftChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextPlayerCharList;
                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1] = shieldCharList;
                }
            }
        }

        public void AInputLogic()
        {
            if (GameManager.ActualPlayerIndex == 1)
            {
                //nothing
            }
            else
            {
                if (!GameManager.playerShielded)
                {

                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex - 1] = GameManager.TextManager.playerChar;
                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                    GameManager.DecreasePlayerIndex();

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                }
                else
                {
                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex - 1] = GameManager.TextManager.playerChar;
                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;


                    GameManager.DecreasePlayerIndex();
                    PlayerShield.collectableCharListIndex--;


                    var shieldCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1];

                    shieldCharList[PlayerShield.collectableCharListIndex] = PlayerShield.centralChar;
                    shieldCharList[PlayerShield.collectableCharListIndex + 1] = PlayerShield.rightChar;
                    shieldCharList[PlayerShield.collectableCharListIndex - 1] = PlayerShield.leftChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1] = shieldCharList;
                }
            }
        }

        public void DInputLogic()
        {
            if (GameManager.ActualPlayerIndex == GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex].Length - 2)
            {
                //nothing
            }
            else
            {
                if (!GameManager.playerShielded)
                {


                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex + 1] = GameManager.TextManager.playerChar;
                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;

                    GameManager.IncreasePlayerIndex();

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                }
                else
                {
                    var nextCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex];

                    nextCharList[GameManager.ActualPlayerIndex + 1] = GameManager.TextManager.playerChar;
                    nextCharList[GameManager.ActualPlayerIndex] = GameManager.TextManager.gameBaseTextChar;






                    GameManager.IncreasePlayerIndex();
                    PlayerShield.collectableCharListIndex++;


                    var shieldCharList = GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1];

                    shieldCharList[PlayerShield.collectableCharListIndex] = PlayerShield.centralChar;
                    shieldCharList[PlayerShield.collectableCharListIndex + 1] = PlayerShield.rightChar;
                    shieldCharList[PlayerShield.collectableCharListIndex - 1] = PlayerShield.leftChar;

                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex] = nextCharList;
                    GameManager.TextManager.gameCharsLists[GameManager.ActualPlayerListIndex + 1] = shieldCharList;
                }
            }
        }
    }
}