using GeoBlockAPI.Models;
using System.Collections.Concurrent;

namespace GeoBlockAPI.Repositories
{
    public class InMemoryRepository
    {
        public ConcurrentDictionary<string, CountryBlock> BlockedCountries { get; } = new();
        public List<BlockedAttemptLog> Logs { get; } = new();

        public bool AddCountry(string code)
        {
            return BlockedCountries.TryAdd(code.ToUpper(), new CountryBlock { CountryCode = code });
        }

        public bool RemoveCountry(string code)
        {
            return BlockedCountries.TryRemove(code.ToUpper(), out _);
        }

        public IEnumerable<CountryBlock> GetBlockedCountries() => BlockedCountries.Values;

        public void AddLog(BlockedAttemptLog log)
        {
            lock (Logs)
            {
                Logs.Add(log);
            }
        }

        public IEnumerable<BlockedAttemptLog> GetLogs() => Logs;
    }
}
