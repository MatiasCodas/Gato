using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public interface IPlayerControlService : IService
    {
        PlayerStats FetchPlayerStats();

        void Move(Vector2 direction);

        void Dash(Vector2 direction);

        void ShootWeapon();
    }
}
