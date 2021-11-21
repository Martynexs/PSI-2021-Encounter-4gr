using DataLibrary.Models;
using System;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class Session
    {
        private static readonly Lazy<Session> _session =
            new Lazy<Session>(() => new Session());
        public static Session Instance { get => _session.Value; }

        private readonly EncounterProcessor _api;
        public User CurrentUser { get; set; }
        public string Authentication_token { get; set; }
        private Session()
        {
            _api = EncounterProcessor.Instance;
        }

        public async Task<User> LogIn(string username, string password)
        {
            Authentication_token = await _api.GetAuthenticationToken(username, password);

            if (Authentication_token == null) 
                return null;

            _api.EnableJWTAuthetication(Authentication_token);

            CurrentUser = await _api.GetUser(username);

            return CurrentUser;
        }
    }
}
