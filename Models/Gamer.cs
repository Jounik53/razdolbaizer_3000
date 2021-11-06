using razdolbaizer_3000.Exceptions;
using System;
using razdolbaizer_3000.Extensions;

namespace razdolbaizer_3000.Models
{
    public class Gamer
    {
        private readonly WriteConsoleExtend _writeConsoleExtend;
        private int currentMagazine;

        public Gamer()
        {
            _writeConsoleExtend = new WriteConsoleExtend();
        }

        public Gun Gun { get; set; }

        public string Name { get; set; }
        public double Life { get; set; }
        public int ChangeGun { get; set; }
        public double Tenacity { get; set; }

        public void ChoceGun(Guns guns)
        {
            var random = new Random();
            var number = random.Next(0, guns.GunsList.Length - 1);

            this.Gun = guns.GunsList[number];
            currentMagazine = this.Gun.Magazine;
            this.Gun.Initialization();
        }

        public void Reload() => this.Gun.Magazine = currentMagazine;

        public void Shoot(Gamer opponent)
        {
            var random = new Random();

            if (this.Gun.Load == false)
            {
                Reload();
                throw new ReloadException($"Player: {Name} reloaing....");
            }

            var currentTenacity = 1.0 + (random.NextDouble() * (this.Tenacity - 1.0));
            //var currentDamage = Math.Round((double)(this.Gun.Damage * currentTenacity / 100));
            var currentDamage = this.Gun.Damage * currentTenacity / 100;

            opponent.Life -= currentDamage;

            _writeConsoleExtend.Shoot($"{Name}({Math.Round(Life, 1)}) " +
                                      $"shoot: damage: {Math.Round(currentDamage, 1)}, " +
                                      $"opoonent {opponent.Name} life {Math.Round(opponent.Life, 1)}");

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"{Name} shoot: damage: {currentDamage}, opoonent {opponent.Name} life {opponent.Life}");
            //Console.ResetColor();

            if (opponent.Life <= 0)
            {
                throw new DeadException($"Player {opponent.Name} is dead!");
            }

            this.Gun.Misfire = random.Next(0, 10);
            this.Gun.Magazine -= 1;
        }
    }
}