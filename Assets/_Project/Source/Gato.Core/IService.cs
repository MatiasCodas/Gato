using System;
using UnityEngine.Scripting;

namespace Gato.Core
{
    [RequireImplementors]
    public interface IService : IDisposable
    {
        public ServiceLocator OwningLocator { get; set; }
    }
}
