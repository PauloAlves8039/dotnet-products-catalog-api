namespace ProductCatalog.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error) { }

        public static void When(bool hasErros, string error)
        {
            if (hasErros)
            {
                throw new DomainExceptionValidation(error);
            }
        }
    }
}
