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
        private readonly ConsoleColor _colorPlayerName = ConsoleColor.DarkMagenta;
        private readonly ConsoleColor _colorDeadPlayerName = ConsoleColor.Yellow;
        private readonly ConsoleColor _colorWinPlayerName = ConsoleColor.Cyan;
        private readonly ConsoleColor _colorReloadPlayer = ConsoleColor.DarkGreen;

        /// <summary>
        /// Custom color settings
        /// </summary>
        private ConsoleColor textConsole;

        private ConsoleColor colorShoot;
        private ConsoleColor colorPlayerName;
        private ConsoleColor colorDeadPlayerName;
        private ConsoleColor colorWinPlayerName;
        private ConsoleColor colorReloadPlayer;

        private bool _isCustomColor = false;

        public WriteConsoleExtend()
        {
        }

        public WriteConsoleExtend(ConsoleColor textConsole, ConsoleColor colorShoot, ConsoleColor colorPlayerName,
            ConsoleColor colorDeadPlayerName, ConsoleColor colorWinPlayerName, ConsoleColor colorReloadPlayer)
        {
            this.textConsole = textConsole;
            this.colorShoot = colorShoot;
            this.colorPlayerName = colorPlayerName;
            this.colorDeadPlayerName = colorDeadPlayerName;
            this.colorWinPlayerName = colorWinPlayerName;
            this.colorReloadPlayer = colorReloadPlayer;

            _isCustomColor = true;
        }

        /// <summary>
        /// Method shoot in custom color
        /// </summary>
        /// <param name="message"></param>
        public void Shoot(string message, bool critical = false)
        {
            ApplyCustomColor();

            Console.ForegroundColor = colorShoot;
            if (!critical)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message + " !Critical Damage!");
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
            Console.ForegroundColor = colorDeadPlayerName;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WriteWinPlayerName(string message)
        {
            var countSymbolMessage = message.Length;

            Console.ForegroundColor = colorWinPlayerName;
            // Enter top
            Console.WriteLine();
            for (int i = 0; i < countSymbolMessage + 4; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();

            Console.WriteLine($"= {message} =");

            for (int i = 0; i < countSymbolMessage + 4; i++)
            {
                Console.Write("=");
            }
        }

        public void WriteReloadPlayer(string message)
        {
            Console.ForegroundColor = colorReloadPlayer;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        private void ApplyCustomColor()
        {
            if (!_isCustomColor)
            {
                textConsole = _defaultTextConsole;
                colorShoot = _colorShoot;
                colorPlayerName = _colorPlayerName;
                colorDeadPlayerName = _colorDeadPlayerName;
                colorWinPlayerName = _colorWinPlayerName;
                colorReloadPlayer = _colorReloadPlayer;
            }
        }
    }
}