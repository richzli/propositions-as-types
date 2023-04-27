using System;

namespace Syntax {

    public class UnfilledHoleException : Exception {
        public UnfilledHoleException() {}
        public UnfilledHoleException(Hole h) : base(String.Format("Hole {0} is unfilled", h.label)) {}
    }

}