﻿using SwinGameSDK;

namespace CodeConversion
{
    public abstract class AIPlayer : Player
    {

        /// <summary>
    /// Location can store the location of the last hit made by an
    /// AI Player. The use of which determines the difficulty.
    /// </summary>
        protected class Location
        {
            private int _Row;
            private int _Column;

            /// <summary>
        /// The row of the shot
        /// </summary>
        /// <value>The row of the shot</value>
        /// <returns>The row of the shot</returns>
            public int Row
            {
                get
                {
                    return _Row;
                }
                set
                {
                    _Row = value;
                }
            }

            /// <summary>
        /// The column of the shot
        /// </summary>
        /// <value>The column of the shot</value>
        /// <returns>The column of the shot</returns>
            public int Column
            {
                get
                {
                    return _Column;
                }
                set
                {
                    _Column = value;
                }
            }

            /// <summary>
        /// Sets the last hit made to the local variables
        /// </summary>
        /// <param name="row">the row of the location</param>
        /// <param name="column">the column of the location</param>
            public Location(int row, int column)
            {
                _Column = column;
                _Row = row;
            }

            /// <summary>
        /// Check if two locations are equal
        /// </summary>
        /// <param name="this">location 1</param>
        /// <param name="other">location 2</param>
        /// <returns>true if location 1 and location 2 are at the same spot</returns>
            public static bool operator ==(Location @this, Location other)
            {

                //return !(@this is null) && other != null && @this.Row == other.Row && @this.Column == other.Column;
                if (ReferenceEquals(@this, null))
                    return false;

                return @this.Equals(other);
            }

            /// <summary>
        /// Check if two locations are not equal
        /// </summary>
        /// <param name="this">location 1</param>
        /// <param name="other">location 2</param>
        /// <returns>true if location 1 and location 2 are not at the same spot</returns>
            public static bool operator !=(Location @this, Location other)
            {
                //return (@this is null) || other == null || @this.Row != other.Row || @this.Column != other.Column;

                return !(@this == other);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                if (!(obj is Location))
                    return false;
                Location other = (Location)obj;

                return (this.Row == other.Row) && (this.Column == other.Column);
            }

            public override int GetHashCode() {
                string hash = this.Row + ", " + this.Column;
                return hash.GetHashCode();
            }
        }



        public AIPlayer(BattleShipsGame game) : base(game)
        {
        }

        /// <summary>
    /// Generate a valid row, column to shoot at
    /// </summary>
    /// <param name="row">output the row for the next shot</param>
    /// <param name="column">output the column for the next show</param>
        protected abstract void GenerateCoords(ref int row, ref int column);

        /// <summary>
    /// The last shot had the following result. Child classes can use this
    /// to prepare for the next shot.
    /// </summary>
    /// <param name="result">The result of the shot</param>
    /// <param name="row">the row shot</param>
    /// <param name="col">the column shot</param>
        protected abstract void ProcessShot(int row, int col, AttackResult result);

        /// <summary>
    /// The AI takes its attacks until its go is over.
    /// </summary>
    /// <returns>The result of the last attack</returns>
        public override AttackResult Attack()
        {
            AttackResult result;
            int row = 0;
            int column = 0;

            do
            {
                Delay();
                GenerateCoords(ref row, ref column);
                result = _game.Shoot(row, column);
                ProcessShot(row, column, result);
            }
            while (result.Value != ResultOfAttack.Miss && result.Value != ResultOfAttack.GameOver && !SwinGame.WindowCloseRequested())// generate coordinates for shot// take shot
    ;

            return result;
        }

        /// <summary>
    /// Wait a short period to simulate the think time
    /// </summary>
        private void Delay()
        {
            int i;
            int wait = 5;
            switch (_game.AITimer) {
                case AITime.Short:
                    wait = 5;
                    break;
                case AITime.Normal:
                    wait = 75;
                    break;
                case AITime.Long:
                    wait = 150;
                    break;
            }
            for (i = 0; i <= wait; i++)
            {
                // Dont delay if window is closed
                if (SwinGame.WindowCloseRequested())
                    return;

                SwinGame.Delay(5);
                SwinGame.ProcessEvents();
                SwinGame.RefreshScreen();
            }
        }
    }
}
