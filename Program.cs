using Newtonsoft.Json;
using razdolbaizer_3000.Exceptions;
using razdolbaizer_3000.Models;
using System;
using System.Collections.Generic;
using System.IO;
using razdolbaizer_3000.Extensions;

namespace razdolbaizer_3000
{
    class Program
    {
        public const string ConfigGamersPath = @"Config\ConfigGamers.json";
        public const string ConfigGunsPath = @"Config\ConfigGuns.json";

        private static WriteConsoleExtend _writeConsoleExtend;

        public static Guns _guns;
        public static Gamers _gamers;

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Time to dead!");
                System.Threading.Thread.Sleep(600);
                Console.Clear();
                System.Threading.Thread.Sleep(400);
            }


            _writeConsoleExtend = new WriteConsoleExtend();

            Initialization();

            var gamers = new List<Gamer>();

            for (int i = 0; i < 2; i++)
            {
                if (i > 0)
                {
                    gamers.Add(ChoiceGamer(gamers[0]));
                }
                else
                {
                    gamers.Add(ChoiceGamer());
                }
            }

            foreach (var gamer in gamers)
            {
                gamer.ChoiceGun(_guns);
                _writeConsoleExtend.WritePlayerName($"Player {gamer.Name}, his gun {gamer.Gun.Name}");
            }

            System.Threading.Thread.Sleep(3000);

            _writeConsoleExtend.WriteLine("Start Fighting 3.....", 500);
            _writeConsoleExtend.WriteLine("Start Fighting 2.....", 500);
            _writeConsoleExtend.WriteLine("Start Fighting 1.....", 500);

            bool endGame = true;
            var count = 0;

            while (endGame)
            {
                var unCounty = 1;
                if (count == 0)
                {
                    count = 1;
                    unCounty = 0;
                }
                else
                {
                    count = 0;
                    unCounty = 1;
                }

                try
                {
                    gamers[count].Shoot(gamers[unCounty], count == 1);
                }
                catch (ReloadException e)
                {
                    _writeConsoleExtend.WriteReloadPlayer(e.Message);
                }
                catch (DeadException e)
                {
                    _writeConsoleExtend.WriteDeadMessage(e.Message);
                    _writeConsoleExtend.WriteWinPlayerName($"Player {gamers[count].Name} is WIN!!!");

                    endGame = false;
                }

                System.Threading.Thread.Sleep(500);
            }

            Console.ReadKey();
        }


        public static void Initialization()
        {
            GetConfigGuns(ConfigGunsPath);
            GetConfigGamers(ConfigGamersPath);
        }

        #region Privat configuration block

        private static Gamer ChoiceGamer(Gamer firstGamer = null)
        {
            var random = new Random();
            var number = random.Next(0, _gamers.GamersList.Length);

            if (firstGamer != null)
            {
                if (firstGamer.Name == _gamers.GamersList[number].Name)
                {
                    return ChoiceGamer(firstGamer);
                }
            }

            return _gamers.GamersList[number];
        }

        private static void GetConfigGuns(string pathConfigFile)
        {
            _guns = JsonConvert.DeserializeObject<Guns>(File.ReadAllText(pathConfigFile));
        }

        private static void GetConfigGamers(string pathConfigFile)
        {
            _gamers = JsonConvert.DeserializeObject<Gamers>(File.ReadAllText(pathConfigFile));
        }

        #region Private Get info block

        private static void WriteGamersInfo()
        {
            foreach (var item in _gamers.GamersList)
            {
                Console.WriteLine("============");
                Console.WriteLine($"Имя: {item.Name}");
                Console.WriteLine($"Количество жизней: {item.Life}");
                Console.WriteLine($"Количество смен оружия: {item.ChangeGun}");
                Console.WriteLine($"Меткость: {item.Tenacity}");
            }
        }

        private static void WriteGunsInfo()
        {
            foreach (var item in _guns.GunsList)
            {
                Console.WriteLine("============");
                Console.WriteLine($"Название: {item.Name}");
                Console.WriteLine($"Калибр: {item.Calibr}");
                Console.WriteLine($"Урон: {item.Damage}");
                Console.WriteLine($"Количество патронов: {item.Magazine}");
            }
        }

        #endregion

        #endregion
    }
}