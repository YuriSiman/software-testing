using FluentValidation.Results;

namespace NerdStore.Core.Messages
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            TimeStamp = DateTime.Now;
        }

        public abstract bool EhValido();
    }
}
