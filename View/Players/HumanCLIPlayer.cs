using System;
using System.Diagnostics;
using System.Linq;
using Model;
using View.Shared;

namespace View.Players
{
    public class HumanCLIPlayer : IInput
    {
        public Color MakeMove(Color[,] board)
        {
            return PromptForColor();
        }

        public void GameOver(WinEventArgs winArgs)
        {
            Console.WriteLine(winArgs);
        }

        private static Color PromptForColor()
        {
            Console.WriteLine("PickColor your color:");
            Console.WriteLine("1: Red");
            Console.WriteLine("2: Orange");
            Console.WriteLine("3: Yellow");
            Console.WriteLine("4: Green");
            Console.WriteLine("5: Blue");
            Console.WriteLine("6: Purple");
            string strResponse = Console.ReadLine();
            return ParseResponse(strResponse);
        }

        private static Color ParseResponse(string strResponse)
        {
            int response;
            if (int.TryParse(strResponse, out response))
            {
                //treat as number
                switch (response)
                {
                    case 1:
                        return Color.Red;
                    case 2:
                        return Color.Orange;
                    case 3:
                        return Color.Yellow;
                    case 4:
                        return Color.Green;
                    case 5:
                        return Color.Blue;
                    case 6:
                        return Color.Purple;
                }
                Debug.Fail("All Colors should be accounted for");
                return default(Color);
            }
            else
            {
                //treat as string
                return LetterToColor(strResponse.First());
            }
        }
        private static Color LetterToColor(char letter)
        {
            Color result = default(Color);
            switch (letter)
            {
                case 'R':
                case 'r':
                    result = Color.Red;
                    break;
                case 'Y':
                case 'y':
                    result = Color.Yellow;
                    break;
                case 'O':
                case 'o':
                    result = Color.Orange;
                    break;
                case 'G':
                case 'g':
                    result = Color.Green;
                    break;
                case 'B':
                case 'b':
                    result = Color.Blue;
                    break;
                case 'P':
                case 'p':
                    result = Color.Purple;
                    break;
            }
            return result;

        }
    }
}
