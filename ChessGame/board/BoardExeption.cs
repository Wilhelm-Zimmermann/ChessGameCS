using System;

namespace board
{
    class BoardExeption:ApplicationException
    {
        public BoardExeption(string message) : base(message) { }
    }
}
