using System;

namespace HarvestHaven.Utils
{
    public static class GameStateManager
    {
        private static Guid _currentUserId;

        public static Guid CurrentUserId
        {
            get { return _currentUserId; }
        }

        public static void SetCurrentUserId(Guid userId)
        {
            _currentUserId = userId;
        }
    }
}
