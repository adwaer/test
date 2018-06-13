using System.Collections.Generic;
using System.Threading;

namespace M13.InterviewProject.Services
{
    /// <summary>
    /// Site Rules Service
    /// </summary>
    public class RulesService
    {
        private readonly ReaderWriterLockSlim _lockSlim;
        private readonly Dictionary<string, string> _rules = new Dictionary<string, string>();

        public RulesService() => _lockSlim = new ReaderWriterLockSlim();

        public void Set(string site, string rule)
        {
            _lockSlim.EnterWriteLock();
            try
            {
                _rules[site.ToUpperInvariant()] = rule;
            }
            finally
            {
                _lockSlim.ExitWriteLock();
            }
        }

        public string Get(string site)
        {
            _lockSlim.EnterReadLock();
            try
            {
                if (_rules.TryGetValue(site.ToUpperInvariant(), out site))
                {
                    return site;
                }
                return null;
            }
            finally
            {
                _lockSlim.ExitReadLock();
            }
        }

        public void Delete(string site)
        {
            _lockSlim.EnterWriteLock();
            try
            {
                _rules.Remove(site.ToUpperInvariant());
            }
            finally
            {
                _lockSlim.ExitWriteLock();
            }
        }
    }
}
