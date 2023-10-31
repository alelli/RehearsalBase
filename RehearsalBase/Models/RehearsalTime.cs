namespace RehearsalBase.Models
{
    public class RehearsalTime : IEquatable<RehearsalTime>
    {
        public DateOnly Date { get; set; }
        public List<TimeOnly> Time { get; set; } = new List<TimeOnly>()
        {
            new TimeOnly(10,0,0),
            new TimeOnly(11,0,0),
            new TimeOnly(12,0,0),
            new TimeOnly(13,0,0),
            new TimeOnly(14,0,0),
            new TimeOnly(15,0,0),
            new TimeOnly(16,0,0),
            new TimeOnly(17,0,0),
            new TimeOnly(18,0,0),
            new TimeOnly(19,0,0),
            new TimeOnly(20,0,0),
            new TimeOnly(21,0,0)
        };

        public bool Equals(RehearsalTime? other)
        {
            if (other is null)
                return false;

            bool timeEquals = true;
            for (int i = 0; i < Time.Count; i++)
            {
                if (Time[i] != other.Time[i])
                {
                    timeEquals = false;
                    break;
                }
            }
            return Date == other.Date && timeEquals;
        }

        public override bool Equals(object obj) => Equals(obj as RehearsalTime);
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var t in Time)
                {
                    hash = hash * 31 + t.GetHashCode();
                }
                int hashDate = hash * 23 + Date.GetHashCode();
                return hash ^ hashDate;
            }
        }
    }
}
