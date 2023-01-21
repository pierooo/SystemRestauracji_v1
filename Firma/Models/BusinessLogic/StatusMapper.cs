using System.Collections.Generic;
using System.Security.Policy;

namespace SystemRestauracji.Models.BusinessLogic
{
    public static class StatusMapper
    {
        public static string MapToDbStatus(Status status)
        {
            var Added = "Dodano";
            var InProgress = "Realizacja";
            var Paid = "Zapłacono";
            var Done = "Zakończono";
            var Cancelled = "Anulowano";
            var Fee = "Wydatek";

            switch (status)
            {
                case (Status.Added): return Added;
                case (Status.InProgress): return InProgress;
                case (Status.Paid): return Paid;
                case (Status.Done): return Done;
                case (Status.Cancelled): return Cancelled;
                case (Status.Fee): return Fee;
                default: return "";
            }  
        }
    }
}
