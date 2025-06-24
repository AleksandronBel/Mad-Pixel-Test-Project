using R3;
using System;
using Zenject;

namespace Infrastructure.Services
{
    /// <summary>
    /// abstraction for every service in project
    /// </summary>
    public abstract class Service : IInitializable, IDisposable
    {
        protected readonly CompositeDisposable Disposables = new();

        void IInitializable.Initialize() => OnInitialize();

        void IDisposable.Dispose() => Disposables.Dispose();

        protected virtual void OnInitialize() { }
    }
}