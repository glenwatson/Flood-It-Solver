using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Input.AI.Logic.DSModel
{
    class SetRepresentative
    {
        private Point _representative;
        public SetRepresentative(Point point)
        {
            _representative = point;
        }
        /// <summary>
        /// Only we need to know how the rep is stored
        /// </summary>
        /// <returns></returns>
        internal Point GetRep()
        {
            return _representative;
        }

        public override bool Equals(object obj)
        {
            if (obj is SetRepresentative)
            {
                SetRepresentative sr = (SetRepresentative)obj;
                return this._representative.Equals(sr._representative);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _representative.GetHashCode();
        }
    }
}
