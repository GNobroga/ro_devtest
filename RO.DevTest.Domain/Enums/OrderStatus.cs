using System.ComponentModel;

namespace RO.DevTest.Domain.Enums;

public enum OrderStatus {
    [Description("Cancelado")]
    Cancelled = 1, 
    [Description("Pago")]
    Paid = 2, 
    [Description("Pendente")]
    Pending = 3
}
