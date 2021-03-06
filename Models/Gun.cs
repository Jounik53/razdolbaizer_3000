using System;

namespace razdolbaizer_3000.Models
{
    public class Gun
    {
        public string Name { set; get; }
        public string Calibr { set; get; }
        public int Magazine { set; get; }
        public double Damage { get; set; }
        public double CritDamage { get; set; }  //урон от крита
        public double ChanceCrit { get; set; } // шанс крита(процент)
        
        public bool Load 
        {
            get
            {
                if (Magazine <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        public bool Chance
        {
            get
            {
                if (ChanceCrit >= 100)
                {
                    return true;
                }

                return false;
            }
        }


        public int Misfire { get; set; }

        public void Initialization()
        {
            var random = new Random();
            this.Misfire = random.Next(1, 10);
        }

    }
}
