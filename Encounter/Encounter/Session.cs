using System;
namespace Encounter.Models
{
    class Session
    {
        private static Session _Session;
        private string _Username;

        private Session(string Nickname)
        {
            _Username = Nickname;
        }
        public static Session CreateNewSession(string Nickname)
        {
            if (_Session != null)
            {
                throw new InvalidOperationException("Session already exists");
            }
            _Session = new Session(Nickname);
            return _Session;
        }

        public static string Get_Username()
        {
            if(_Session == null)
            {
                return null;
            }
            return _Session._Username;
        }

    }
}
