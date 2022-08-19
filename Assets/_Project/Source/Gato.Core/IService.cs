using System;
using UnityEngine.Scripting;

namespace Gato.Core
{
    [RequireImplementors]
    public interface IService : IDisposable
    {
        ServiceLocator OwningLocator { get; set; }
    }
}
