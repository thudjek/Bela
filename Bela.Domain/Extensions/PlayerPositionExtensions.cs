using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class PlayerPositionExtensions
    {
        public static PlayerPosition GetNextPosition(this PlayerPosition position)
        {
            switch (position)
            {
                case PlayerPosition.Down:
                    return PlayerPosition.Right;
                case PlayerPosition.Right:
                    return PlayerPosition.Up;
                case PlayerPosition.Up:
                    return PlayerPosition.Left;
                case PlayerPosition.Left:
                    return PlayerPosition.Down;
                default:
                    throw new ArgumentException();
            }
        }

        public static PlayerPosition GetPreviousPosition(this PlayerPosition position)
        {
            switch (position)
            {
                case PlayerPosition.Down:
                    return PlayerPosition.Left;
                case PlayerPosition.Right:
                    return PlayerPosition.Down;
                case PlayerPosition.Up:
                    return PlayerPosition.Right;
                case PlayerPosition.Left:
                    return PlayerPosition.Up;
                default:
                    throw new ArgumentException();
            }
        }

        public static PlayerPosition GetTeammatePosition(this PlayerPosition position)
        {
            switch (position)
            {
                case PlayerPosition.Down:
                    return PlayerPosition.Up;
                case PlayerPosition.Right:
                    return PlayerPosition.Left;
                case PlayerPosition.Up:
                    return PlayerPosition.Down;
                case PlayerPosition.Left:
                    return PlayerPosition.Right;
                default:
                    throw new ArgumentException();
            }
        }

        public static PlayerPosition GetOnScreenPosition(this PlayerPosition myPosition, PlayerPosition otherPosition)
        {
            if (myPosition.GetTeammatePosition() == otherPosition)
                return PlayerPosition.Up;

            if (myPosition.GetNextPosition() == otherPosition)
                return PlayerPosition.Right;

            if (myPosition.GetPreviousPosition() == otherPosition)
                return PlayerPosition.Left;

            return PlayerPosition.Down;
        }
    }
}
