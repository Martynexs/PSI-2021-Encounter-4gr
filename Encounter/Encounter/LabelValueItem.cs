using System;
using System.Collections.Generic;

namespace Encounter
{
    public class LabelValueItem<T> : IEquatable<LabelValueItem<T>>
    {
        public string label {get; set;}
        public T value { get; set; }

        public LabelValueItem(string label, T value)
        {
            this.label = label;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LabelValueItem<T>);
        }

        public bool Equals(LabelValueItem<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
