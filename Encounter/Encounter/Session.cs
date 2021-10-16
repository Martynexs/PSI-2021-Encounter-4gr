using System;
namespace Encounter.Models
{
    class Session
    {
        private static Session _Session;
        private User _user;

        private Session(User user)
        {
            _user = user;
        }
        public static Session CreateNewSession(User user)
        {
            if (_Session != null)
            {
                throw new InvalidOperationException("Session already exists");
            }
            _Session = new Session(user);
            return _Session;
        }

        public static string Get_Username()
        {
            if(_Session == null)
            {
                return null;
            }
            return _Session._user.Nickname;
        }

    }
}
