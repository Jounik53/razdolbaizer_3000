using razdolbaizer_3000.Exceptions;
using System;
using razdolbaizer_3000.Extensions;

namespace razdolbaizer_3000.Models
{
    public class Gamer
    {
        private readonly WriteConsoleExtend _writeConsoleExtend;
        private int currentMagazine;
        private Random _random;

        public Gamer()
        {
            _writeConsoleExtend = new WriteConsoleExtend();
            _random = new Random();
        }

        public Gun Gun { get; set; }

        public string Name { get; set; }
        public double Life { get; set; }
        public int ChangeGun { get; set; }
        public double Tenacity { get; set; }

        public void ChoceGun(Guns guns)
        {
            var number = _random.Next(0, guns.GunsList.Length - 1);

            this.Gun = guns.GunsList[number];
            currentMagazine = this.Gun.Magazine;
            this.Gun.Initialization();
        }

        public void Reload() => this.Gun.Magazine = currentMagazine;

        public void Shoot(Gamer opponent)
        {
            if (this.Gun.Load == false)
            {
                Reload();
                throw new ReloadException($"Player: {Name} reloading....");
            }

            var currentTenacity = 1.0 + (_random.NextDouble() * (this.Tenacity - 1.0));
            var currentDamage = this.Gun.Damage * currentTenacity / 100;

            if (ChanceCritical())
            {
                currentDamage = GetCriticalDamage(currentDamage);
            }

            opponent.Life -= currentDamage;

            _writeConsoleExtend.Shoot($"{Name}({Math.Round(Life, 1)}) " +
                                      $"shoot: damage: {Math.Round(currentDamage, 1)}, " +
                                      $"opoonent {opponent.Name} life {Math.Round(opponent.Life, 1)}", ChanceCritical());

            if (opponent.Life <= 0)
            {
                throw new DeadException($"Player {opponent.Name} is dead!");
            }

            this.Gun.Misfire = _random.Next(0, 10);
            this.Gun.Magazine -= 1;
        }

        private bool ChanceCritical()
        {
            var chance = _random.Next(1, 10);

            if (chance == 7)
            {
                return true;
            }

            return false;
        }

        private double GetCriticalDamage(double currentDamage) => currentDamage * 2;
    }
}