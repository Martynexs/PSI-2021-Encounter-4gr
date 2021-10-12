using System;
using System.Collections.Generic;

namespace Encounter
{
    public class LabelValueItem<T> : IEquatable<LabelValueItem<T>>
    {
        public string Label {get; set;}
        public T Value { get; set; }

        public LabelValueItem(string label, T value)
        {
            Label = label;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LabelValueItem<T>);
        }

        public bool Equals(LabelValueItem<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
