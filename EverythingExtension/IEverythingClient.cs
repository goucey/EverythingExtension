using EverythingExtension.Internal;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension
{
    internal interface IEverythingClient : IDisposable
    {
        #region Public Methods

        public long Cookie { get; }
        public ConcurrentQueue<SearchResult> SearchResults { get; }
        Version Version { get; }

        void EnsureEverythingAvailable();

        void Cancel();

        bool Initialize();

        void Execute(string searchText, long cookie);

        uint IncRunCountFromFilename(string filename);

        #endregion Public Methods
    }
}