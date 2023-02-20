using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public interface IPlayerControlService : IService
    {
        void Move(Vector2 direction);

        void Dash(Vector2 direction);

        void ShootWeapon(Vector2 direction);
    }
}
