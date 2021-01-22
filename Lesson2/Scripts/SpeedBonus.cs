using UnityEngine;

namespace Geekbrains
{
    /// <summary>
    /// Бонус изменяющий скорость
    /// </summary>
    public class SpeedBonus : Bonus
    {        
        private float BonusSpeed = 3;

        public SpeedBonus() : base()
        {
            lifeTime = 4;
        }
        
        public SpeedBonus(float _bonusSpeed) : this()
        {
            BonusSpeed = _bonusSpeed;
        }
        public SpeedBonus(float _bonusSpeed, float _lifeTime) : this(_bonusSpeed)
        {
            lifeTime = _lifeTime;
        }        

        public override bool GetBonus(Player _player)
        {
            if (lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
                _player.Speed += BonusSpeed;
                return true;
            }
            else 
            {                
                return false;
            }
        }

    }

}
