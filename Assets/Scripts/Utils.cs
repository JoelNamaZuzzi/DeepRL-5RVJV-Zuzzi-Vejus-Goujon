using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class IntList : List<int>
    {
        public double RealNumber { get; set; }
        public double ImaginaryUnit { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as IntList);
        }

        public bool Equals(IntList other)
        {
            for(int i = 0; i < Count; i++)
            {
                if(other[i] != this[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = this[0];
            
            for(int i = 1; i < Count; i++)
            {
                hash = hash ^ (this[i] << i);
            }

            return hash;
        }
    }
}