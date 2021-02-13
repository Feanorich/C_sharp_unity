
namespace Geekbrains
{
    /// <summary>
    /// Бонус влияющий на игрока
    /// </summary>
    public abstract class Bonus
    {
        protected float lifeTime;
        public abstract bool GetBonus(Player _player);
    }
}