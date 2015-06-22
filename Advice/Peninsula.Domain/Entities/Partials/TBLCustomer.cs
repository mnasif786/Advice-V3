using System;

namespace Peninsula.Domain.Entities.Partials
{
    public partial class TBLCustomer
    {
        public static TBLCustomer Create()
        {
            return new TBLCustomer();
        }
    }
}
