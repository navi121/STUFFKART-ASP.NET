using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuffKartProject.Models
{
  public abstract class CardDetails
  {
    public abstract string CardType { get; set; }
    public abstract int CreditLimit { get; set; }
    public abstract int AnnualCharge { get; set; }
  }
}
