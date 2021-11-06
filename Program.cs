using Newtonsoft.Json;
using razdolbaizer_3000.Exceptions;
using razdolbaizer_3000.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace razdolbaizer_3000
{
    class Program
    {
        public const string _configGamersPath = @"Config\ConfigGamers.json";
        public const string _configGunsPath = @"Config\ConfigGuns.json";

        public static Guns _guns;
        public static Gamers _gamers;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Time to dead!");
            Initialization();
            //WriteGamersInfo();
            //Console.WriteLine("=======================");
            //WriteGunsInfo();

            var gamers = new List<Gamer>();

            for (int i = 0; i < 2; i++)
            {
                gamers.Add(ChoceGamer());
            }
                       

            foreach(var gamer in gamers)
            {
                gamer.ChoceGun(_guns);
                Console.WriteLine($"Player {gamer.Name}, his gun {gamer.Gun.Name}");
            }

            System.Threading.Thread.Sleep(3000);
                        
            Console.WriteLine("Start Fighting 3.....");
            System.Threading.Thread.Sleep(500);

            Console.WriteLine("Start Fighting 2.....");
            System.Threading.Thread.Sleep(500);

            Console.WriteLine("Start Fighting 1.....");
            System.Threading.Thread.Sleep(500);

            bool endGame = true;
            var count = 0;
            var _count = 1;

            while (endGame)
            {
                if(count == 0)
                {
                    count = 1;
                    _count = 0;
                }
                else
                {
                    count = 0;
                    _count = 1;
                }
                
                try
                {
                    gamers[count].Shoot(gamers[_count]);
                }
                catch (ReloadException e)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                catch (DeadException e)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(e.Message);
                    Console.WriteLine($"Player {gamers[count].Name} is WIN!!!!");
                    Console.ResetColor();

                    endGame = false;
                }
                                
                System.Threading.Thread.Sleep(500);
            }

            Console.ReadKey();

        }

        
        public static void Initialization()
        {
            GetConfigGuns(_configGunsPath);
            GetConfigGamers(_configGamersPath);
        }

        #region privateConfigBlock

        private static Gamer ChoceGamer()
        {
            var random = new Random();
            var number = random.Next(0, _gamers.GamersList.Length - 1);

            return _gamers.GamersList[number];
        }

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
        private static void GetConfigGuns(string pathConfigFile)
        {
            _guns = JsonConvert.DeserializeObject<Guns>(File.ReadAllText(pathConfigFile));
        }

        private static void GetConfigGamers(string pathConfigFile)
        {
            _gamers = JsonConvert.DeserializeObject<Gamers>(File.ReadAllText(pathConfigFile));
        }
        #endregion
    }
}
