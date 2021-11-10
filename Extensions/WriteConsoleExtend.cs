using System;

namespace razdolbaizer_3000.Extensions
{
    public class WriteConsoleExtend
    {
        /// <summary>
        /// Default color settings
        /// </summary>
        private readonly ConsoleColor _defaultTextConsole = ConsoleColor.White;

        private readonly ConsoleColor _colorShoot = ConsoleColor.DarkRed;
        private readonly ConsoleColor _colorShootSecond = ConsoleColor.Red;
        private readonly ConsoleColor _colorPlayerName = ConsoleColor.DarkMagenta;
        private readonly ConsoleColor _colorDeadPlayerName = ConsoleColor.Yellow;
        private readonly ConsoleColor _colorWinPlayerName = ConsoleColor.Cyan;
        private readonly ConsoleColor _colorReloadPlayer = ConsoleColor.DarkGreen;
        private readonly ConsoleColor _colorMissPlayer = ConsoleColor.Yellow;

        /// <summary>
        /// Custom color settings
        /// </summary>
        private ConsoleColor textConsole;

        private ConsoleColor colorShoot;
        private ConsoleColor colorShootSecond;
        private ConsoleColor colorPlayerName;
        private ConsoleColor colorDeadPlayerName;
        private ConsoleColor colorWinPlayerName;
        private ConsoleColor colorReloadPlayer;
        private ConsoleColor colorMissPlayer;

        private bool _isCustomColor = false;

        public WriteConsoleExtend()
        {
        }

        public WriteConsoleExtend(ConsoleColor textConsole, ConsoleColor colorShoot, ConsoleColor colorPlayerName,
            ConsoleColor colorDeadPlayerName, ConsoleColor colorWinPlayerName, ConsoleColor colorReloadPlayer
            , ConsoleColor colorShootSecond, ConsoleColor colorMissPlayer)
        {
            this.textConsole = textConsole;
            this.colorShoot = colorShoot;
            this.colorPlayerName = colorPlayerName;
            this.colorDeadPlayerName = colorDeadPlayerName;
            this.colorWinPlayerName = colorWinPlayerName;
            this.colorReloadPlayer = colorReloadPlayer;
            this.colorShootSecond = colorShootSecond;
            this.colorMissPlayer = colorMissPlayer;

            _isCustomColor = true;
        }

        public void Miss(string message)
        {
            ApplyCustomColor();

            var wigth = Console.WindowWidth;
            var lengthMessage = message.Length;

            var cursorPos = (wigth / 2) - (lengthMessage / 2);

            Console.ForegroundColor = colorMissPlayer;
            Console.CursorLeft = cursorPos;
            Console.WriteLine(message);
            Console.ResetColor();

        }

        /// <summary>
        /// Method shoot in custom color
        /// </summary>
        /// <param name="message"></param>
        public void Shoot(string message, bool critical = false, bool secondGamer = false, string firstGamerName = "")
        {
            ApplyCustomColor();

            Console.ForegroundColor = colorShoot;
            Console.WriteLine();
            if (critical)
            {
                message += " !Critical Damage!";
                WriteFrame("-", message, colorShoot);
                Console.WriteLine();
                Console.WriteLine(message);
                WriteFrame("-", message, colorShoot);
            }

            if (secondGamer)
            {
                Console.CursorLeft = firstGamerName.Length;
                Console.ForegroundColor = _colorShootSecond;
            }

            if (!critical)
            {
                Console.Write(message);
            }

            Console.ResetColor();
        }

        public void WritePlayerName(string message)
        {
            ApplyCustomColor();

            Console.ForegroundColor = colorPlayerName;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WriteLine(string message, int sleep = 0)
        {
            Console.ForegroundColor = textConsole;
            Console.WriteLine(message);
            Console.ResetColor();

            if (sleep > 0)
            {
                System.Threading.Thread.Sleep(sleep);
            }
        }

        public void WriteDeadMessage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = colorDeadPlayerName;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WriteWinPlayerName(string message)
        {
            Console.ForegroundColor = colorWinPlayerName;
            // Enter top
            Console.WriteLine();
            
            WriteFrame("=", message, colorWinPlayerName);
            
            Console.WriteLine();

            Console.WriteLine($"= {message} =");

            WriteFrame("=", message, colorWinPlayerName);
        }

        public void WriteReloadPlayer(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = colorReloadPlayer;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        private void WriteFrame(string symbol, string message, ConsoleColor color)
        {
            var countSymbolMessage = message.Length;

            Console.ForegroundColor = color;

            for (int i = 0; i < countSymbolMessage + 4; i++)
            {
                Console.Write(symbol);
            }

            Console.ResetColor();
        }

        private void ApplyCustomColor()
        {
            if (!_isCustomColor)
            {
                textConsole = _defaultTextConsole;
                colorShoot = _colorShoot;
                colorShootSecond = _colorShootSecond;
                colorPlayerName = _colorPlayerName;
                colorDeadPlayerName = _colorDeadPlayerName;
                colorWinPlayerName = _colorWinPlayerName;
                colorReloadPlayer = _colorReloadPlayer;
            }
        }
    }
}